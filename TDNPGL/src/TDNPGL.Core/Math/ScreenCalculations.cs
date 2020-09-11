namespace TDNPGL.Core.Math
{
    public static class ScreenCalculations
    {
        public static double CalculatePixelSize(double width,double height)
        {
            return ((width + height) / 2) / (System.Math.PI * 100);
        }
    }
}
