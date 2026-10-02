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
8. `docs/DECISIONS.md` - Past architecture decisions (do not reverse)
9. `docs/DESIGN_BIBLE.md` - Visual identity rules
10. `docs/ART_ASSET_REGISTRY.md` - Asset status and pipeline
11. `docs/ASSET_LICENSE_REGISTRY.md` - Asset source and license tracking

## Source of Truth

Each type of information has exactly ONE authoritative file. Do not contradict it.

| Information | Source of Truth | Other files are |
|---|---|---|
| Gameplay parameters (damage, speed, HP) | GAME_DESIGN.md | reference only |
| Visual style, colors, silhouette | DESIGN_BIBLE.md | reference only |
| Asset status, tri counts, materials | ART_ASSET_REGISTRY.md | reference only |
| Current task, phase, blockers | PROJECT_STATUS.md | - |
| Test definitions, pass criteria | TEST_PLAN.md | - |
| Known bugs | KNOWN_ISSUES.md | - |
| Code quality rules | DEVELOPMENT_RULES.md | - |
| Autonomous workflow | AUTONOMOUS_DEVELOPMENT.md | - |
| Director feedback | HUMAN_FEEDBACK.md | - |
| Architecture decisions | DECISIONS.md | - |
| Asset licenses | ASSET_LICENSE_REGISTRY.md | - |

If two files disagree, the Source of Truth file wins.

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

## PASS Definition

A task is PASS only when ALL of the following are true:
1. Compile: zero errors
2. Console: zero runtime errors
3. Relevant unit tests: PASS
4. Relevant PlayMode tests: PASS
5. XR Simulator test (if applicable): PASS
6. Git diff reviewed: no unintended changes
7. PROJECT_STATUS.md updated
8. No performance budget breach (frame time, draw calls, GC, memory)

If ANY condition fails, the task is NOT PASS. Do not commit.

## Human Gate

There are 3 types of gates. Codex may NEVER mark a Human Gate as PASS.

### Technical Gate (automated)
Compile, tests, performance validation. Codex handles this.

### Director Gate (human only)
"Is it fun?" "Does it feel right?" "Is this the right design?"
Only Director can PASS this. Codex sets status to WAITING.

### Device Gate (human only)
Quest 3 real-device testing: comfort, tracking, FPS, thermals.
Only Director can PASS this after wearing Quest 3.

## After Every Implementation

1. Compile - zero errors
2. Run relevant unit tests
3. Run relevant PlayMode tests
4. Run XR Simulator test if applicable
5. Check `git diff` - review your own changes
6. Update `docs/PROJECT_STATUS.md`
7. Commit ONLY if all PASS conditions met

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
- Marking a Human Gate (Director Gate / Device Gate) as PASS
- Deleting or modifying a test to make the suite pass
- Reducing performance budgets to make validation pass
- Replacing an APPROVED asset without updating ART_ASSET_REGISTRY.md
- Committing generated build artifacts or API keys
- Reversing an architecture decision in DECISIONS.md without approval
