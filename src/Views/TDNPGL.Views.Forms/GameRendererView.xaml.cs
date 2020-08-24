using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Graphics.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDNPGL.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameRendererView : SKGLView,IGameRenderer
    {
        public double width => CanvasSize.Width;
        public double height => CanvasSize.Height;

        public double PixelSize => ((width + height) / 2) / (Math.PI * 100);
        public GameRendererView()
        {
            InitializeComponent();

        }
        public void InitGame(Assembly assembly, string GameName)=>
            TDNPGL.Core.Game.Init(this, assembly, GameName, true);
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint => InitGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

        public SKBitmap CurrentGameBitmap { get; set; }

        public override IDispatcher Dispatcher => base.Dispatcher;

        public void Dispose()
        {
            
        }

        public void DrawBitmap(SKBitmap bitmap)
        {
            CurrentGameBitmap = bitmap;
            InvalidateSurface();
        }

        private void SKGLView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintGLSurfaceEventArgs e)
        {
            try
            {
                e.Surface.Canvas.DrawBitmap(CurrentGameBitmap, new SKRect(0, 0, CanvasSize.Width, CanvasSize.Height));
            }
            catch { 
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }

        protected override void OnRequestedThemeChanged(OSAppTheme newValue)
        {
            base.OnRequestedThemeChanged(newValue);
        }

        protected override void OnTabIndexPropertyChanged(int oldValue, int newValue)
        {
            base.OnTabIndexPropertyChanged(oldValue, newValue);
        }

        protected override int TabIndexDefaultValueCreator()
        {
            return base.TabIndexDefaultValueCreator();
        }

        protected override void OnTabStopPropertyChanged(bool oldValue, bool newValue)
        {
            base.OnTabStopPropertyChanged(oldValue, newValue);
        }

        protected override bool TabStopDefaultValueCreator()
        {
            return base.TabStopDefaultValueCreator();
        }

        public override SizeRequest GetSizeRequest(double widthConstraint, double heightConstraint)
        {
            return base.GetSizeRequest(widthConstraint, heightConstraint);
        }

        protected override void InvalidateMeasure()
        {
            base.InvalidateMeasure();
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
        }

        protected override void OnChildRemoved(Element child)
        {
            base.OnChildRemoved(child);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            return base.OnSizeRequest(widthConstraint, heightConstraint);
        }

        protected override void ChangeVisualState()
        {
            base.ChangeVisualState();
        }

        public override IList<GestureElement> GetChildElements(Point point)
        {
            return base.GetChildElements(point);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            base.OnTouch(e);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return base.OnMeasure(widthConstraint, heightConstraint);
        }
    }
}