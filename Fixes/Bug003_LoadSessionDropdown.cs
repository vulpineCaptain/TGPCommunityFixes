
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using TGPFixIsolationTest;

namespace TGPCommunityFixes.Fixes
{
    [HarmonyPatch(
        typeof(LobbySettingsComponent),
        "UpdateChangeability")]
    internal static class Bug003_LoadSessionDropdown
    {
        [HarmonyPostfix]
        private static void Postfix(
            LobbySettingsComponent __instance)
        {
            Dropdown dropdown = __instance._loadFromFile;

            if (dropdown == null || dropdown.template == null)
                return;

            ScrollRect scrollRect =
                dropdown.template
                    .GetComponentInChildren<ScrollRect>(true);

            if (scrollRect == null)
                return;

            // Read the user-configured scrolling speed.
            scrollRect.scrollSensitivity = Mathf.Clamp(
                Plugin.ScrollSensitivity.Value,
                1f,
                200f
            );

            // Find the height of an individual dropdown item.
            Toggle item =
                dropdown.template
                    .GetComponentInChildren<Toggle>(true);

            float rowHeight = 20f;

            if (item != null)
            {
                RectTransform itemRect =
                    item.GetComponent<RectTransform>();

                if (itemRect != null && itemRect.rect.height > 0f)
                    rowHeight = itemRect.rect.height;
            }

            // Respect the configured maximum number of rows.
            int maxRows = Mathf.Clamp(
                Plugin.MaxVisibleRows.Value,
                2,
                20
            );

            int visibleRows = Mathf.Min(
                dropdown.options.Count,
                maxRows
            );

            // Original template dimensions:
            // 80px high with a 72px viewport.
            const float padding = 8f;
            const float minimumHeight = 80f;
            const float maximumHeight = 400f;

            float desiredHeight =
                visibleRows * rowHeight + padding;

            desiredHeight = Mathf.Clamp(
                desiredHeight,
                minimumHeight,
                maximumHeight
            );

            // Resize the dropdown's template.
            dropdown.template.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                desiredHeight
            );
        }
    }
}
