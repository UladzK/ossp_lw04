#include <stdio.h>
#include <string.h>
#include <stdlib.h>

char *strCut(char *, int, int);

/* return number of occurences of word in line */
int zFunction(char *word, char *line){
	
	int ptr = 0, occur = 0, i;
	int lineLength = strlen(line);
	int wordLength = strlen(word);
	char *wordBuf;

	while (ptr < lineLength){
		wordBuf = strCut(line, ptr, wordLength);
		printf("current occur: %d\n", occur);
		printf("wordBuf: %s, line: %s\n", wordBuf, line);

		if (strcmp(wordBuf, word) == 0){
			occur++;
			ptr += wordLength;
		}
		else
			ptr++;
	}

	return occur;
}

char *strCut(char *from, int whence, int number){
	int i;
	char *wordBuf = (char *)malloc( (number) * sizeof(char) );
	
	for(i = 0; i < whence; i++)
		from++;
	strncpy(wordBuf, from, number);
	return wordBuf;
}