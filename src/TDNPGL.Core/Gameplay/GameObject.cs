using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Graphics;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Linq;
using TDNPGL.Core.Gameplay.Interfaces;
using TDNPGL.Core.Debug.Exceptions;
using System.Threading;
using TDNPGL.Core.Math;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TDNPGL.Core.Gameplay
{
    [Serializable]
    public class GameObject : IParentable, IEquatable<GameObject>, IComparable, IUpdateable, INotifyPropertyChanged
    {
        #region Private Fields
        [JsonIgnore]
        protected bool firstTick = true;
        #endregion
        #region Delegate
        public event PropertyChangedEventHandler PropertyChanged;
        public GameObjectEventHandler Tick;
        #endregion
        #region Animation
        private bool Paused = false;
        [JsonProperty("sprite")]
        public string SpriteName
        {
            get
            {
                return spritename;
            }
            set
            {
                spritename = value;
                Sprite = Graphics.Sprite.Sprites[value];
                NotifyPropertyChange();
            }
        }
        [JsonIgnore]
        private string spritename;
        public GameObject(Sprite sprite, bool loaded, IParentable parent)
        {
            this.Sprite = sprite;
            this.Loaded = loaded;
            this.Parent = parent;

        }
        [JsonIgnore]
        public Sprite Sprite { get; private set; }
        private int spriteIndex = 0;
        [JsonIgnore]
        public int SpriteIndex
        {
            get { return spriteIndex; }
            set
            {
                spriteIndex = value;
                NotifyPropertyChange();
            }
        }
        [JsonIgnore]
        public SKBitmap SpriteBitmap => this.Sprite.Frames[SpriteIndex];

        [JsonProperty("animate_delay")]
        public int UpdateDelay;
        [JsonProperty("z_layer")]
        public int ZLayer = 0;

        [JsonIgnore]
        private Task AnimationTask;

        public void BeginAnimation()
        {
            AnimationTask = new Task(() =>
            {
                while (true)
                {
                    while (Paused) ;
                    #region Animate
                    if (SpriteIndex == Sprite.Frames.Length - 1) SpriteIndex = 0;
                    else SpriteIndex++;
                    #endregion
                    Task.Delay(this.UpdateDelay).Wait();
                }
            }
            );
            AnimationTask.Start();
        }
        public void StopAnimation() => AnimationTask.Dispose();
        public void PauseAnimation() => Paused = true;
        public void ResumeAnimation() => Paused = false;
        public void ChangeAnimationState() => Paused = !Paused;
        public void SetSprite()
        {
            SpriteIndex = 0;
            NotifyPropertyChange();
        }
        #endregion
        #region Controllers
        [JsonIgnore]
        public bool Loaded { get; private set; } = false;
        [JsonIgnore]
        public Guid InLevelID { get; internal set; }
        private AABB aABB = new AABB();
        [JsonProperty("aabb")]
        public AABB AABB
        {
            get { return aABB; }
            set
            {
                aABB = value;
                NotifyPropertyChange();
            }
        }
        [JsonIgnore]
        public Vec2f Position => AABB.min;
        [JsonIgnore]
        public SKSize Size => new SKSize(AABB.max.X - AABB.min.X, AABB.max.Y - AABB.min.Y);
        [JsonIgnore]
        public SKRect Rect => AABB.ToRect();

        public IParentable Parent { get; set; }

        [HandleProcessCorruptedStateExceptions]
        private static void OnTick(GameObject @object)
        {
            try
            {
                if (@object.firstTick)
                    @object.OnFirstTick();
                @object.OnTick();
                @object.firstTick = false;

                if (@object.Parent == null)
                    return;

                List<GameObject> objects = (@object?.Parent as Level)?.Objects.ToList();
                foreach (GameObject obj in objects)
                {
                    if (AABB.AABBvsAABB(obj.AABB, @object.AABB))
                    {
                        obj.CollidedWith(@object);
                        @object.CollidedWith(obj);
                    }
                }
            }
            catch
            {
            }
        }
        internal void Render(SKCanvas canvas)
        {
            SKRect rect = Rect;
            double pixelSize = GraphicsOutput.GetMainRenderer().PixelSize;

            IParentable parent = Parent;
            while (!(parent is Level))
                parent = parent.Parent;

            Level level = parent as Level;

            rect.Left = (float)(rect.Left * pixelSize) + (float)(level.CameraPosition.X * pixelSize);
            rect.Right = (float)(rect.Right * pixelSize) + (float)(level.CameraPosition.X * pixelSize);
            rect.Bottom = (float)(rect.Bottom * pixelSize) + (float)(level.CameraPosition.Y * pixelSize);
            rect.Top = (float)(rect.Top * pixelSize) + (float)(level.CameraPosition.Y * pixelSize);

            canvas.DrawBitmap(SpriteBitmap, rect);
        }
        #endregion
        #region UserListeners
        [JsonProperty("listeners")]
        public string[] listeners;
        [JsonIgnore]
        public List<CSharpGameObjectListener> Listeners = new List<CSharpGameObjectListener>();
        internal void CollidedWith(GameObject obj)
        {
            foreach (CSharpGameObjectListener listener in Listeners)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                listener.OnCollideWith(state as GameObject), obj);
            }
        }
        public virtual void OnMouseReleased(int button, SkiaSharp.SKPoint point)
        {
            foreach (CSharpGameObjectListener script in Listeners)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                    script.OnMouseReleased(button, point));
                if (AABB.IsPointOver(AABB, point.ToVec2f()))
                    ThreadPool.QueueUserWorkItem((object state) =>
                        script.OnMouseReleasedOver(button, point));
            }
        }
        public virtual void OnMouseReleasedOver(int button, SkiaSharp.SKPoint point)
        {
        }
        internal void LoadListeners()
        {
            if (listeners != null)
                foreach (string scriptName in listeners)
                {
                    Type type = (Parent as Level).Game.CurrentEntry.GetScript(scriptName);
                    if (type == null)
                        throw new AssetsException(Parent as Level, "Script not found!");
                    CSharpGameObjectListener script = Activator.CreateInstance(type, this) as CSharpGameObjectListener;
                    Listeners.Add(script);
                }
            else
                Console.WriteLine($"Wrong object format!");
        }
        #endregion
        #region Implementations
        public bool Equals(GameObject other)
        {
            return other.ZLayer.Equals(ZLayer);
        }
        public int CompareTo(object obj)
        {
            return obj is GameObject ? (obj as GameObject).ZLayer.CompareTo(ZLayer) : -1;
        }
        public virtual void OnTick()
        {
            foreach (CSharpGameObjectListener listener in Listeners)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                listener.OnTick());
            }
        }
        public virtual void OnStart()
        {
            LoadListeners();
            foreach (CSharpGameObjectListener listener in Listeners)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                listener.OnStart());
            }
            Loaded = true;
        }
        public virtual void OnFirstTick()
        {
            foreach (CSharpGameObjectListener listener in Listeners)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                listener.OnFirstTick());
            }
        }

        public void OnMouseMove(int button, SKPoint point)
        {
            foreach (CSharpGameObjectListener listener in Listeners)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                listener.OnMouseMove(button, point));
            }
        }
        public void OnMouseDown(int button, SKPoint point)
        {
            foreach (CSharpGameObjectListener listener in Listeners)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                listener.OnMouseDown(button, point));
            }
        }
        private void NotifyPropertyChange([CallerMemberName] string name = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void OnKeyDown(ConsoleKeyInfo key)
        {
            foreach (CSharpGameObjectListener listener in Listeners)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                listener.OnKeyDown(key));
            }
        }
        #endregion
        #region Constructors
        [JsonConstructor]
        internal GameObject(IParentable parent)
        {
            this.Parent = parent;
            SKBitmap bmp = new SKBitmap(1, 1);
            bmp.Erase(SKColors.White);
            Sprite = new Sprite(bmp);
            this.AABB = new AABB() { max = new Vec2f(100, 100) };
            this.BeginAnimation();

            Tick += OnTick;
        }
        public GameObject(IParentable parent, string sprite) : this(parent, Sprite.Sprites[sprite])
        {
        }
        public GameObject(IParentable parent, Sprite sprite) : this(parent)
        {
            this.Sprite = sprite;
        }
        #endregion
        #region Networking
        [JsonProperty("multiplayer")]
        public bool UseInMultiplayer = true;
        #endregion
    }
}
