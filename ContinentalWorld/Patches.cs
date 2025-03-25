using System.Collections.Generic;
using HarmonyLib;
using Vintagestory.API.Server;
using Vintagestory.ServerMods;

namespace ContinentalWorld
{
    [HarmonyPatchCategory("continentalworld")]
    internal static class ContinentalPatches
    {
        [HarmonyPostfix()]
        [HarmonyPatch(typeof(GenMaps), "GetOceanMapGen")]
        public static void GetOceanMapGen(ref MapLayerBase __result, long seed, float landcover, int oceanMapScale, float oceanScaleMul, List<XZ> requireLandAt, bool requiresSpawnOffset)
        {
            var map = new MapLayerContinental(seed, oceanMapScale * oceanScaleMul, landcover, requireLandAt);
            var blur = new MapLayerBlurSafe(seed, map, 5);
            __result = blur;
        }

        [HarmonyPrefix()]
        [HarmonyPatch(typeof(GenMaps), "ForceRandomLandArea")]
        public static bool ForceRandomLandArea(int positionX, int positionZ, int radius, ref List<XZ> ___requireLandAt)
        {
            var minx = (positionX - radius);
            var minz = (positionZ - radius);
            var maxx = (positionX + radius);
            var maxz = (positionZ + radius);

            for (int x = minx; x <= maxx; x++)
            {
                for (int z = minz; z < maxz; z++)
                {
                    ___requireLandAt.Add(new XZ(x, z));
                }
            }

            return false;

            // var regionSize = ___sapi.WorldManager.RegionSize;
            // var minx = (positionX - radius) * ___noiseSizeOcean / regionSize;
            // var minz = (positionZ - radius) * ___noiseSizeOcean / regionSize;
            // var maxx = (positionX + radius) * ___noiseSizeOcean / regionSize;
            // var maxz = (positionZ + radius) * ___noiseSizeOcean / regionSize;
            //
            // // randomly grow the square into a more natural looking shape if all surroundings are ocean
            // var lcgRandom = new LCGRandom(sapi.World.Seed);
            // lcgRandom.InitPositionSeed(positionX, positionZ);
            // var naturalShape = new NaturalShape(lcgRandom);
            // var sizeX = maxx - minx;
            // var sizeZ = maxz - minz;
            // naturalShape.InitSquare(sizeX, sizeZ);
            // naturalShape.Grow(sizeX * sizeZ);
            //
            // foreach (var pos in naturalShape.GetPositions())
            // {
            //     requireLandAt.Add(new XZ(minx + pos.X, minz + pos.Y));
            // }

        }
    }
}
