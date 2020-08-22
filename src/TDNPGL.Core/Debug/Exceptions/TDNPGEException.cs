using System;
using System.Collections.Generic;
using System.Text;
using TDNPGL.Core.Gameplay;

namespace TDNPGL.Core.Debug.Exceptions
{
    public abstract class TDNPGLException : Exception
    {
        public Level Level { get; private set; }
        public DateTime Time { get; private set; }
        public TDNPGLException(Level level,DateTime time):this(level,time,"In-game error!")
        {
        }
        public TDNPGLException(Level level, string message) : this(level, DateTime.Now, message)
        {
        }
        public TDNPGLException(Level level, DateTime time,string message) : base(message)
        {
            this.Level = level;
            this.Time = time;
        }
    }
}
