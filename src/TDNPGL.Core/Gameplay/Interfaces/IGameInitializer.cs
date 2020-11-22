using System.Reflection;
using TDNPGL.Core.Gameplay.Assets;

namespace TDNPGL.Core.Gameplay.Interfaces
{
    public interface IGameInitializer
    {
        public void InitGame(Assembly assembly, string GameName);
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint;
    }
}
