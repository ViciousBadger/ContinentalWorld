using System.Collections.Generic;
using Vintagestory.ServerMods;

namespace ContinentalWorld
{
    static class ContinentalWorld
    {
        static void Main(string[] args)
        {
            long seed = 12312312323;
            var map = new MapLayerContinental(seed, 1.0f, 0.3f, new List<XZ>());
            var blur = new MapLayerBlurImproved(seed, map, 8);

            var debug = new NoiseDebug(1024, 1024);

            var layer = blur.GenLayer(0, 0, 1024, 1024);

            for (var x = 0; x < 1024; x++)
            {
                for (var z = 0; z < 1024; z++)
                {
                    debug.SetPixel(x, z, new SkiaSharp.SKColor((uint)layer[z * 1024 + x]));
                }
            }

            debug.SaveToFile("test.bmp");
        }
    }
}
