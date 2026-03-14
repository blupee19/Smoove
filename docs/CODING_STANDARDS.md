# Coding Standards

All C# code in this project follows these standards. They are enforced by `.editorconfig` where possible.

---

## Namespaces

Every script must declare a namespace. The namespace must match the assembly and folder:

| Folder | Namespace |
|--------|-----------|
| `Scripts/Core/` | `Smoove.Core` |
| `Scripts/Gameplay/` | `Smoove.Gameplay` |
| `Scripts/Gameplay/Input/` | `Smoove.Gameplay.Input` |
| `Scripts/Gameplay/Characters/` | `Smoove.Gameplay.Characters` |
| `Scripts/Gameplay/Systems/` | `Smoove.Gameplay.Systems` |
| `Scripts/UI/` | `Smoove.UI` |
| `Scripts/Utilities/` | `Smoove.Utilities` |

```csharp
// ✅ Correct
namespace Smoove.Gameplay.Characters
{
    public class PlayerController : MonoBehaviour { }
}

// ❌ Wrong — missing namespace
public class PlayerController : MonoBehaviour { }
```

---

## Naming Conventions

| Symbol | Convention | Example |
|--------|-----------|---------|
| Class / Struct / Enum | `PascalCase` | `PlayerController`, `GameState` |
| Interface | `IPascalCase` | `IDamageable` |
| Method | `PascalCase` | `TakeDamage()` |
| Property | `PascalCase` | `CurrentHealth` |
| Public field | `PascalCase` | *(avoid — prefer properties)* |
| Private field | `_camelCase` | `_currentHealth` |
| Local variable | `camelCase` | `damageAmount` |
| Constant | `ALL_CAPS` | `MAX_HEALTH` |
| Parameter | `camelCase` | `damageAmount` |

```csharp
public class Enemy : MonoBehaviour
{
    private const int MAX_HEALTH = 100;

    [SerializeField] private int _health;

    public int Health => _health;

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
    }
}
```

---

## Unity Event Methods

Unity event methods (`Awake`, `Start`, `Update`, `OnEnable`, `OnDisable`, `OnDestroy`, etc.) must be `private` unless they are intentionally `protected` for inheritance.

```csharp
// ✅ Correct
private void Awake() { }
private void Update() { }
protected virtual void OnDestroy() { }

// ❌ Wrong
public void Awake() { }
void Update() { }   // missing access modifier
```

**Order of Unity event methods** (follow this ordering for consistency):

1. `Awake`
2. `OnEnable`
3. `Start`
4. `Update` / `FixedUpdate` / `LateUpdate`
5. `OnDisable`
6. `OnDestroy`
7. Other Unity callbacks (`OnTriggerEnter2D`, `OnCollisionEnter2D`, etc.)

---

## SerializeField vs Public Fields

Always use `[SerializeField]` with a private backing field. Never expose public fields.

```csharp
// ✅ Correct — inspector-visible, encapsulated
[SerializeField] private float _moveSpeed = 5f;
public float MoveSpeed => _moveSpeed;

// ❌ Wrong — breaks encapsulation
public float MoveSpeed = 5f;
```

---

## Null Checks

Prefer null-conditional operators and null-coalescing:

```csharp
// ✅
target?.TakeDamage(damage);
string name = go != null ? go.name : "Unknown";

// ✅ C# 8+ null-coalescing assignment
_instance ??= FindFirstObjectByType<T>();

// ❌ Verbose
if (target != null)
    target.TakeDamage(damage);
```

---

## Avoiding `Find` in Hot Paths

**Never** call `Find`, `FindObjectOfType`, or `GetComponent` in `Update` or other per-frame methods. Cache references in `Awake` or `Start`.

```csharp
// ✅ Cache once
private Rigidbody2D _rb;

private void Awake()
{
    _rb = GetComponent<Rigidbody2D>();
}

private void FixedUpdate()
{
    _rb.linearVelocity = _moveInput * _moveSpeed;
}

// ❌ Expensive every frame
private void FixedUpdate()
{
    GetComponent<Rigidbody2D>().linearVelocity = _moveInput * _moveSpeed;
}
```

---

## Comments and Documentation

- **XML summary comments** are required on all `public` and `protected` API members.
- **Inline comments** should explain *why*, not *what*.
- Do not comment out dead code — delete it (Git preserves history).

```csharp
/// <summary>
/// Applies damage to this entity and triggers the hurt animation.
/// </summary>
/// <param name="amount">Damage to apply. Must be positive.</param>
public void TakeDamage(int amount)
{
    // Clamp prevents health going below zero on massive hits
    _health = Mathf.Max(0, _health - amount);
    _animator.SetTrigger("Hurt");

    if (_health == 0)
        Die();
}
```

---

## Coroutines vs Async/Await

- Use **coroutines** (`IEnumerator`) for Unity frame-based operations (tweens, waiting for frames, etc.).
- Use **`async/await`** with `UniTask` (if added) or `Task` for I/O operations (file read/write, network).
- Never mix the two in the same workflow.

---

## ScriptableObjects as Data Containers

- Suffix all ScriptableObject classes with `SO`: `PlayerStatsSO`, `AudioClipSO`.
- Use `[CreateAssetMenu]` to allow creation from the Unity menu.
- Store all SO assets in `Assets/_Project/ScriptableObjects/`.

---

## Braces

Always use braces, even for single-line blocks:

```csharp
// ✅
if (isDead)
{
    Die();
}

// ❌
if (isDead) Die();
```
