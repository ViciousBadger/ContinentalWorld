using System.Collections.Generic;
using HarmonyLib;
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
    }
}
