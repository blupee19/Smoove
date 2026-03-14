# Contributing

Thank you for contributing to Smoove. Please read this guide before opening a pull request.

---

## Prerequisites

- Ensure you have the correct Unity version installed (see [README.md](../README.md#prerequisites)).
- Read the [Coding Standards](CODING_STANDARDS.md) and [Architecture](ARCHITECTURE.md) docs before writing code.

---

## Branching Strategy

We use a simplified **Git Flow**:

| Branch | Purpose |
|--------|---------|
| `main` | Stable, always shippable |
| `develop` | Integration branch for features |
| `feature/<name>` | New features (`feature/player-jump`) |
| `fix/<name>` | Bug fixes (`fix/camera-jitter`) |
| `refactor/<name>` | Refactoring with no behaviour change |
| `docs/<name>` | Documentation only |

### Rules

- **Never commit directly to `main`.**
- Branch from `develop` for features and fixes.
- Keep branches short-lived and focused on a single concern.

---

## Commit Messages

Use the **imperative mood** and follow this format:

```
<type>: <short summary> (50 chars max)

<optional body explaining WHY, not what>
```

**Types:**

| Type | When to use |
|------|------------|
| `feat` | New feature |
| `fix` | Bug fix |
| `refactor` | Code change without behaviour change |
| `docs` | Documentation only |
| `style` | Formatting, whitespace |
| `test` | Tests |
| `chore` | Build, CI, dependency updates |

**Examples:**

```
feat: add double-jump to PlayerController

fix: prevent GameManager from spawning twice in editor

refactor: extract scene loading logic into SceneLoader

docs: add input system setup guide
```

---

## Pull Request Checklist

Before opening a PR, confirm:

- [ ] Code follows the [Coding Standards](CODING_STANDARDS.md).
- [ ] All scripts use the correct namespace.
- [ ] No `Debug.Log` calls left in production code (use `#if UNITY_EDITOR` guards if needed).
- [ ] New scripts are in the correct folder under `Assets/_Project/Scripts/`.
- [ ] New assets are in the correct folder under `Assets/_Project/`.
- [ ] No broken Unity meta file references (open Unity and check the console).
- [ ] PR description explains what changed and why.
- [ ] Branch is up to date with the target branch.

---

## Pull Request Template

```markdown
## Summary
What does this PR do?

## Changes
- Added ...
- Fixed ...
- Refactored ...

## Testing
How was this tested?

## Screenshots / GIFs (if applicable)
```

---

## Unity-Specific Guidelines

- **Scene files**: avoid committing unnecessary scene changes. If your feature requires a new scene object, discuss with the team first.
- **Meta files**: always commit `.meta` files alongside their assets. A missing meta file breaks GUID references.
- **Prefabs**: prefer Prefab Variants over duplicating prefabs.
- **No console errors**: the project console must be clean (0 errors, 0 warnings) before merging.

---

## Getting Help

Open an issue with the `question` label or ask in the team channel.
