
using System.Collections.Generic;
using Vintagestory.ServerMods;

namespace ContinentalWorld
{
    public class MapLayerContinental : MapLayerBase
    {
        private float landCoverRate;
        private CustomWorley noiseSource;
        private List<XZ> requireLandAt;
        private int lastRequiredLandCount;

        public MapLayerContinental(long seed, float scale, float landCoverRate, List<XZ> requireLandAt, ContinentalConfig config) : base(seed)
        {
            this.landCoverRate = landCoverRate;
            this.noiseSource = new CustomWorley(seed, scale, config);
            this.requireLandAt = requireLandAt;
            this.lastRequiredLandCount = 0;
            RefreshRequiredLand();
        }

        public int NoiseAt(int x, int z)
        {
            var rawNoise = this.noiseSource.GetPointValue(x, z);
            return rawNoise > (-1.0f + this.landCoverRate * 2.0f) ? 255 : 0;
        }

        private void RefreshRequiredLand()
        {
            // The requireLandAt is mutated after map layers are constructed, so
            // we need to look for newly added required cooridinates in
            // order to properly generate land at all story locations.
            if (this.requireLandAt.Count > lastRequiredLandCount)
            {
                for (int i = lastRequiredLandCount; i < this.requireLandAt.Count; i++)
                {
                    this.noiseSource.RequireLandAt(this.requireLandAt[i]);
                }
                lastRequiredLandCount = this.requireLandAt.Count;
            }
        }

        public override int[] GenLayer(int xCoord, int zCoord, int sizeX, int sizeZ)
        {
            RefreshRequiredLand();

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
