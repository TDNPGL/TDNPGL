using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;

namespace TDNPGL.Cli
{
    public static class ProjectTools
    {
        public static void CreateNewProject(this CLI cli,string name)
        {
            cli.WriteWithColor("Running dotnet cli for create project...\n", ConsoleColor.Yellow);
            Console.WriteLine("DotNet CLI output: ");

            Directory.CreateDirectory(name);
            Directory.CreateDirectory(name+"\\Resources");

            Process create = RunCliWithArgs(name,"new", "classlib");
            create.WaitForExit();

            cli.WriteWithColor("Running dotnet cli for add TDNPGL.Core to project...\n", ConsoleColor.Yellow);
            Console.WriteLine("DotNet CLI output: ");

            Process addNuget = RunCliWithArgs(name, "add", "package", "TDNPGL.Core");
            addNuget.WaitForExit();

            CreateResourcesForProject(name);
        }
        public static void CreateSolution(string name)
        {

        }
        private static void CreateResourcesForProject(string name)
        {
            string resourcesDirectory = name + "\\Resources\\";
            string file = resourcesDirectory+"Resources.resources";
            ResourceWriter writer = new ResourceWriter(new FileStream(file, FileMode.OpenOrCreate));
            Level lvl_main = Level.Empty;
            lvl_main.Name = "lvl_main";
            string emptyLevelJson = lvl_main.ToJSON();
            writer.AddResource("lvl_main", emptyLevelJson);
            writer.Generate();
        }
        private static Process RunCliWithArgs(string workingDir,params string[] args)
        {
            Process process = new Process();
            process.StartInfo.FileName = "dotnet";

            args.ToList().ForEach(x => process.StartInfo.ArgumentList.Add(x));
            process.StartInfo.WorkingDirectory = workingDir;

            process.StartInfo.RedirectStandardOutput = true;

            process.Start();

            return process;
        }
    }
}
