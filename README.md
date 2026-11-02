# TGP Community Fixes

A lightweight, standalone bugfix mod for **The Genesis Project**, built using BepInEx 5 and Harmony.

This mod aims to fix gameplay bugs while preserving the game's original behavior wherever possible.

## Current Version

**v1.1.0 — Load Session Improvements**

Three fixes included.

### Bugfix 0001 — Deployable Furniture Disappearing Between Floors

**Problem:** Certain deployable furniture objects could become invisible when moving between floors of a building, even though the objects themselves still existed.

**Fix:** Corrects the Unity layers assigned to furniture objects after the game's occupancy checks, restoring their intended visibility behavior.

### Bugfix 0002 — Out-of-Bounds Furniture Placement Crash

**Problem:** Placing or moving certain furniture objects, particularly staircases, near the outer boundaries of a building could cause a `NullReferenceException` in the game's quadtree system.

In some cases, this error could disrupt the network session or disconnect the player, even when playing alone.

**Fix:** Adjusts the initial spawn coordinates of floor furniture to ensure its full footprint fits within the supported building area.

This prevents the furniture from entering the quadtree in an invalid initial position. The game's existing placement validation continues to handle subsequent invalid moves.

### Bugfix 0003 — Load Session Dropdown Usability

**Problem:** The Load Session dropdown displayed only a small number of entries at once and scrolled so slowly that navigating a large list of saved sessions was difficult.

**Fix:** Increases mouse-wheel scroll sensitivity and expands the dropdown to display more entries at once, up to a configurable limit.

The dropdown automatically shrinks when fewer entries are available. Both the scroll sensitivity and maximum number of visible rows can be adjusted through BepInEx configuration.

## Requirements

- The Genesis Project — tested with **v0.6.0-2 (Build 22962023)**
- **BepInEx 5** — tested with v5.4.23.6

No other mods are required.

## Installation

1. Install BepInEx 5 for The Genesis Project, if you haven't already.
2. Download and extract the TGP Community Fixes release.
3. Place the `TGPCommunityFixes` folder inside your game's `BepInEx/plugins` directory.
4. Launch the game.

Your installation should look like this:

```text
The Genesis Project/
└── BepInEx/
    └── plugins/
        └── TGPCommunityFixes/
            └── TGPCommunityFixes.dll
```

## Verifying Installation

After launching the game, open the BepInEx console or log file.

You should see a message similar to:

```text
Loading [TGP Community Fixes 1.1.0]
```

The displayed version will depend on the release build.

## Configuration

Bugfix 0003 settings are available in `BepInEx/config/vulpinecaptain.tgp.communityfixes.cfg` after launching the game.

- `ScrollSensitivity` — defaults to `25`; supported range: `1` to `200`.
- `MaxVisibleRows` — defaults to `12`; supported range: `2` to `20`.

The dropdown grows with the number of available entries, up to the configured row limit and a maximum height of 400 UI units. Changes to the configuration take effect after restarting the game.

## Compatibility

- Other mods that modify furniture placement, building occupancy checks, or furniture visibility may conflict.
- Compatibility with other game versions has not been verified.

## Multiplayer

Bugfixes 0001 and 0002 have been tested in multiplayer during development.

Client-side behavior and compatibility with mixed mod installations have not been exhaustively tested.

## Uninstallation

Delete the `TGPCommunityFixes` folder from `BepInEx/plugins`.

The mod does not add custom items or assets. It modifies game behavior at runtime through Harmony patches.

## Known Limitations

- Bugfix 0002 prevents invalid *initial* furniture placement; it does not expand the game's building area or make out-of-bounds placement valid.
- Rapid repeated placement inputs near building boundaries may occasionally result in a valid placement at the edge rather than an invalid placement being discarded.

## Reporting Bugs

When reporting a problem, please include:

- The Genesis Project version
- BepInEx version
- Installed mods
- Steps to reproduce the problem
- Relevant BepInEx console output or `LogOutput.log` entries

## Changelog

### v1.1.0

Load Session improvements.

- **0003:** Improved Load Session dropdown scrolling and automatically adjusted its height for larger save lists. Added configurable scroll sensitivity and maximum visible rows.

### v1.0.0

Initial release.

- **0001:** Fixed deployable furniture visibility when changing building floors.
- **0002:** Fixed a quadtree exception caused by out-of-bounds initial floor-furniture placement.

---

**TGP Community Fixes** is an unofficial community-made modification for The Genesis Project and is not affiliated with the game's developers.
