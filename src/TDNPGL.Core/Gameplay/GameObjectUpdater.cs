using System;
using System.Threading;

namespace TDNPGL.Core.Gameplay
{
    public class GameObjectUpdater
    {
        private Thread ObjectUpdateThread;
        public Level Level { get; private set; }
        public Game game{get;private set;}

        public void Start()
        {
            ObjectUpdateThread = new Thread(UpdateTHR);
            ObjectUpdateThread.Name = "GameObjectUpdateThread_"+Level.Name;
            ObjectUpdateThread.Start();
        }
        public void Stop()
        {
            try
            {
                ObjectUpdateThread.Abort();
            }
            catch (Exception ex)
            {
                Exceptions.Call(ex);
            }
        }
        internal static void PressKey(ConsoleKeyInfo keyInfo)
        {

        }
        private void UpdateTHR()
        {
            while (true) try
                {
                    foreach (GameObject obj in Level.Objects)
                        obj.Tick?.Invoke(obj);
                    Thread.Sleep(10);
                }
                catch(Exception ex)
                {
                    if (ex is InvalidOperationException) { }
                    else
                        Exceptions.Call(ex);
                }
        }

        public GameObjectUpdater(Level level,Game game)
        {
            this.Level = level;
        }
    }
}
