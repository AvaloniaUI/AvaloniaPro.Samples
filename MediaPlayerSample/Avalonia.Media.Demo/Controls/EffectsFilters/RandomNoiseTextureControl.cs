using System;
using Avalonia.Controls;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace Avalonia.Media.Demo.Controls.EffectsFilters
{
    public class RandomNoiseTextureControl : Control
    {
        private const int DesiredWidth = 256;
        private const int DesiredHeight = 256;

        private static readonly Random Rng = new();
        private readonly NoiseTextureCustomDrawOp _renderLogic;

        public RandomNoiseTextureControl()
        {
            _renderLogic = new(Opacity);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            if (change.Property == OpacityProperty)
            {
                _renderLogic.Opacity = Opacity;
                InvalidateVisual();
            }

            base.OnPropertyChanged(change);
        }

        private class NoiseTextureCustomDrawOp(double opacity) : ICustomDrawOperation
        {
            private static SKImage CreateNoiseImage(int width, int height)
            {
                var imageInfo = new SKImageInfo(width, height, SKColorType.Rgba8888);
                using var surface = SKSurface.Create(imageInfo);
                var canvas = surface.Canvas;

                using var paint = new SKPaint();

                // Draw pixels directly
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        var curVal = (byte)(Rng.NextDouble() * 0xff);
                        paint.Color = new SKColor(curVal, curVal, curVal);
                        canvas.DrawPoint(x, y, paint);
                    }
                }

                return surface.Snapshot();
            }

            private readonly SKImage _texture = CreateNoiseImage(DesiredWidth, DesiredHeight);

            public Rect Bounds { get; set; }

            public double Opacity
            {
                set => opacity = value;
            }

            public void Dispose()
            {
            }

            public bool Equals(ICustomDrawOperation? other) => other == this;

            public bool HitTest(Point p)
            {
                return false;
            }

            public void Render(ImmediateDrawingContext context)
            {
                var skia = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
                if (skia is null)
                    throw new InvalidOperationException();

                using var lease = skia.Lease();
                var canvas = lease.SkCanvas;

                using var shader = _texture.ToShader(SKShaderTileMode.Repeat, SKShaderTileMode.Repeat);

                // Draw the tiled noise with overlay blend
                using var blendPaint = new SKPaint();
                blendPaint.Shader = shader;
                blendPaint.BlendMode = SKBlendMode.Overlay;
                var op = (byte)(opacity * 0xff);

                blendPaint.Color = new SKColor(0xff, 0xff, 0xff, op);

                canvas.DrawRect(new SKRect(0, 0, (float)Bounds.Width, (float)Bounds.Height), blendPaint);
            }
        }

        public override void Render(DrawingContext context)
        {
            // Probably Redundant but anyway.
            if (!IsEffectivelyVisible || Opacity == 0 || !IsVisible)
                return;
            _renderLogic.Bounds = Bounds;
            context.Custom(_renderLogic);
            base.Render(context);
        }
    }
}
