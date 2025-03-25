using System;
using Vintagestory.ServerMods;

namespace ContinentalWorld
{
    class MapLayerBlurSafe : MapLayerBase
    {
        private int range;
        private MapLayerBase parent;

        public MapLayerBlurSafe(long seed, MapLayerBase parent, int range) : base(seed)
        {
            if ((range & 1) == 0)
            {
                throw new InvalidOperationException("Range must be odd!");
            }

            this.parent = parent;
            this.range = range;
        }

        public override int[] GenLayer(int xCoord, int zCoord, int sizeX, int sizeZ)
        {
            int[] map = parent.GenLayer(xCoord, zCoord, sizeX, sizeZ);

            BoxBlurHorizontal(map, range, 0, 0, sizeX, sizeZ);
            BoxBlurVertical(map, range, 0, 0, sizeX, sizeZ);

            return map;
        }


        internal void BoxBlurHorizontal(int[] map, int range, int xStart, int yStart, int xEnd, int yEnd)
        {
            int[] pixels = map;

            int w = xEnd - xStart;
            int h = yEnd - yStart;

            int halfRange = range / 2;
            int index = yStart * w;
            int[] newColors = new int[w];

            for (int y = yStart; y < yEnd; y++)
            {
                int hits = 0;
                int a = 0;
                int r = 0;
                int g = 0;
                int b = 0;
                for (int x = xStart - halfRange; x < xEnd; x++)
                {
                    int oldPixel = x - halfRange - 1;
                    if (oldPixel >= xStart)
                    {
                        int col = (int)(pixels[index + oldPixel]);
                        if (col != 0)
                        {
                            a -= (col >> 24) & 0xff;
                            r -= (col >> 16) & 0xff;
                            g -= (col >> 8) & 0xff;
                            b -= col & 0xff;
                        }
                        hits--;
                    }

                    int newPixel = x + halfRange;
                    if (newPixel < xEnd)
                    {
                        int col = (int)(pixels[index + newPixel]);
                        if (col != 0)
                        {
                            a += (col >> 24) & 0xff;
                            r += (col >> 16) & 0xff;
                            g += (col >> 8) & 0xff;
                            b += col & 0xff;
                        }
                        hits++;
                    }

                    if (x >= xStart)
                    {
                        int color = (int)(
                            ((byte)(a / hits) << 24)
                            | ((byte)(r / hits) << 16)
                            | ((byte)(g / hits) << 8)
                            | ((byte)(b / hits))
                            );

                        newColors[x] = color;
                    }
                }

                for (int x = xStart; x < xEnd; x++)
                {
                    pixels[index + x] = newColors[x];
                }

                index += w;
            }
        }

        internal void BoxBlurVertical(int[] map, int range, int xStart, int yStart, int xEnd, int yEnd)
        {
            int[] pixels = map;

            int w = xEnd - xStart;
            int h = yEnd - yStart;

            int halfRange = range / 2;

            int[] newColors = new int[h];
            int oldPixelOffset = -(halfRange + 1) * w;
            int newPixelOffset = (halfRange) * w;

            for (int x = xStart; x < xEnd; x++)
            {
                int hits = 0;
                int a = 0;
                int r = 0;
                int g = 0;
                int b = 0;
                int index = yStart * w - halfRange * w + x;
                for (int y = yStart - halfRange; y < yEnd; y++)
                {
                    int oldPixel = y - halfRange - 1;
                    if (oldPixel >= yStart)
                    {
                        int col = (int)(pixels[index + oldPixelOffset]);
                        if (col != 0)
                        {
                            a -= (col >> 24) & 0xff;
                            r -= (col >> 16) & 0xff;
                            g -= (col >> 8) & 0xff;
                            b -= col & 0xff;
                        }
                        hits--;
                    }

                    int newPixel = y + halfRange;
                    if (newPixel < yEnd)
                    {
                        int col = (int)(pixels[index + newPixelOffset]);
                        if (col != 0)
                        {
                            a += (col >> 24) & 0xff;
                            r += (col >> 16) & 0xff;
                            g += (col >> 8) & 0xff;
                            b += col & 0xff;
                        }
                        hits++;
                    }

                    if (y >= yStart)
                    {
                        int color = (int)(
                            ((byte)(a / hits) << 24)
                            | ((byte)(r / hits) << 16)
                            | ((byte)(g / hits) << 8)
                            | ((byte)(b / hits))
                            );

                        newColors[y] = color;
                    }

                    index += w;
                }

                for (int y = yStart; y < yEnd; y++)
                {
                    pixels[y * w + x] = newColors[y];
                }
            }
        }


    }
}

