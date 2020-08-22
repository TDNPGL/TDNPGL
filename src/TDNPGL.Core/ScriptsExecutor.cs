using System.Threading;
using TDNPGL.Core.Gameplay;
using static TDNPGL.Core.ScriptExecutionType;

namespace TDNPGL.Core
{
    public enum ScriptExecutionType
    {
        Collide, Create, Tick, FirstTick
    }
    public static class ScriptsExecutor
    {
        public static void RunScripts(GameObject @object,ScriptExecutionType type,object state)
        {
            foreach(GameObjectScript script in @object.Scripts)
            {
                switch (type)
                {
                    case Collide:
                        ThreadPool.QueueUserWorkItem(CollideAsync,new object[]{@object,state});
                        break;
                    case Create:
                        ThreadPool.QueueUserWorkItem(CollideAsync, new object[] { @object, state });
                        break;
                    case Tick:
                        ThreadPool.QueueUserWorkItem(CollideAsync, new object[] { @object, state });
                        break;
                    case FirstTick:
                        ThreadPool.QueueUserWorkItem(CollideAsync, new object[] { @object, state });
                        break;
                }
            }
        }
        private static void CollideAsync(object state)
        {
            GameObject[] args = state as GameObject[];
        }
        private static void TickAsync(object state)
        {

        }
        private static void CreateAsync(object state)
        {

        }
        private static void FirstTickAsync(object state)
        {

        }
    }
}
