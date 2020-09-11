using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TDNPGL.Core.Debug
{
    public class CustomLogger : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(string value)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Logging.SetConsoleColor(ConsoleColor.DarkGray);
            Logging.DefaultWriter.Write("(" + DateTime.Now.ToString().Split(' ')[1] + ")");
            Console.ForegroundColor = ConsoleColor.Green;
            Logging.DefaultWriter.Write(" [" + "DEBUG" + "]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Logging.DefaultWriter.WriteLine(" " + value);
            Logging.SetConsoleColor(prevColor);
        }
        public override void WriteLine(string value)
        {
            Write(value);
            base.WriteLine();
        }
    }
}
