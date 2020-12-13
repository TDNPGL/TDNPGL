using System;
using System.Collections.Generic;
using System.Text;
using TDNPGL.Core.Gameplay;

namespace TDNPGL.Core.Debug.Exceptions
{
    public class AssetsException : TDNPGLException
    {
        public AssetsException(Level level, DateTime time) : base(level, time, "Wrong asset")
        {
        }

        public AssetsException(DateTime time) : base(Game.GetInstance().CurrentLevel, time, "Wrong asset")
        {
        }

        public AssetsException(Level level, string message) : base(level, DateTime.Now, message)
        {
        }

        public AssetsException(string message) : base(Game.GetInstance().CurrentLevel, DateTime.Now, message)
        {
        }
    }
}