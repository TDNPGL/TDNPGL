using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Cli
{
    static class ConsoleMethods
    {
        public static void WriteLink(this CLI console,string linkName, string link)
        {
            Console.Write(linkName + ": ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(link);
            Console.ResetColor();
        }
        public static void WriteWithColor(this CLI console,string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ResetColor();
        }
    }
}
