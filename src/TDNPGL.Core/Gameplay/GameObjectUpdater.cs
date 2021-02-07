using System;
using System.Threading;
using System.Threading.Tasks;

namespace TDNPGL.Core.Gameplay
{
    public class GameObjectUpdater
    {
        private Task ObjectUpdateTask;

        public Level Level { get; private set; }
        private Game game{get;set;}

        public void Start()
        {
            ObjectUpdateTask = new Task(UpdateTHR);
            ObjectUpdateTask.Start();
        }
        public void Stop()
        {
            try
            {
                ObjectUpdateTask.Dispose();
            }
            catch (Exception ex)
            {
#if DEBUG
                throw;
#else
                Exceptions.Call(ex);
#endif
            }
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
