using System;
using System.Linq;
using System.Reflection;
using TDNPGL.Cli.Properties;

namespace TDNPGL.Cli
{
    static class Messages
    {
        internal const string githubRepo = "https://github.com/zatrit/TDNPGL";
        internal const string nugetPackage = "https://www.nuget.org/packages/TDNPGL.Core";
        internal const string documentation = "https://tdnpgl.readthedocs.io/en/latest/";

        internal static void ShowASCIIMessage(this CLI cli)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Resources.AsciiArt);
            Console.ResetColor();
            Console.Write("\nIt's ");
            cli.WriteWithColor("TDNPGL CLI.\n\n", ConsoleColor.Yellow);
            cli.WriteWithColor("Diagnostics: ", ConsoleColor.DarkGreen);
            Console.WriteLine("\nResources and console working fine");
            cli.WriteLink("\nGithub repo", githubRepo);
            cli.WriteLink("Nuget package", nugetPackage);
            cli.WriteLink("Documentation", documentation);

            Version version = Assembly.GetEntryAssembly().GetName().Version;
            cli.WriteLink("Version", version.ToString()+version.Build);
        }
        internal static void ShowAboutHelpMessage(this CLI cli, string firstString)
        {
            cli.WriteWithColor(firstString, ConsoleColor.Yellow);
            Console.Write("\nUse ");
            cli.WriteWithColor("tdnpgl -help", ConsoleColor.Yellow);
            Console.WriteLine(" for help");
        }
        internal static void ShowHelpMessage(this CLI cli, string command = "")
        {
            if (string.IsNullOrEmpty(command))
            {
                Resources.HelpMessages.First().Print();
                foreach (HelpMessage message in Resources.HelpMessages.Skip(1))
                {
                    Console.WriteLine("\n");
                    message.Print();
                }
            }
            else
                Resources.HelpMessages.LastOrDefault(x => x.Name == command ||
                x.Aliases.Contains(command))
                    .Print();
        }
    }
}
