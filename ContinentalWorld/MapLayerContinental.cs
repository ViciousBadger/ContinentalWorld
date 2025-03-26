
using System.Collections.Generic;
using Vintagestory.ServerMods;
using Vintagestory.API.MathTools;

namespace ContinentalWorld
{
    public class MapLayerContinental : MapLayerBase
    {
        // private long seed;
        // private float scale;
        private float landCoverRate;
        private CustomWorley noiseSource;
        private NormalizedSimplexNoise warpNoiseX;
        private NormalizedSimplexNoise warpNoiseZ;

        private float warpPower;

        public MapLayerContinental(long seed, float scale, float landCoverRate, List<XZ> requireLandAt) : base(seed)
        {
            // this.seed = seed;
            // this.scale = scale;
            this.landCoverRate = landCoverRate;

            this.noiseSource = new CustomWorley(seed, scale * 2.0f, requireLandAt);

            int warpOctaves = 5;
            float warpScale = 4.0f * scale;
            float warpPersistence = 0.75f;
            this.warpPower = scale * 2.0f;

            this.warpNoiseX = NormalizedSimplexNoise.FromDefaultOctaves(warpOctaves, 1 / warpScale, warpPersistence, seed + 628903);
            this.warpNoiseZ = NormalizedSimplexNoise.FromDefaultOctaves(warpOctaves, 1 / warpScale, warpPersistence, seed + 467216);
        }

        public int NoiseAt(int x, int z)
        {
            var xOffset = x + this.warpNoiseX.Noise(x, z) * this.warpPower;
            var zOffset = z + this.warpNoiseZ.Noise(x, z) * this.warpPower;

            var rawNoise = this.noiseSource.GetPointValue((float)xOffset, (float)zOffset);
            return rawNoise > (-1.0f + this.landCoverRate * 2.0f) ? 255 : 0;
            //return rawNoise > -1.0f ? 255 : 0;
        }

        public override int[] GenLayer(int xCoord, int zCoord, int sizeX, int sizeZ)
        {
            var result = new int[sizeX * sizeZ];
            for (var x = 0; x < sizeX; x++)
            {
                for (var z = 0; z < sizeZ; z++)
                {
                    var nx = xCoord + x;
                    var nz = zCoord + z;

                    result[z * sizeX + x] = NoiseAt(nx, nz);
                }
            }
            return result;
        }
    }
}
