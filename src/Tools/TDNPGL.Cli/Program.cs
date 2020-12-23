using System;

namespace TDNPGL.Cli
{
    static class Program
    {
        static void Main(string[] args)
        {
            new CLI().RunWithArgs(args);
            if(NativeMethods.IsOwnConsole())
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
