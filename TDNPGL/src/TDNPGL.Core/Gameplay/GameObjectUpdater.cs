using TDNPGL.Core.Debug;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TDNPGL.Core.Gameplay
{
    public class GameObjectUpdater
    {
        private Thread ObjectUpdateThread;
        public Level Level { get; private set; }

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
                Console.WriteLine(ex);
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
                    if(ex is InvalidOperationException) { }
                    else
                        Logging.WriteError(ex);
                }
        }

        public GameObjectUpdater(Level level)
        {
            this.Level = level;
        }
    }
}
