# Smoove

A 2D game built with **Unity 6000** using the **Universal Render Pipeline (URP)** and the **Input System** package.

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Getting Started](#getting-started)
3. [Project Structure](#project-structure)
4. [Architecture](#architecture)
5. [Input System](#input-system)
6. [Coding Standards](#coding-standards)
7. [Assembly Definitions](#assembly-definitions)
8. [Contributing](#contributing)
9. [Changelog](#changelog)

---

## Prerequisites

| Tool | Version |
|------|---------|
| Unity Editor | **6000.3.11f1** (exact version required) |
| Git | 2.x or later |
| IDE | Rider 2024+ or Visual Studio 2022+ |

> Download the exact Unity version via [Unity Hub](https://unity.com/download). Using a different version may cause serialization warnings.

---

## Getting Started

### 1. Clone the repository

```bash
git clone <repository-url>
cd Smoove
```

### 2. Open in Unity

1. Launch **Unity Hub**.
2. Click **Open > Add project from disk**.
3. Select the `Smoove/` folder (the one containing `Assets/`, `Packages/`, `ProjectSettings/`).
4. Unity will import the project — the first import takes a few minutes.

### 3. Open the main scene

In the **Project** window navigate to:

```
Assets/Scenes/SampleScene.unity
```

Double-click to open, then press **Play** to run.

### 4. IDE Setup

- **Rider / VS Code**: Unity auto-generates `.csproj` files on open. Select your IDE in **Edit → Preferences → External Tools → External Script Editor**.
- The `.editorconfig` at the project root enforces code style automatically.

---

## Project Structure

```
Smoove/
├── Assets/
│   ├── _Project/                   # All game-specific content lives here
│   │   ├── Art/
│   │   │   ├── Animations/         # Animator controllers & animation clips
│   │   │   ├── Fonts/              # Custom fonts
│   │   │   └── Sprites/
│   │   │       ├── Characters/
│   │   │       ├── Environment/
│   │   │       └── UI/
│   │   ├── Audio/
│   │   │   ├── Music/
│   │   │   └── SFX/
│   │   ├── Prefabs/
│   │   │   ├── Characters/
│   │   │   ├── Environment/
│   │   │   └── UI/
│   │   ├── ScriptableObjects/
│   │   │   └── Data/               # ScriptableObject data assets
│   │   ├── Scenes/                 # Game scenes (gameplay, menus, etc.)
│   │   └── Scripts/
│   │       ├── Core/               # [Smoove.Core] GameManager, SceneLoader
│   │       ├── Gameplay/
│   │       │   ├── Characters/     # Player & NPC controllers
│   │       │   ├── Input/          # [Smoove.Gameplay] InputHandler
│   │       │   └── Systems/        # Game systems (combat, inventory, etc.)
│   │       ├── UI/                 # [Smoove.UI] HUD, menus, panels
│   │       └── Utilities/          # [Smoove.Utilities] Singleton, extensions
│   ├── Plugins/                    # Native plugins (DLLs, .so, etc.)
│   ├── Scenes/                     # Legacy / Unity-generated scenes
│   ├── Scripts/                    # Legacy placeholder — move scripts to _Project
│   ├── Settings/                   # URP renderer & pipeline assets
│   └── ThirdParty/                 # Third-party Unity assets
├── Packages/                       # Unity package manifest (do not edit manually)
├── ProjectSettings/                # Unity project settings (committed to git)
├── docs/                           # Extended documentation
│   ├── ARCHITECTURE.md
│   ├── CODING_STANDARDS.md
│   ├── CONTRIBUTING.md
│   └── INPUT_SYSTEM.md
├── .editorconfig                   # Code style rules
├── .gitignore
├── CHANGELOG.md
└── README.md
```

> **Rule:** All new game content and scripts go inside `Assets/_Project/`. Only third-party assets go into `Assets/Plugins/` or `Assets/ThirdParty/`.

---

## Architecture

The project uses a lightweight layered architecture:

```
[ UI Layer ]  ──→  [ Gameplay Layer ]  ──→  [ Core Layer ]
                                         ──→  [ Utilities ]
```

| Layer | Assembly | Responsibility |
|-------|----------|----------------|
| **Core** | `Smoove.Core` | GameManager, SceneLoader, global state |
| **Gameplay** | `Smoove.Gameplay` | Player, enemies, input, game systems |
| **UI** | `Smoove.UI` | HUD, menus, panels |
| **Utilities** | `Smoove.Utilities` | Singleton base, extension methods |

For full details see [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md).

---

## Input System

Input is handled via **Unity's Input System** package. The action map is defined in:

```
Assets/InputSystem_Actions.inputactions
```

Available action maps:

| Map | Key Actions |
|-----|------------|
| **Player** | Move, Look, Jump, Attack, Interact, Sprint, Crouch |
| **UI** | Navigate, Submit, Cancel, Point, Click, ScrollWheel |

The `InputHandler` component (`Assets/_Project/Scripts/Gameplay/Input/InputHandler.cs`) wraps the Input System and exposes clean properties to other scripts.

For setup details see [docs/INPUT_SYSTEM.md](docs/INPUT_SYSTEM.md).

---

## Coding Standards

- All scripts use **namespaces** (`Smoove.Core`, `Smoove.Gameplay`, etc.).
- Private fields are prefixed with `_` and use `camelCase`.
- Public members use `PascalCase`.
- Constants use `ALL_CAPS_WITH_UNDERSCORES`.
- Unity event methods (`Awake`, `Start`, `Update`, etc.) are `private` unless intentionally overridden.
- No `Find()` or `FindObjectOfType()` in hot paths — use dependency injection via `[SerializeField]` or the `Singleton` base class.

For the full guide see [docs/CODING_STANDARDS.md](docs/CODING_STANDARDS.md).

---

## Assembly Definitions

Each script module compiles into its own assembly, reducing incremental compile times and enforcing layer boundaries.

| Assembly | Location |
|----------|----------|
| `Smoove.Core` | `Assets/_Project/Scripts/Core/` |
| `Smoove.Gameplay` | `Assets/_Project/Scripts/Gameplay/` |
| `Smoove.UI` | `Assets/_Project/Scripts/UI/` |
| `Smoove.Utilities` | `Assets/_Project/Scripts/Utilities/` |

Dependency graph:

```
Smoove.Core      ──→  Smoove.Utilities
Smoove.Gameplay  ──→  Smoove.Core, Smoove.Utilities, Unity.InputSystem
Smoove.UI        ──→  Smoove.Core, Smoove.Utilities
```

---

## Contributing

Please read [docs/CONTRIBUTING.md](docs/CONTRIBUTING.md) before submitting pull requests.

Quick summary:
1. Branch from `main` using the naming convention `feature/your-feature-name` or `fix/bug-description`.
2. Follow the coding standards.
3. Write descriptive commit messages (imperative mood: "Add player jump", not "Added player jump").
4. Open a PR with a clear description of what changed and why.

---

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a full version history.
