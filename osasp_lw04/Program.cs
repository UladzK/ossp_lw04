using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osasp_lw04
{
    struct Word{
        public string word;
        public int occur;
    }

    class Program
    {
        //private static string wordToFind = "duck";
        private static string wordToFind;
        //private static string pathToFind = "C:\\lw03\\lw03\\CustomMonitor.cs";
        private static string pathToFind;
        public delegate int MapFunction(string item);
        public delegate Word ReduceFunction(KeyValuePair<string, IEnumerable<int>> item);

        static void Main(string[] args)
        {
            Console.Write("What to find: ");
            wordToFind = Console.ReadLine();

            Console.Write("Where to find: ");
            pathToFind = Console.ReadLine();

            if (File.Exists(pathToFind))
            {
                BlockingCollection<Word> mrResult = MapReduce(Map, Reduce);

                foreach (Word word in mrResult)
                {
                    Console.WriteLine(word.word + " => " + word.occur);
                }

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("File Not Found");
            }
        }

        public static int Map(string sourceItem)
        {
            return ZClassWrapper.ZFunction(wordToFind, sourceItem);
        }

        public static Word Reduce(KeyValuePair<string, IEnumerable<int> > reduceItem)
        {
            return new Word
            {
                word = reduceItem.Key,
                occur = reduceItem.Value.Sum()
            };
        }

        public static BlockingCollection<Word> MapReduce(MapFunction map, ReduceFunction reduce)
        {
            var mapResults = new ConcurrentBag<KeyValuePair<string, int>>();
            
            IEnumerable<string> lines = System.IO.File.ReadAllLines(pathToFind);
            
            Parallel.ForEach(lines, 
                line =>
            {
                int value = map(line);
                if (value != 0)
                {
                    var mapResult = new KeyValuePair<string, int>(wordToFind, value);

                    mapResults.Add(mapResult);
                }
            });
            
            foreach(var mapItem in mapResults){
                Console.WriteLine("Key = " + mapItem.Key + " Value: = " + mapItem.Value);
            }

            // Grouping by key
            var reduceSources = mapResults.GroupBy(
                    item => item.Key,
                    (key, values) => new KeyValuePair<string, IEnumerable<int>>(key, values.Select(i => i.Value)));
           
            var resultCollection = new BlockingCollection<Word>();
                                   
            Parallel.ForEach(reduceSources,
                            (reduceItem) => resultCollection.Add(reduce(reduceItem)));

            resultCollection.CompleteAdding();
            return resultCollection;
        }
    }
}
