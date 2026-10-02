# BFTK VR - AI Development Instructions

This file is the primary instruction set for any AI agent (Codex, Claude Code, or other) working on this repository.

## Before Any Work

Read these files in order:
1. `docs/DEVELOPMENT_RULES.md` - What you must and must not do
2. `docs/PROJECT_STATUS.md` - Current phase, tasks, and state
3. `docs/GAME_DESIGN.md` - Complete game specification
4. `docs/TEST_PLAN.md` - Test cases for current phase
5. `docs/KNOWN_ISSUES.md` - Known bugs and workarounds
6. `docs/AUTONOMOUS_DEVELOPMENT.md` - Autonomous loop rules
7. `docs/HUMAN_FEEDBACK.md` - Director's latest feedback

## Target Platform

- Meta Quest 3 (standalone, NOT PCVR)
- Unity 6.3 LTS (6000.0.x)
- URP (Forward/Forward+)
- OpenXR + Meta Quest Support (Multi-View / Single Pass Instanced)
- Vulkan
- Meta XR SDK v207+
- IL2CPP, ARM64
- minSdk 32, targetSdk 34
- 90Hz target

## Performance Budget

- Frame time: < 11.1ms (aim for 9ms)
- Draw calls: < 150/eye (warning 250, hard limit 300)
- Visible triangles: < 300k ideal (hard limit 750k)
- Texture memory: < 400MB (hard limit 512MB)
- GC Alloc during gameplay: 0 B/frame
- Rigidbodies: max 30 active
- Colliders: max 50 active, primitive only
- Particles: max 200 simultaneous
- AudioSources: < 16 simultaneous
- Textures: max 2K, ASTC 6x6 compression

## Required Architecture

- ScriptableObject data-driven design (WeaponDefinition, EnemyDefinition)
- State Machine enemy AI (IEnemyState, not Behavior Tree)
- IDamageable interface + event-driven combat
- Strategy pattern weapons (IWeapon + WeaponFactory)
- Factory pattern enemies (EnemyFactory + EnemyConfig)
- Object pooling for projectiles, effects, enemies
- Input abstraction (XR Controller -> PlayerInput -> MechController)

## After Every Implementation

1. Compile - zero errors, zero warnings where possible
2. Run relevant unit tests
3. Run relevant PlayMode tests
4. Run XR Simulator test if applicable
5. Check `git diff` - review your own changes
6. Update `docs/PROJECT_STATUS.md`
7. Commit ONLY if all checks pass

## Forbidden

- Changing Unity/SDK/package versions without director approval
- Modifying ProjectSettings without explicit approval
- FindObjectOfType / FindObjectsByType / Camera.main in Update
- Instantiate/Destroy during combat (use pools)
- LINQ / new List / new GameObject in Update
- String concatenation in Update
- Empty Update() methods
- SendMessage in damage loops
- MeshCollider (use primitive only)
- Standard shader (use URP/Lit or URP/Simple Lit)
- Multi-Pass rendering
- OpenGLES
- Realtime GI / SSAO / Motion Blur / HDR
- Deferred rendering
- Silently changing game design values
