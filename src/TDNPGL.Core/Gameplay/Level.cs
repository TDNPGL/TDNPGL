using TDNPGL.Core.Gameplay.Assets;
using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.Generic;
using TDNPGL.Core.Gameplay.Interfaces;
using TDNPGL.Core;
using System.Runtime.Serialization;
using System;

namespace TDNPGL.Core.Gameplay
{
    [Serializable]
    public class Level : ContentFile, IParentable, ISerializable
    {
        public static Level Empty { 
            get =>
                new Level(TDNPGL.Core.Game.GetInstance()); 
        }
        public bool IsObjectsLoaded()
        {
            for(int i = 0; i < objects.Count;i++)
            {
                if (!objects[i].Loaded)
                    return false;
            }
            return true;
        }
        [JsonIgnore()]
        public Game Game;
        [JsonProperty("name")]
        public string Name { get; set; }
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
        internal Level(Game game) : base("level")
        {
            Updater = new GameObjectUpdater(this,game);
            BackColor = SKColor.Parse(BackColor_HEX);
        }
        public void BeginUpdate()
        {
            foreach (GameObject @object in objects)
            {
                @object.Parent = this;
                @object.OnStart();
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("objects", this.objects);
            info.AddValue("name", this.Name);
            info.AddValue("backColor", this.BackColor_HEX);
        }
    }
}