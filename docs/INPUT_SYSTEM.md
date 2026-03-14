# Input System

This project uses **Unity's Input System** package (v1.19.0). This document explains the setup and how to use input in your scripts.

---

## Overview

Input is defined in a single action asset:

```
Assets/InputSystem_Actions.inputactions
```

This file contains two action maps:

| Action Map | Purpose |
|------------|---------|
| `Player` | In-game player controls |
| `UI` | Menu / UI navigation |

---

## Player Actions

| Action | Type | Default Bindings |
|--------|------|-----------------|
| `Move` | Value (Vector2) | WASD / Arrow Keys / Left Stick |
| `Look` | Value (Vector2) | Mouse Delta / Right Stick |
| `Jump` | Button | Space / Gamepad South |
| `Attack` | Button | Left Mouse / Gamepad West |
| `Interact` | Button | E / Gamepad North |
| `Crouch` | Button | C / Gamepad East |
| `Sprint` | Button | Left Shift / Left Stick Press |
| `Previous` | Button | 1 / D-Pad Left |
| `Next` | Button | 2 / D-Pad Right |

---

## Control Schemes

| Scheme | Devices |
|--------|---------|
| Keyboard & Mouse | Keyboard + Mouse |
| Gamepad | Xbox, PlayStation, Switch Pro controllers |
| Touch | Mobile touch screen |
| Joystick | Arcade sticks |
| XR | VR controllers |

---

## How to Use Input in Scripts

### Step 1 — Add components to your player GameObject

Add both components to the same GameObject:

1. **`PlayerInput`** (Unity built-in)
   - Set **Actions** to `InputSystem_Actions`
   - Set **Default Map** to `Player`
   - Set **Behavior** to `Send Messages`

2. **`InputHandler`** (`Assets/_Project/Scripts/Gameplay/Input/InputHandler.cs`)

### Step 2 — Reference InputHandler in your script

```csharp
using UnityEngine;
using Smoove.Gameplay.Input;

namespace Smoove.Gameplay.Characters
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputHandler _input;

        private void Update()
        {
            HandleMovement();
            HandleActions();
            _input.ConsumeOneShots(); // reset frame-based inputs
        }

        private void HandleMovement()
        {
            Vector2 move = _input.MoveInput;
            // apply movement ...
        }

        private void HandleActions()
        {
            if (_input.JumpPressed)
                Jump();

            if (_input.AttackPressed)
                Attack();
        }
    }
}
```

### Step 3 — Assign the reference

In the Unity Inspector, drag the `InputHandler` component into the `_input` field on `PlayerController`.

---

## One-Shot vs Continuous Inputs

| Type | Property | Notes |
|------|----------|-------|
| Continuous | `MoveInput`, `LookInput`, `SprintHeld`, `CrouchHeld` | Updated every frame while held |
| One-Shot | `JumpPressed`, `AttackPressed`, `InteractPressed` | True for one frame; call `ConsumeOneShots()` to reset |

Always call `_input.ConsumeOneShots()` at the **end** of your update loop, after all systems have had a chance to read the values.

---

## Adding New Actions

1. Open `Assets/InputSystem_Actions.inputactions` in Unity.
2. Click the `+` button in the **Player** action map to add a new action.
3. Configure bindings for each control scheme.
4. Add a corresponding `OnActionName(InputValue value)` method to `InputHandler.cs`.

```csharp
// Example: adding a "Dash" action
public bool DashPressed { get; private set; }

public void OnDash(InputValue value)
{
    DashPressed = value.isPressed;
}
```

> The method name must exactly match `On` + the action name (case-sensitive).

---

## Switching Action Maps

To switch between Player and UI input (e.g., when opening a menu):

```csharp
private PlayerInput _playerInput;

private void Awake()
{
    _playerInput = GetComponent<PlayerInput>();
}

public void EnableUIInput()
{
    _playerInput.SwitchCurrentActionMap("UI");
}

public void EnablePlayerInput()
{
    _playerInput.SwitchCurrentActionMap("Player");
}
```

---

## Testing Input

Use **Window → Analysis → Input Debugger** in the Unity Editor to inspect active devices, bindings, and live input values.
