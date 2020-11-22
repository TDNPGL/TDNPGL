using System;
using System.IO;

namespace TDNPGL.Core.Debug
{
    public static class Logging
    {
        internal static TextWriter DefaultWriter;

        internal static Random GameRandom=new Random();

        public static void SetConsoleColor(ConsoleColor color) =>
            Console.ForegroundColor = color;
        public static void WriteError(Exception ex)
        {
            SetConsoleColor(ConsoleColor.Red);
            DefaultWriter.WriteLine("\n"+ex);
            Console.ResetColor();
        }
        public static void MessageAction(string from, string msg,ConsoleColor HeaderColor=ConsoleColor.Green, ConsoleColor MsgColor = ConsoleColor.Gray,params object[] MsgParams)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            SetConsoleColor(ConsoleColor.DarkGray);
            DefaultWriter.Write("("+DateTime.Now.ToString().Split(' ')[1]+")");
            Console.ForegroundColor = HeaderColor;
            DefaultWriter.Write(" [" + from + "]");
            Console.ForegroundColor = MsgColor;
            DefaultWriter.WriteLine(" " + msg,MsgParams);

            Console.ForegroundColor = prevColor;
        }
        public static void SetCustomLogger()
        {
            DefaultWriter = Console.Out;

            Console.SetOut(new CustomLogger());
        }
        public static void ResetLogger()
        {
            Console.SetOut(DefaultWriter);
        }
    }
}
