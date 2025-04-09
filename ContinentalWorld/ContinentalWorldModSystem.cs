using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;
using HarmonyLib;
using System;

namespace ContinentalWorld;

public class ContinentalWorldModSystem : ModSystem
{
    private Harmony patcher;

    // public static ILogger Logger;
    //
    // Static so it can be accessed by the patch of GenMaps..
    public static ContinentalConfig Config;

    // Called on server and client
    public override void Start(ICoreAPI api)
    {
        try
        {
            Config = api.LoadModConfig<ContinentalConfig>("continental-world.json");
            if (Config == null)
            {
                Config = new ContinentalConfig();
            }
            api.StoreModConfig<ContinentalConfig>(Config, "continental-world.json");
        }
        catch (Exception e)
        {
            //Couldn't load the mod config... Create a new one with default settings, but don't save it.
            Mod.Logger.Error("Could not load config! Loading default settings instead. If you delete the config file, this error will go away magically, but your custom settings will also be lost.");
            Mod.Logger.Error(e);
            Config = new ContinentalConfig();
        }


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
