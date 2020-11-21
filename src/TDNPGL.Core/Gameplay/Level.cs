using TDNPGL.Core.Gameplay.Assets;
using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.Generic;
using TDNPGL.Core.Gameplay.Interfaces;

namespace TDNPGL.Core.Gameplay
{
    public class Level : ContentFile, IParentable
    {
        public static Level Empty { get { return new Level(); } }
        public bool IsObjectsLoaded()
        {
            for(int i = 0; i < objects.Count;i++)
            {
                if (!objects[i].Loaded)
                    return false;
            }
            return true;
        }
        [JsonProperty("name")]
        public string Name { get; protected set; }
        public override string ContentType { get; set; }
        #region Rendering
        [JsonIgnore]
        public SKColor BackColor = new SKColor();
        [JsonProperty("back_color")]
        public string BackColor_HEX { get { return BackColor.ToString(); } set { BackColor = SKColor.Parse(value); } }
        [JsonProperty("camera_position")]
        public SKPoint CameraPosition = new SKPoint();
        #endregion
        #region Objects
        [JsonProperty("objects")]
        private List<GameObject> objects = new List<GameObject>();
        [JsonIgnore]
        public GameObject[] Objects => objects.ToArray();
        [JsonIgnore]
        public IReadOnlyCollection<GameObject> ObjectsCollection => objects;
        [JsonIgnore]
        public GameObjectUpdater Updater { get; protected set; }
        [JsonIgnore]
        public IParentable Parent { get; set; } = null;
        #endregion
        internal Level() : base("level")
        {
            Updater = new GameObjectUpdater(this);
            BackColor = SKColor.Parse(BackColor_HEX);
        }
        public void BeginUpdate()
        {
            foreach (GameObject @object in objects)
            {
                @object.Parent = this;
                @object.OnCreate();
            }

            Updater.Start();
        }
        public void AddObject(GameObject @object)
        {
            objects.Add(@object);
            @object.LevelID = objects.Count - 1;
        }
        public GameObject GetObject(int index) => objects[index];
        public void RemoveObject(int index) => objects.RemoveAt(index);
        public void SortObjects() => objects.Sort();
        internal void ReloadObjectsIDs()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].LevelID = i;
            }
        }
    }
}
