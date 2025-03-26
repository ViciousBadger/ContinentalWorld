using System;
using SkiaSharp;

namespace ContinentalWorldTest
{
    public class NoiseDebug : IDisposable
    {
        private SKBitmap bitmap;
        private SKCanvas canvas;

        public NoiseDebug(int width, int height)
        {
            bitmap = new SKBitmap(width, height);
            canvas = new SKCanvas(bitmap);

            canvas.Clear(new SKColor(100, 149, 237));
        }

        public void SetPixel(int x, int y, SKColor color)
        {
            if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
            {
                bitmap.SetPixel(x, y, color);
            }
        }

        public void SaveToFile(string filePath)
        {
            using (SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100))
            using (System.IO.FileStream stream = System.IO.File.OpenWrite(filePath))
            {
                data.SaveTo(stream);
            }
        }

        public void Dispose()
        {
            canvas?.Dispose();
            bitmap?.Dispose();
        }
    }
}
