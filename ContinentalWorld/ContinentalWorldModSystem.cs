using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;
using HarmonyLib;

namespace ContinentalWorld;

public class ContinentalWorldModSystem : ModSystem
{
    private Harmony patcher;

    // public static ILogger Logger;

    // Called on server and client
    public override void Start(ICoreAPI api)
    {
        // Logger = Mod.Logger;
        if (!Harmony.HasAnyPatches(Mod.Info.ModID))
        {
            patcher = new Harmony(Mod.Info.ModID);
            patcher.PatchCategory(Mod.Info.ModID);
        }
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
    }

    public override void Dispose()
    {
        patcher?.UnpatchAll(Mod.Info.ModID);
    }
}
