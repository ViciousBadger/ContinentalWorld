using System.Collections.Generic;
using ContinentalWorld;
using Vintagestory.ServerMods;

namespace ContinentalWorldTest
{
    static class ContinentalWorld
    {
        static void Main(string[] args)
        {
            long seed = 2358917986;
            var forceLand = new List<XZ>();
            forceLand.Add(new XZ(512, 512));
            forceLand.Add(new XZ(256, 256));
            forceLand.Add(new XZ(768, 256));
            forceLand.Add(new XZ(256, 768));
            forceLand.Add(new XZ(768, 768));

            var map = new MapLayerContinental(seed, TerraGenConfig.oceanMapScale * 1.0f, 0.3f, forceLand);
            var blur = new MapLayerBlurImproved(seed, map, 8);

            var debug = new NoiseDebug(1024, 1024);

            var layer = blur.GenLayer(0, 0, 1024, 1024);

            for (var x = 0; x < 1024; x++)
            {
                for (var z = 0; z < 1024; z++)
                {
                    var sample = (byte)layer[z * 1024 + x];
                    var color = new SkiaSharp.SKColor(sample, sample, sample);
                    debug.SetPixel(x, z, color);
                }
            }

            foreach (var land in forceLand)
            {
                debug.SetPixel(land.X, land.Z, new SkiaSharp.SKColor(255, 0, 0));
            }


            debug.SaveToFile("test.bmp");
        }
    }
}
