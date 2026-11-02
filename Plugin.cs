
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace TGPFixIsolationTest
{
    [BepInPlugin(
        "vulpinecaptain.tgp.communityfixes",
        "TGP Community Fixes",
        "1.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ConfigEntry<float> ScrollSensitivity;
        internal static ConfigEntry<int> MaxVisibleRows;

        private void Awake()
        {
            Logger.LogInfo("TGP Community Fixes ONLINE!");

            ScrollSensitivity = Config.Bind(
                "BUG003 - Load Session Dropdown",
                "ScrollSensitivity",
                25f,
                new ConfigDescription(
                    "Mouse wheel scrolling speed.",
                    new AcceptableValueRange<float>(1f, 200f)
                )
            );

            MaxVisibleRows = Config.Bind(
                "BUG003 - Load Session Dropdown",
                "MaxVisibleRows",
                12,
                new ConfigDescription(
                    "Maximum number of visible session entries.",
                    new AcceptableValueRange<int>(2, 20)
                )
            );

            Harmony harmony =
                new Harmony("vulpinecaptain.tgp.communityfixes");

            harmony.PatchAll();

            Logger.LogInfo("TGP Community Fixes Applied.");
        }
    }
}
