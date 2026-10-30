using BepInEx;
using HarmonyLib;

namespace TGPFixIsolationTest
{
    [BepInPlugin(
        "ben.tgp.communityfixes",
        "TGP Community Fixes",
        "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("TGP Community Fixes ONLINE!");

            Harmony harmony =
                new Harmony("ben.tgp.communityfixes");

            harmony.PatchAll();

            Logger.LogInfo("Fix isolation patches applied.");
        }
    }
}