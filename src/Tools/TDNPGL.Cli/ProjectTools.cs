using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;

namespace TDNPGL.Cli
{
    public static class ProjectTools
    {
        private static string fsSlash=>Os.FileSystemSlash;
        public static void CreateNewProject(this CLI cli,string name)
        {
            cli.ShowRunCliForMessage("create project");
            Console.WriteLine("DotNet CLI output: ");

            Directory.CreateDirectory(name);
            Directory.CreateDirectory(name+fsSlash+"Resources");

            Process create = RunCliWithArgs(name,"new", "classlib","--force");
            create.WaitForExit();

            cli.ShowRunCliForMessage("add TDNPGL.Core to project");
            Console.WriteLine("DotNet CLI output: ");

            Process addNuget = RunCliWithArgs(name, "add", "package", "TDNPGL.Core");
            addNuget.WaitForExit();

            CreateResourcesForProject(name);
        }
        public static void CreateSolution(this CLI cli,string name,params string[] projects)
        {
            cli.ShowRunCliForMessage("create solution");
            Console.WriteLine("DotNet CLI output: ");
            Process createSLN = RunCliWithArgs(".", "new", "sln","--name", name);
            createSLN.WaitForExit();
            projects.ToList().ForEach(x=>{
                cli.ShowRunCliForMessage("add project to sln");
                RunCliWithArgs(".", "sln","add", x).WaitForExit();
            });
        }
        private static void CreateResourcesForProject(string name)
        {
            string resourcesDirectory = name + fsSlash + "Resources"+fsSlash;
            string file = resourcesDirectory+"lvl_main.json";
            Level lvl_main = Level.Empty;
            lvl_main.Name = "lvl_main";

            string res="\n  <ItemGroup>\n\t<Resource Include=\"Resources\\**\\*.json\" />\n  </ItemGroup>";
            string projFile=name+fsSlash+name+".csproj";
            string[] projContent=File.ReadAllLines(projFile);
            projContent[projContent.Length-2]+=(res);
            File.WriteAllLines(projFile,projContent);

            string emptyLevelJson = lvl_main.ToJSON();
            File.WriteAllText(file,emptyLevelJson);
        }
        private static Process RunCliWithArgs(string workingDir,params string[] args)
        {
            Process process = new Process();
            process.StartInfo.FileName = "dotnet";

            args.ToList().ForEach(x => process.StartInfo.ArgumentList.Add(x));
            process.StartInfo.WorkingDirectory = workingDir;

            process.Start();

            return process;
        }
        internal static void ShowRunCliForMessage(this CLI cli,string target){
            cli.WriteWithColor("Running dotnet cli for "+target+"...\n", ConsoleColor.Yellow);
        }
    }
}
