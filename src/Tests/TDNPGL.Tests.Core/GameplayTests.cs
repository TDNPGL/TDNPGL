using NUnit.Framework;
using SkiaSharp;
using System;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Graphics;
using TDNPGL.Core.Graphics.Renderers;

namespace TDNPGL.Tests.Core
{
    public class GameplayTests
    {
        [SetUp]
        public void Setup()
        {
            Exceptions.NoThrowExceptions = false;
        }

        [Test]
        public void LevelUpdaterTest()
        {
            CreateTestLevel().Updater.Stop();
            Assert.Pass();
        }
        [Test]
        public void LevelRenderTest()
        {
            Level lvl=CreateTestLevel();
            var renderer = new BaseLevelRenderer();
            var bitmap=renderer.Render(lvl, new BaseGameRenderer(800,600));
            Assert.Pass();
        }
        private Level CreateTestLevel()
        {
            Level level = Level.Empty;

            GameObject @object = new GameObject(level, new Sprite(new SkiaSharp.SKBitmap()));

            level.AddObject(@object);
            level.BeginUpdate();

            return level;
        }
    }
}