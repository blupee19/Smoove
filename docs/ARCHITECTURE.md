# Architecture

This document describes the high-level architecture of the Smoove project.

---

## Guiding Principles

1. **Separation of concerns** — each assembly/namespace has a single, well-defined responsibility.
2. **Dependency direction** — dependencies flow inward (UI → Core, Gameplay → Core). Core never depends on UI or Gameplay.
3. **No singletons by default** — use `[SerializeField]` references wherever possible. The `Singleton<T>` base class exists only for truly global services (GameManager, SceneLoader, AudioManager, etc.).
4. **Data over code** — prefer `ScriptableObject` data containers for game configuration (stats, audio clips, level data). This decouples tuning from implementation.
5. **Event-driven communication** — use C# `Action` / `event` or Unity Events to notify systems of state changes instead of direct cross-layer calls.

---

## Layer Diagram

```
┌─────────────────────────────────┐
│           UI Layer              │  Smoove.UI
│  HUD, Menus, Panels, Popups     │
└──────────────┬──────────────────┘
               │ reads state / subscribes to events
┌──────────────▼──────────────────┐
│        Gameplay Layer           │  Smoove.Gameplay
│  Player, Enemies, Systems,      │
│  Input, Cameras                 │
└──────────────┬──────────────────┘
               │ reads state / raises events
┌──────────────▼──────────────────┐
│          Core Layer             │  Smoove.Core
│  GameManager, SceneLoader,      │
│  Audio, Save/Load               │
└──────────────┬──────────────────┘
               │ uses
┌──────────────▼──────────────────┐
│        Utilities Layer          │  Smoove.Utilities
│  Singleton<T>, Extensions,      │
│  Helper classes                 │
└─────────────────────────────────┘
```

---

## Core Systems

### GameManager (`Smoove.Core.GameManager`)

- Single source of truth for the current `GameState` (MainMenu, Loading, Gameplay, Paused, GameOver).
- Other systems subscribe to the static `GameManager.OnGameStateChanged` event.
- Lives on a DontDestroyOnLoad GameObject — instantiated in the first (bootstrap) scene.

**Usage:**
```csharp
GameManager.Instance.SetState(GameState.Paused);
GameManager.OnGameStateChanged += HandleStateChange;
```

### SceneLoader (`Smoove.Core.SceneLoader`)

- Wraps `SceneManager.LoadSceneAsync` with a configurable minimum load time.
- Automatically sets GameState to `Loading` during transitions.

**Usage:**
```csharp
SceneLoader.Instance.LoadScene("GameplayScene");
SceneLoader.Instance.ReloadCurrentScene();
```

---

## Gameplay Systems

### InputHandler (`Smoove.Gameplay.Input.InputHandler`)

- Reads raw input from Unity's Input System via the `SendMessage` callback model.
- Exposes clean, typed properties (`MoveInput`, `JumpPressed`, etc.) to other components.
- Call `ConsumeOneShots()` at the end of your update loop to reset frame-based inputs.

**Usage:**
```csharp
// On the same GameObject as PlayerInput component:
[SerializeField] private InputHandler _input;

void Update()
{
    Move(_input.MoveInput);
    if (_input.JumpPressed) Jump();
    _input.ConsumeOneShots();
}
```

---

## ScriptableObject Data Pattern

Use `ScriptableObject` assets in `Assets/_Project/ScriptableObjects/Data/` for all game configuration:

```csharp
// Example: PlayerStats.cs
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Smoove/Data/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public int MaxHealth = 100;
}
```

Reference the asset via `[SerializeField]` — never hard-code values in scripts.

---

## Scene Structure

| Scene | Purpose |
|-------|---------|
| `SampleScene` | Default Unity scene — use as sandbox |
| `_Project/Scenes/Bootstrap` *(future)* | Initialises singletons (GameManager, SceneLoader) before loading the main menu |
| `_Project/Scenes/MainMenu` *(future)* | Title screen |
| `_Project/Scenes/Gameplay` *(future)* | Primary gameplay scene |

**Bootstrap Pattern:**

Create a `Bootstrap` scene that is always the first scene in Build Settings (index 0). It instantiates all persistent singletons and then immediately loads the `MainMenu` scene.

---

## Folder Conventions

| Path | Contents |
|------|----------|
| `Assets/_Project/Scripts/Core/` | Global singletons and services |
| `Assets/_Project/Scripts/Gameplay/Characters/` | `PlayerController`, NPC base classes |
| `Assets/_Project/Scripts/Gameplay/Input/` | `InputHandler` |
| `Assets/_Project/Scripts/Gameplay/Systems/` | Combat, inventory, dialogue systems |
| `Assets/_Project/Scripts/UI/` | All UI scripts (no game logic) |
| `Assets/_Project/Scripts/Utilities/` | Reusable helpers, extensions |
| `Assets/_Project/Prefabs/` | All prefabs mirroring the Scripts structure |
| `Assets/_Project/ScriptableObjects/Data/` | Runtime data assets |
