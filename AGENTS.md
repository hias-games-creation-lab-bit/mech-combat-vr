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
| Test definitions, measurement methods, test-specific pass criteria | TEST_PLAN.md | - |
| Task PASS and commit eligibility | AGENTS.md | reference only |
| Known bugs | KNOWN_ISSUES.md | - |
| Code quality rules | DEVELOPMENT_RULES.md | - |
| Autonomous workflow | AUTONOMOUS_DEVELOPMENT.md | - |
| Director feedback | HUMAN_FEEDBACK.md | - |
| Architecture decisions, approved Unity version and rendering path | DECISIONS.md | reference only |
| Asset licenses | ASSET_LICENSE_REGISTRY.md | - |

If two files disagree, the Source of Truth file wins.

## Target Platform

- Meta Quest 3 (standalone, NOT PCVR)
- Unity 6.3 LTS (6000.3.25f1), exact Editor version pinned by ADR-001
- URP (Forward only; Forward+ is not approved)
- OpenXR + Meta Quest Support (Multi-View / Single Pass Instanced)
- Vulkan
- Meta XR SDK v207+
- IL2CPP, ARM64
- minSdk 32, targetSdk 34
- 90Hz target

## Performance Budget

- Technical acceptance: sustained 90Hz; application CPU and GPU frame times each < 11.1ms (aim for 9ms)
- Emergency stop floor: application FPS < 72, or CPU/GPU frame time > 13.9ms; this is NOT an alternative PASS threshold
- Measurement and FAIL/BLOCKED classification: see `docs/TEST_PLAN.md`, Performance Acceptance
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

A task is Technical PASS only when ALL eight conditions below are satisfied for its predeclared validation profile:
1. Compile: zero errors.
2. Console: zero runtime errors during the required run.
3. Relevant unit tests: PASS.
4. Relevant PlayMode tests: PASS.
5. Required XR Simulator/operator/device technical checks: PASS.
6. Required performance checks: no acceptance-budget breach.
7. Evidence saved and `docs/PROJECT_STATUS.md` prepared with the task result, test results, evidence references, and remaining blockers.
8. Final staged Git diff reviewed, including the evidence and status update: no unintended, unapproved, or out-of-scope changes.

Use the validation profiles in `docs/AUTONOMOUS_DEVELOPMENT.md`.
A genuinely inapplicable check must be recorded as N/A with a reason before implementation; it is not a test PASS.
An unavailable required tool, device, test, or measurement is BLOCKED, not N/A.
A task-level N/A cannot waive a required Phase-level technical test.

Tests passing alone is NOT task PASS. Evidence, status, and final diff review must also be complete.
A proposed PASS in uncommitted status text is provisional. Persist it only if final review and commit succeed; otherwise record FAIL/BLOCKED and do not advance.
If any required condition fails, do not commit the feature implementation.
The only failure-time exception is the restricted diagnostic/state-only commit path in `docs/AUTONOMOUS_DEVELOPMENT.md`; it never makes the failed task PASS.

## Gates

There are three gate types. Technical Gate is automated; Director Gate and Device Gate are Human Gates.

### Technical Gate (automated)
Codex validates compile, tests, and required performance evidence.
A Phase Technical Gate is PASS when its AUTO tasks and required technical tests are complete.
Human evaluations are excluded from that prerequisite.

### Director Gate (human only)
"Is it fun?" "Does it feel right?" "Is this the right design?"
Only the Director can approve this. Agents may request review and set WAITING, but cannot supply an approval.

### Device Gate (human only)
The Director wears Quest 3 and checks comfort, tracking, presentation, and sustained experience with the measured FPS/thermal evidence.
Automated Quest telemetry is technical evidence, not a substitute for this approval.
Codex never approves a Human Gate. Dots may transcribe an explicit Director decision with its source, candidate build/revision, and timestamp; it must not infer approval.

The Phase sequence is:
`AUTO tasks complete -> Technical Gate PASS -> Human Gates WAITING -> Director decisions recorded -> Phase COMPLETE`.
Both required Human Gates must be approved for the same candidate. Technical PASS alone does not authorize the next Phase.

## After Every Implementation

1. Implement within the approved scope.
2. Compile, inspect runtime Console, and run the relevant unit/PlayMode tests.
3. Run required XR and Quest technical/performance checks.
4. Save sanitized evidence in `docs/validation/` as Markdown reports; keep generated builds and raw capture binaries outside Git.
5. Prepare `docs/PROJECT_STATUS.md` with results, evidence references, and task/gate state.
6. Stage the intended files and review the final staged diff, including status and evidence. Re-run affected checks if implementation or requirements changed after testing.
7. Commit only when all applicable PASS conditions are satisfied. Include implementation, evidence, and status in the same commit.
8. Advance only after commit succeeds. Do not require a post-commit status edit to record that commit's own SHA; Git history identifies it.

On failure, use the diagnostic/state-only path instead of committing failed feature changes.

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
- Deferred rendering / Forward+ rendering
- Silently changing game design values
- Approving a Human Gate on behalf of the Director or inferring approval from silence
- Deleting or modifying a test to make the suite pass
- Reducing performance budgets to make validation pass
- Replacing an APPROVED asset without updating ART_ASSET_REGISTRY.md
- Committing generated build artifacts or API keys
- Reversing an architecture decision in DECISIONS.md without approval
