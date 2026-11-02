
using HarmonyLib;
using UnityEngine;

namespace TGPCommunityFixes.Fixes
{
    [HarmonyPatch(typeof(Furniture), "CheckOccupied")]
    internal static class Bug001_FurnitureVisibility
    {
        [HarmonyPostfix]
        private static void Postfix(Furniture __instance)
        {
            if (__instance == null)
                return;

            Transform parent = __instance.transform.parent;

            if (parent == null)
                return;

            // The destination room determines visibility.
            int targetLayer = Visibility.Get(parent.gameObject) ? 0 : 8;

            // Repair only this furniture hierarchy.
            foreach (Transform child in
                __instance.GetComponentsInChildren<Transform>(true))
            {
                if (child.CompareTag("Fixed Layer"))
                    continue;

                child.gameObject.layer = targetLayer;
            }
        }
    }
}
