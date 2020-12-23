using System;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;

namespace TDNPGL.Cli
{
    public partial class CLI
    {
        public void RunWithArgs(string[] args)
        {
            if (args.Length > 0)
            {
                string command = args[0];
                switch (command)
                {
                    case "-about":
                        this.ShowASCIIMessage();
                        break;
                    case "-info":
                        goto case "-about";
                    case "-help":
                        this.ShowHelpMessage(args.Length > 1 ? args[1] : "");
                        break;
                    case "-create":
                        string gameName = args.Length > 1 
                            ? Path.GetFileNameWithoutExtension(Directory.GetCurrentDirectory()) : args[1];
                        this.CreateNewProject(gameName+".Assets");
                        break;
                    default:
                        this.ShowAboutHelpMessage("Unknown command \'" + command + "\'.");
                        break;
                }
            }
            else
            {
                this.ShowAboutHelpMessage("Command not specifed.");
            }
        }
    }
}
