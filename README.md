# TGP Community Fixes

A lightweight, standalone bugfix mod for **The Genesis Project**, built using BepInEx 5 and Harmony.

This mod aims to fix gameplay bugs while preserving the game's original behavior wherever possible.

## Current Version

**v1.0.0 — Initial Release**

Two fixes included.

### Bugfix 0001 — Deployable Furniture Disappearing Between Floors

**Problem:** Certain deployable furniture objects could become invisible when moving between floors of a building, even though the objects themselves still existed.

**Fix:** Corrects the Unity layers assigned to furniture objects after the game's occupancy checks, restoring their intended visibility behavior.

### Bugfix 0002 — Out-of-Bounds Furniture Placement Crash

**Problem:** Placing or moving certain furniture objects, particularly staircases, near the outer boundaries of a building could cause a `NullReferenceException` in the game's quadtree system.

In some cases, this error could disrupt the network session or disconnect the player, even when playing alone.

**Fix:** Adjusts the initial spawn coordinates of floor furniture to ensure its full footprint fits within the supported building area.

This prevents the furniture from entering the quadtree in an invalid initial position. The game's existing placement validation continues to handle subsequent invalid moves.

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

**Do not install multiple copies of this mod.**

## Verifying Installation

After launching the game, open the BepInEx console or log file.

You should see a message similar to:

```text
Loading [TGP Community Fixes 1.0.0]
```

The displayed version will depend on the release build.

## Compatibility

- Designed to work independently of BenMods.
- Tested with BenMods installed alongside it, with the corresponding fixes removed from BenMods to avoid duplicate Harmony patches.
- Other mods that modify furniture placement, building occupancy checks, or furniture visibility may conflict.
- Compatibility with other game versions has not been verified.

## Multiplayer

Both fixes have been tested in multiplayer during development.

Client-side behavior and compatibility with mixed mod installations have not been exhaustively tested.

## Uninstallation

Delete the `TGPCommunityFixes` folder from `BepInEx/plugins`.

The mod does not add custom items or assets. It modifies game behavior at runtime through Harmony patches.

## Known Limitations

- Bugfix 0002 prevents invalid *initial* furniture placement; it does not expand the game's building area or make out-of-bounds placement valid.
- Rapid repeated placement inputs near building boundaries may occasionally result in a valid placement at the edge rather than an invalid placement being discarded.
- Additional bugs may exist outside the scope of these fixes.

## Reporting Bugs

When reporting a problem, please include:

- The Genesis Project version
- BepInEx version
- Installed mods
- Steps to reproduce the problem
- Relevant BepInEx console output or `LogOutput.log` entries

## Changelog

### v1.0.0

Initial release.

- **0001:** Fixed deployable furniture visibility when changing building floors.
- **0002:** Fixed a quadtree exception caused by out-of-bounds initial floor-furniture placement.

---

**TGP Community Fixes** is an unofficial community-made modification for The Genesis Project and is not affiliated with the game's developers.
