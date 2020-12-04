using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDNPGL.Cli
{
    public class HelpMessage
    {
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("aliases")]
        public string[] Aliases;
        public void Print()
        {
            ConsoleMethods.WriteWithColor(null, "tdnpgl " + Name, ConsoleColor.Yellow);
            Console.WriteLine(" - \n" + Description);
            Console.Write("Aliases: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Aliases.ToList().ForEach((string str) => 
            Console.WriteLine(str + 
            (Aliases.Last()==str ? "" : "; ")
            ));
            Console.ResetColor();
        }
        public HelpMessage(string Description, string Name, params string[] Aliases)
        {
            this.Description = Description;
            this.Name = Name;
            this.Aliases = Aliases;
        }
    }
}
