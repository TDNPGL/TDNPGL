using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources.NetStandard;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Gameplay.Assets;

namespace TDNPGL.Cli
{
    public static class ProjectTools
    {
        public static void CreateNewProject(this CLI cli,string name,string gameName,string lang)
        {
            cli.ShowRunCliForMessage("create project");
            Console.WriteLine("DotNet CLI output: ");

            Directory.CreateDirectory(name);
            Directory.CreateDirectory(name+"\\Resources");

            Process create = RunCliWithArgs(name,"new", "classlib","--force", "--framework","netstandard2.1","-lang",lang);
            create.WaitForExit();

            cli.ShowRunCliForMessage("add references to project");
            Console.WriteLine("DotNet CLI output: ");

            RunCliWithArgs(name, "add", "package", "TDNPGL.Core").WaitForExit();
            RunCliWithArgs(name, "add", "package", "System.Resources.Extensions").WaitForExit();
            RunCliWithArgs(name, "add", "package", "ResXResourceReader.NetStandard").WaitForExit();

            CreateResourcesForProject(name,gameName,lang);
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
        private static void CreateResourcesForProject(string name,string gameName,string lang)
        {
            string resourcesDirectory = name + "\\Resources\\";
            string resDir2 = "Resources\\";
            //string resxFile = resourcesDirectory + "Resources.resx";

            string lvl_mainFile = resourcesDirectory+"lvl_main.json";
            string lvl_mainFile2 = resDir2 + "lvl_main.json";
            Level lvl_main = Level.Empty;
            lvl_main.Name = "lvl_main";

            string assets_entryFile = resourcesDirectory+"assets_entry.json";
            string assets_entryFile2 = resDir2 + "assets_entry.json";
            EntryPoint assets_entry = new EntryPoint(){
                Name=gameName,
                Namespace=name,
                AutoLoadLevel="lvl_main"};

            string frmat= "\n\t\t<LogicalName>{0}</LogicalName>\n\t</EmbeddedResource>";

            string res = " <ItemGroup>\n\t<EmbeddedResource Include = \"" + lvl_mainFile2+ "\">"+String.Format(frmat, "lvl_main")+"\n\t<EmbeddedResource Include = \"" + assets_entryFile2+ "\">"+ String.Format(frmat,"assets_entry") + "\n  </ItemGroup> ";
            string projFile=name+"\\"+name+"."+GetProjectExtension(null,lang);
            string[] projContent=File.ReadAllLines(projFile);
            projContent[projContent.Length-2]+=(res)+"\n";
            File.WriteAllLines(projFile,projContent);

            //FileStream stream = new FileStream(resxFile,FileMode.OpenOrCreate);
            //ResXResourceWriter resx = new ResXResourceWriter(stream);
            //var lvl_mainRef = CreateRef(lvl_mainFile);
            //resx.AddResource("lvl_main",lvl_mainRef);
            //var assets_entryRef = CreateRef(assets_entryFile);
            //resx.AddResource("assets_entry",assets_entryRef);

            //resx.Generate();
            //resx.Close();
            //resx.Dispose();

            string emptyLevelJson = lvl_main.ToJSON();
            File.WriteAllText(lvl_mainFile,emptyLevelJson);
            string assetsEntryJson = assets_entry.ToJSON();
            File.WriteAllText(assets_entryFile,assetsEntryJson);
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
        internal static ResXFileRef CreateRef(string file)
            => new ResXFileRef(file, typeof(System.String).FullName);
        internal static string GetProjectExtension(this CLI cli,string lang)
        {
            switch (lang.ToLower())
            {
                case "c#":
                    return "csproj";
                case "f#":
                    return "fsproj";
                case "vb":
                    return "vbproj";
                default:
                    goto case "c#";
            }
        }
    }
}
