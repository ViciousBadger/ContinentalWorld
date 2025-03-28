using Vintagestory.ServerMods;

namespace ContinentalWorld
{
    public class MapLayerBlurImproved : MapLayerBase
    {
        // Author's note: "Improved" is a bit hyperbole. I had to re-implement the blur algorithm because
        // 1) The original class in VS Surival is marked private and cannot be used in mods
        // 2) The original algorithm uses unsafe code, which I would like to avoid as I am raw-pointer-phobic :)
        private int iterations;
        private MapLayerBase parent;

        public MapLayerBlurImproved(long seed, MapLayerBase parent, int iterations) : base(seed)
        {
            this.parent = parent;
            this.iterations = iterations;
        }

        public override int[] GenLayer(int xCoord, int zCoord, int sizeX, int sizeZ)
        {
            int[] origMap = parent.GenLayer(xCoord, zCoord, sizeX, sizeZ);
            var newMap = new int[origMap.Length];

            for (int iter = 0; iter < this.iterations; iter++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    for (int z = 0; z < sizeX; z++)
                    {
                        var here = origMap[z * sizeX + x];
                        var north = z == 0 ? here : origMap[(z - 1) * sizeX + x];
                        var south = z == sizeZ - 1 ? here : origMap[(z + 1) * sizeX + x];
                        var west = x == 0 ? here : origMap[z * sizeX + (x - 1)];
                        var east = x == sizeX - 1 ? here : origMap[z * sizeX + (x + 1)];

                        var avg = here;
                        avg += north;
                        avg += south;
                        avg += west;
                        avg += east;
                        avg /= 5;

                        newMap[z * sizeX + x] = avg;
                    }
                }

                for (int i = 0; i < origMap.Length; i++)
                {
                    origMap[i] = newMap[i];
                }
            }


            return newMap;
        }
    }
}

