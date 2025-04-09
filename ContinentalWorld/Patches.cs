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
            // Clone the config to avoid the object reference keeping things in memory.
            var config = ContinentalWorldModSystem.Config.Clone();
            //
            // Hijack the ocean map generator
            var map = new MapLayerContinental(seed, oceanMapScale * oceanScaleMul, landcover, requireLandAt, config);
            var blur = new MapLayerBlurImproved(seed, map, 8);
            __result = blur;
        }
    }
}
