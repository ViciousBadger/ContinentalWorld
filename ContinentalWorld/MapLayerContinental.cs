
using System.Collections.Generic;
using Vintagestory.ServerMods;

namespace ContinentalWorld
{
    public class MapLayerContinental : MapLayerBase
    {
        private float landCoverRate;
        private CustomWorley noiseSource;

        public MapLayerContinental(long seed, float scale, float landCoverRate, List<XZ> requireLandAt) : base(seed)
        {
            this.landCoverRate = landCoverRate;
            this.noiseSource = new CustomWorley(seed, scale, requireLandAt);
        }

        public int NoiseAt(int x, int z)
        {
            var rawNoise = this.noiseSource.GetPointValue(x, z);
            return rawNoise > (-1.0f + this.landCoverRate * 2.0f) ? 255 : 0;
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
