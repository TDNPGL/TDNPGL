using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Sound;

namespace TDNPGL.Core.Gameplay.Interfaces{
    public class GameProvider{
        public IGameRenderer Renderer{get;private set;}
        public ISoundProvider SoundProvider{get;private set;}
        public GameProvider(IGameRenderer renderer,ISoundProvider sound){
            this.Renderer=renderer;
            this.SoundProvider=sound;
        }
    }
}