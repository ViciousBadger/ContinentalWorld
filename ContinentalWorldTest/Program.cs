using System.Collections.Generic;
using ContinentalWorld;
using Vintagestory.ServerMods;
using SkiaSharp;

namespace ContinentalWorldTest
{
    static class ContinentalWorld
    {
        static void Main(string[] args)
        {
            float Lerp(float a, float b, float f)
            {
                return a * (1.0f - f) + (b * f);
            }

            long seed = 1235;
            var forceLand = new List<XZ>();
            forceLand.Add(new XZ(512, 512));
            // forceLand.Add(new XZ(256, 256));
            // forceLand.Add(new XZ(768, 256));
            // forceLand.Add(new XZ(256, 768));
            // forceLand.Add(new XZ(768, 768));

            var map = new MapLayerContinental(seed, TerraGenConfig.oceanMapScale * 1.0f, 0.3f, forceLand);
            var blur = new MapLayerBlurImproved(seed, map, 8);

            // var blur = GenMaps.GetOceanMapGen(seed, 0.3f, TerraGenConfig.oceanMapScale, 1.0f, forceLand, false);

            var debug = new NoiseDebug(1024, 1024);

            var layer = blur.GenLayer(0, 0, 1024, 1024);

            for (var x = 0; x < 1024; x++)
            {
                for (var z = 0; z < 1024; z++)
                {
                    byte sample = (byte)layer[z * 1024 + x];
                    float oceanValue = sample / 255f;
                    SKColor land = new SKColor(153, 122, 78);
                    SKColor ocean = new SKColor(193, 183, 130);

                    SKColor blended = new SKColor(
                        (byte)Lerp(land.Red, ocean.Red, oceanValue),
                        (byte)Lerp(land.Green, ocean.Green, oceanValue),
                        (byte)Lerp(land.Blue, ocean.Blue, oceanValue)
                    );

                    debug.SetPixel(x, z, blended);
                }
            }

            // foreach (var land in forceLand)
            // {
            //     debug.SetPixel(land.X, land.Z, new SkiaSharp.SKColor(255, 0, 0));
            // }


            debug.SaveToFile("test.bmp");
        }
    }
}
