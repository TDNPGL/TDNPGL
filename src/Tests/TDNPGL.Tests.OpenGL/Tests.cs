using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkiaSharp;
using TDNPGL.Views.OpenGL;

namespace TDNPGL.Tests.OpenGL
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test()
        {
            SkiaSharp.SKBitmap sKBitmap = new SkiaSharp.SKBitmap(800,600);
            sKBitmap.Erase(SKColors.Red);
            GameWindow client = new GameWindow(sKBitmap);
            client.DrawBitmap(sKBitmap);
            client.Run();
            Assert.IsTrue(true);
        }
    }
}
