
# ExoPioneer – The Promise of Kallarai (Unity Starter MVP)

This is a **Unity starter pack** for your game. Import the `Assets` folder into a new Unity 2021.3+ (LTS) or newer project.

## What’s included
- Core game loop: survival stats (Oxygen, Shield), day/night, simple weather.
- Player controller with mobile joystick/buttons support.
- Weapon system (projectile), enemy spawner + basic AI.
- Pets framework with abilities.
- Crafting system with JSON recipe data.
- Simple HUD, inventory UI hooks.
- Multiplayer stub (Mirror/Photon selectable) – optional.

## Quick Start
1. Open Unity → New 3D URP (or 3D) project.
2. Copy this `Assets` folder into your project directory.
3. In *SampleScene* (or new scene):
   - Create empty `GameManager` object → add **GameManager**, **WeatherSystem**, **DayNightCycle**.
   - Create `Player` (Capsule + CharacterController) → add **PlayerController**, **MobileInput**.
   - Create `HUD` Canvas → add **HUDController** (see prefabs TODO).
   - Add an `EnemySpawner` empty in the scene.
4. Press Play. You should see survival HUD values changing; tap screen (or mouse) to shoot.
5. Build Settings → Android → Switch Platform → Player Settings (company/game name, icons) → Build APK.

## Controls
- Move: WASD / virtual joystick.
- Look: Mouse / right side drag.
- Fire: Left Click / Fire button.
- Jump: Space.
- Inventory: Tab (placeholder).

## Next Steps (Milestones)
- M1: MVP survival loop + 1 biome + 2 weapons + 1 pet.
- M2: Crafting UI + 5 recipes + basic enemy variety.
- M3: Story collectibles (Kallarai memories) + cutscene triggers.
- M4: LAN/online coop (enable Mirror/Photon).
- M5: Polish SFX, music, menu, settings, optimization.

See more docs in `Docs/IMPLEMENTATION_NOTES.md`.
