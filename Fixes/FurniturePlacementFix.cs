
using System;
using System.Reflection;
using HarmonyLib;
using Mirror;
using UnityEngine;

namespace BenMods.Fixes
{
    // Bug 0002:
    // Prevent furniture from initially spawning with a footprint
    // outside the building's spatial index.
    //
    // An out-of-bounds initial spawn can create a quadtree entry
    // whose Owner is null, causing subsequent movement to throw
    // inside QuadTreeCommon.Remove().

    [HarmonyPatch]
    internal static class FurniturePlacementFix
    {
        private const int MinCoord = -30;
        private const int MaxCoord = 30;
        private const string LogPrefix = "[BenMods][Bug0002] ";

        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(
                typeof(Furniture),
                "Make",
                new Type[]
                {
                    typeof(string),
                    typeof(Building),
                    typeof(Vector3Int),
                    typeof(Orientation),
                    typeof(NetworkConnection)
                }
            );
        }

        [HarmonyPrefix]
        private static void Prefix(
            string name,
            ref Vector3Int coords,
            Orientation orientation)
        {
            if (string.IsNullOrEmpty(name) ||
                name.StartsWith("Prefabs/"))
                return;

            try
            {
                Furniture prefab =
                    HouseManager.Instance.GetFurniturePrefab(name);

                if (!(prefab is FloorFurniture))
                    return;

                Vector2Int size = prefab.Size;

                // Preserve the orientation handling from the
                // successfully tested fix.
                if (orientation == Orientation.EAST ||
                    orientation == Orientation.WEST)
                {
                    size = new Vector2Int(size.y, size.x);
                }

                if (size.x <= 0 ||
                    size.y <= 0 ||
                    size.x > 60 ||
                    size.y > 60)
                {
                    Debug.LogWarning(
                        LogPrefix +
                        $"Unsupported footprint for {name}: {size}"
                    );
                    return;
                }

                Vector3Int original = coords;

                coords.x = Mathf.Clamp(
                    coords.x,
                    MinCoord,
                    MaxCoord - size.x
                );

                coords.z = Mathf.Clamp(
                    coords.z,
                    MinCoord,
                    MaxCoord - size.y
                );

                if (coords != original)
                {
                    Debug.Log(
                        LogPrefix +
                        $"Adjusted initial spawn: {name} " +
                        $"{original} -> {coords} " +
                        $"(size={size})"
                    );
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(
                    LogPrefix +
                    "Initial spawn correction failed: " + ex
                );
            }
        }
    }
}
