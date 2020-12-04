using System;

namespace TDNPGL.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(string arg in args)
            {
                Console.Write(arg+" ");
            }
        }
    }
}
