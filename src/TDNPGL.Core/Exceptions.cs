using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Core
{
    public static class Exceptions
    {
        #region Type Defines
        public delegate void ExceptionEventHandler(ExceptionEventArgs args);
        public class ExceptionEventArgs : EventArgs {
            public Exception Exception;
            public ExceptionEventArgs(Exception exception)
            {
                this.Exception = exception;
            }
        }
        #endregion
        public static bool NoThrowExceptions = true;
        public static event ExceptionEventHandler ExceptionHandler=
            new ExceptionEventHandler(delegate (ExceptionEventArgs args) {
                Debug.Logging.WriteError(args.Exception);
            });
        public static void Call(Exception ex)
        {
            ExceptionHandler.Invoke(new ExceptionEventArgs(ex));
        }
    }
}
