using System.Reflection;
using TDNPGL.Core.Gameplay.Assets;

namespace TDNPGL.Core.Gameplay.Interfaces
{
    public interface IGameInitializer
    {
        Game game{get;set;}
        public Game CreateGame(Assembly assembly, string GameName);
        public Game CreateGame<EntryType>(string GameName) where EntryType : EntryPoint;
    }
}
