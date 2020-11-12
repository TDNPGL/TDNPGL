using OpenTK.Graphics.OpenGL;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDNPGL.Views.OpenGL.Utils
{
    public static class BitmapUtils
    {
        public static byte[] GetPixels(SKBitmap bitmap)
        {
            //Get an array of the pixels, in ImageSharp's internal format.
            SKColor[] tempPixels = bitmap.Pixels;

            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            List<byte> pixels = new List<byte>();

            foreach (SKColor p in tempPixels)
            {
                pixels.Add(p.Red);
                pixels.Add(p.Green);
                pixels.Add(p.Blue);
                pixels.Add(p.Alpha);
            }
            return pixels.ToArray();
        }
        public static int LoadTexture(SKBitmap bitmap)
        {
            int id = 0;
            id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bitmap.Handle);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            return id;
        }
    }
}
