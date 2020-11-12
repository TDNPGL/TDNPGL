using NUnit.Framework;
using SkiaSharp;
using TDNPGL.Core.Math;
using TDNPGL.Native.Win32.Extensions;

namespace TDNPGL.Tests.Native
{
    public class AABBTests
    {
        static AABB aabb = new AABB(-1, -1, 1, 1);
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void AABBWithTrue()
        {
            bool result = aabb.IsPointOverNative((Vec2f)new SKPoint(0,0));
            bool expected = true;

            Assert.AreEqual(result, expected);
        }
        [Test]
        public void AABBWithFalse()
        {
            bool result = aabb.IsPointOverNative((Vec2f)new SKPoint(2, 2));
            bool expected = false;

            Assert.AreEqual(result, expected);
        }
        [Test]
        public void AABBManagedWithFalse()
        {
            bool result = aabb.IsPointOver((Vec2f)new SKPoint(2, 2));
            bool expected = false;

            Assert.AreEqual(result, expected);
        }
        [Test]
        public void AABBManagedWithTrue()
        {
            bool result = aabb.IsPointOverNative((Vec2f)new SKPoint(0, 0));
            bool expected = true;

            Assert.AreEqual(result, expected);
        }
    }
}