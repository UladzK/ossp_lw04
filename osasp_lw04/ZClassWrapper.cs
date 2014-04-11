using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace osasp_lw04
{
    class ZClassWrapper
    {
        [DllImport("C:\\osasp_lw04\\osasp_lw04\\bin\\Debug\\ZFuncDLL.dll",
            EntryPoint = "zFunction",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int ZFunction(string word, string line);
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
    }
}
