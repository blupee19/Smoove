# Changelog

All notable changes to this project are documented here.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

---

## [Unreleased]

### Added
- Industry-standard project folder structure under `Assets/_Project/`
- Assembly Definition files: `Smoove.Core`, `Smoove.Gameplay`, `Smoove.UI`, `Smoove.Utilities`
- `Singleton<T>` generic MonoBehaviour base class (`Smoove.Utilities`)
- `GameManager` with `GameState` enum and `OnGameStateChanged` event (`Smoove.Core`)
- `SceneLoader` for async scene transitions (`Smoove.Core`)
- `InputHandler` wrapping Unity Input System (`Smoove.Gameplay.Input`)
- `ExtensionMethods` utility class with common helpers (`Smoove.Utilities`)
- `.editorconfig` for consistent C# code style
- `README.md` with setup and usage instructions
- `docs/ARCHITECTURE.md` — system design and layer diagram
- `docs/CODING_STANDARDS.md` — naming, style, and Unity-specific rules
- `docs/CONTRIBUTING.md` — branching strategy and PR checklist
- `docs/INPUT_SYSTEM.md` — Input System setup and usage guide
- `CHANGELOG.md`

---

## [0.1.0] — 2026-03-14

### Added
- Initial Unity 6000.3.11f1 project using URP 2D template
- `InputSystem_Actions.inputactions` with Player and UI action maps
- Default URP renderer and pipeline settings
- `SampleScene` with orthographic camera
