namespace TDNPGL.Core.Math
{
    public static class ScreenCalculations
    {
        public static double OptimalPixelSize(double width,double height)
        {
            return ((width + height) / 2) / (System.Math.PI * 100);
        }
    }
}
