namespace TDNPGL.Core.Graphics
{
    public static class Options
    {
        public static class FPS
        {
            public static int FPSLimit { get { return fpslimit; } set { if (value > 300) fpslimit = 0; else if (value < 20) fpslimit = VSync; else fpslimit = value; } }

            private static int fpslimit = 200;
            public static int FPSDelay { get { return fpslimit == 0 ? 0 : 1000 / fpslimit; } }
            public static int VSync = 60;
        }
    }
}
