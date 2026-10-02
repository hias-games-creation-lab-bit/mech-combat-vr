# PROJECT STATUS

## Platform
- Unity 6.3 LTS (6000.3.25f1, exact Editor version; ADR-001)
- Quest 3 only
- Android ARM64
- IL2CPP
- Vulkan
- OpenXR + Multi-View
- URP (Forward only)
- Meta XR SDK v207+

## Current Phase
M0

## Phase State
RUNNING

## Current Task
M0-003 OpenXR initial installation

## Autonomous
YES

## Technical Gate
RUNNING

## Human Gate
NOT_READY

## Director Gate
NOT_READY

## Device Gate
NOT_READY

## State Rules
- Task states: TODO / IN_PROGRESS / PASS / FAIL / BLOCKED.
- Phase states: NOT_STARTED / RUNNING / WAITING_HUMAN / BLOCKED / COMPLETE.
- Technical Gate: NOT_STARTED / RUNNING / PASS / FAIL / BLOCKED.
- Human Gates: NOT_READY / WAITING / PASS / FAIL. Human Gate is the aggregate of Director Gate and Device Gate.
- The task lists below contain AUTO work only. Check an AUTO task only when AGENTS.md task PASS conditions are met and its implementation/evidence/status commit succeeds.
- A working-tree PASS edit is provisional; only a successfully committed result is authoritative.
- After all AUTO tasks and required technical tests pass, set Technical Gate = PASS, Human Gate = WAITING, Director Gate = WAITING, Device Gate = WAITING, and Phase State = WAITING_HUMAN. Save this state before stopping.
- Human Gate = PASS only when both human decisions for the same candidate are explicitly approved. Any human rejection makes Human Gate = FAIL and Phase State = BLOCKED.
- Only the Director supplies human approval. Dots may mirror an explicit decision with a HUMAN_FEEDBACK.md reference; Codex cannot approve it.
- Phase COMPLETE requires Technical Gate PASS and both human approvals. Enter the next Phase only with the Director's explicit transition authorization.
- A changed candidate implementation or relevant specification invalidates prior gate approvals and requires revalidation. Diagnostic/approval records alone do not change the tested build.
- Autonomous = YES is permission to run approved AUTO work, not permission to bypass WAITING_HUMAN or BLOCKED.

## Tasks (AUTO only)

### M0 - Technical Foundation
- [x] M0-001 Create Unity 6.3 LTS (6000.3.25f1) project (Universal 3D / URP); verify ProjectVersion.txt matches ADR-001
- [x] M0-002 Configure Android build settings (IL2CPP, ARM64, Vulkan, minSdk 32, targetSdk 34)
- [x] M0-003 Install OpenXR Plugin (compatible latest stable)
- [ ] M0-004 Install Meta XR SDK All-in-One v207+
- [ ] M0-005 Run Meta XR SDK Project Setup Tool; review findings and apply only explicitly approved fixes (no Editor upgrade)
- [ ] M0-006 Configure URP Asset (Forward, MSAA 4x, HDR Off, Post Processing Off)
- [ ] M0-007 Set 90Hz target in OVRManager
- [ ] M0-008 Install Meta VR CLI + XR Operator via Meta XR SDK AI Tools
- [ ] M0-009 Build APK for Quest 3
- [ ] M0-010 Deploy to Quest 3 and verify launch
- [ ] M0-011 Verify head tracking via automated pose/log checks (human confirmation belongs to HG-M0-DEVICE)
- [ ] M0-012 Verify both controllers detected via automated input/log checks (human confirmation belongs to HG-M0-DEVICE)
- [ ] M0-013 Prepare Phase technical handoff report; save evidence/status, review final diff, then commit (not a Human Gate)

### M1 - VR Cockpit
- [ ] M1-001 Create cockpit geometry (box prototype, 4 walls + ceiling)
- [ ] M1-002 Set cockpit scale (seat at ~15m height, interior 1:1 human)
- [ ] M1-003 Fix cockpit/seat to vehicle; preserve independent 1:1 head tracking and lean parallax (ADR-008)
- [ ] M1-004 Left hand input -> mech movement (forward/back/turn)
- [ ] M1-005 Implement yaw turn rate limit (25 deg/sec default, inspector tunable)
- [ ] M1-006 Lock artificial movement pitch/roll; do not constrain tracked head pose
- [ ] M1-007 Implement acceleration limit (< 5 m/s^2, inspector tunable)
- [ ] M1-008 Right hand weapon mount (controller forward = barrel)
- [ ] M1-009 Trigger -> fire pooled projectile (80 m/s)
- [ ] M1-010 Add ground plane (200m x 200m) + scale cue boxes (3-5)
- [ ] M1-011 Basic vignette on boost (inspector tunable)
- [ ] M1-012 5-minute automated XR Simulator stability/tracking test (TEST-M1-005 technical part; no comfort verdict)
- [ ] M1-013 Quest 3 build + deploy + technical performance checks (human comfort belongs to HG-M1-DEVICE)
- [ ] M1-014 Prepare Phase technical handoff report; save evidence/status, review final diff, then commit (not a Human Gate)

### M2 - Combat Core
- [ ] M2-001 IDamageable interface + DamageInfo
- [ ] M2-002 Event-driven damage system (OnDamaged, OnDeath)
- [ ] M2-003 Player health + death + restart (3 sec)
- [ ] M2-004 Enemy spawn system (object pooled)
- [ ] M2-005 Enemy movement (approach player, basic strafe)
- [ ] M2-006 Enemy shooting (hitscan with tracer)
- [ ] M2-007 Hit detection (projectile -> IDamageable)
- [ ] M2-008 Enemy hit reaction (visual feedback, stagger)
- [ ] M2-009 Enemy death (return to pool)
- [ ] M2-010 Weapon heat system (ScriptableObject WeaponDefinition)
- [ ] M2-011 Basic haptic on fire (single pattern, amplitude 0.5, 30ms)
- [ ] M2-012 Basic haptic on player hit (both controllers, 80ms)
- [ ] M2-013 Duplicate damage prevention (1 hit = 1 damage)
- [ ] M2-014 GC allocation check (must be 0 B/frame during combat)
- [ ] M2-016 Quest 3 build + deploy (prerequisite for M2-015)
- [ ] M2-015 5-minute Quest 3 technical combat/performance session (TEST-M2-010 technical part; human feel belongs to HG-M2-DIRECTOR)
- [ ] M2-017 Prepare Phase technical handoff report; save evidence/status, review final diff, then commit (not a Human Gate)

### M3 - Feel
- [ ] M3-001 Weapon-specific haptic patterns (Rifle/Cannon/Missile distinct)
- [ ] M3-002 Directional hit haptics (left/right controller based on hit direction)
- [ ] M3-003 Boost haptic (continuous low amplitude pulsing)
- [ ] M3-004 Muzzle flash (point light 0.05 sec on cockpit)
- [ ] M3-005 Enemy stagger on cannon hit
- [ ] M3-006 Placeholder SFX (Freesound.org CC0): fire, impact, enemy react, cockpit ambient
- [ ] M3-007 Optional secondary-part visual walk bob (2-4cm, 1-2 deg roll; default off, Inspector-tunable; never move cockpit reference shell or XR camera)
- [ ] M3-008 Speed lines / dust particles on boost
- [ ] M3-009 Quest 3 build + deploy + automated smoke/performance checks (human feel belongs to HG-M3-DIRECTOR)
- [ ] M3-010 Prepare Phase technical handoff report; save evidence/status, review final diff, then commit (not a Human Gate)

### M4 - Vertical Slice
- [ ] M4-001 3 WeaponDefinition SOs (Rifle, Cannon, Missile)
- [ ] M4-002 Weapon switching (button input)
- [ ] M4-003 Missile lock-on system (1.5 sec, homing 15 m/s)
- [ ] M4-004 3 EnemyDefinition SOs (Grunt, Lancer, Support)
- [ ] M4-005 Grunt AI: strafe at 15-25m, stay in front arc
- [ ] M4-006 Lancer AI: boost to 5m, circle at 60 deg/sec
- [ ] M4-007 Support AI: stay at 40m+, telegraphed charge shot
- [ ] M4-008 Boss: 3 phases x 90 sec, targetable parts
- [ ] M4-009 Stage flow: 2 enemies -> 3 -> strong -> group -> boss -> clear
- [ ] M4-010 Cockpit HUD: Layer 1 instruments + Layer 2 reticle (#00FFCC)
- [ ] M4-011 Victory / defeat / result screen
- [ ] M4-012 Automated full-stage flow test (technical outcome only; engagement is a Human Gate)
- [ ] M4-014 Quest 3 build + deploy + technical full-stage/performance test (human playtest belongs to HG-M4-DIRECTOR/DEVICE)
- [ ] M4-013 Review Quest performance capture from M4-014 (draw calls, tri, GC, frame time)
- [ ] M4-015 Prepare Phase technical handoff report; save evidence/status, review final diff, then commit (not a Human Gate)

## Human Gate Checklist (not AUTO tasks)

All entries are initially NOT_READY. The current Phase's entries become WAITING only after Technical Gate PASS.
These entries never block completion of the AUTO task list; they block Phase completion and transition.

| Gate ID | Owner | Required check | Status |
|---|---|---|---|
| HG-M0-DIRECTOR | Director | Review foundation evidence and approve M0 completion | NOT_READY |
| HG-M0-DEVICE | Director wearing Quest 3 | Launch, display, head tracking, both controllers | NOT_READY |
| HG-M1-DIRECTOR | Director | Cockpit presence and control feel | NOT_READY |
| HG-M1-DEVICE | Director wearing Quest 3 | TEST-M1-005 human comfort check, lean/turn tracking | NOT_READY |
| HG-M2-DIRECTOR | Director | Is shooting fun? | NOT_READY |
| HG-M2-DEVICE | Director wearing Quest 3 | TEST-M2-010 human comfort check with 90Hz evidence | NOT_READY |
| HG-M3-DIRECTOR | Director | Weapon feel, haptics, sound, satisfaction | NOT_READY |
| HG-M3-DEVICE | Director wearing Quest 3 | Sensory feedback, optional effects, comfort | NOT_READY |
| HG-M4-DIRECTOR | Director | Full-stage engagement and completion approval | NOT_READY |
| HG-M4-DEVICE | Director wearing Quest 3 | Full playtest, comfort, sustained performance/thermal review | NOT_READY |

## Current Validation Record
- Task: M0-003 PASS (provisional until final review and commit); profile FOUNDATION, declared before initial package installation.
- Initial package selection: com.unity.xr.openxr 1.18.0, highest stable version observed in Unity Registry, Unity requirement 6000.0; excludes 1.19.0-pre.1. Meets Meta XR Operator documented minimum 1.17.0.
- Required: resolved package/lock inspection, exact Editor and Android-target compile, persisted Android/Forward assertions, existing PlayMode smoke without runtime errors; final staged implementation/evidence/status review.
- N/A: separate gameplay unit tests (package installation without new gameplay); XR session/input/Simulator/device/performance checks (no XR loader/rig configured in this installation-only task). These are pending later setup/runtime tasks and Phase checks, not waived for lack of hardware.
- Base: cc0110f49daa98de460c5ca3bf5ce89315c0dc3c. APK: none. Human approvals: none.
- Results: Android compile/configuration PASS; PlayMode 1 passed / 0 failed / 0 skipped. [M0-003 evidence](validation/m0/2026-10-02-m0-003.md).

## Retry / Failure History
- Applicability correction (parent explicitly authorized resume): commit 47206b9 incorrectly applied the Phase-level Quest requirement to M0-001. FOUNDATION and TEST-M0-001 do not require a connected Quest. Preserve the observation/count below as history; ENV-QUEST-NO-DEVICE is an unresolved dependency for M0-010 onward, not a present M0-001 failure. No counter was reset and no device retest occurred. M0-001 through M0-009 may proceed subject to each task's actual requirements; this does not authorize Phase PASS.
- M0-001 / ENV-QUEST-NO-DEVICE: failure_count=1, open, observed 2026-10-02T11:15Z. Authorized ADB daemon started successfully, but `adb devices -l` returned no devices. No repair/retest of this root cause attempted. Next: connect an available Quest 3 with approved development/USB debugging access, then explicitly resume. Evidence: validation/m0/2026-10-02-environment-preflight.md.
- PREFLIGHT / ENV-SANDBOX-HOST-ACCESS: failure_count=1, resolved for checked operations. Initial Git Schannel credential and ADB daemon operations failed in the sandbox; permission-reviewed host executions succeeded. This did not validate Quest connectivity or any M0 task. Counts retained.

For each open cause, record: task ID, stable root-cause ID, failure_count, attempted fixes, evidence path, last update, and next action.
Counts persist across sessions. A diagnostic commit does not check off a task or clear a blocker.

## Completed Tasks
- M0-003: FOUNDATION PASS; initial OpenXR 1.18.0 installed, Android compile and foundation smoke verified.
- M0-002: FOUNDATION PASS; Android settings and PlayMode verified, evidence validation/m0/2026-10-02-m0-002.md.
- M0-001: FOUNDATION PASS; see docs/validation/m0/2026-10-02-m0-001.md. Result becomes durable with the implementation/evidence/status commit.

## Performance Targets
- Acceptance: sustained 90Hz; application CPU and GPU frame times each < 11.1ms (aim for 9ms)
- Emergency floor: application FPS < 72 or CPU/GPU frame time > 13.9ms -> STOP/BLOCKED, not an alternative target
- Measurements and classification: TEST_PLAN.md Performance Acceptance
- Draw calls: < 150/eye (warning >= 250, hard limit 300)
- Visible triangles: < 300k ideal, ~325k target (hard limit 750k)
- Texture memory: < 400MB (hard limit 512MB)
- GC Alloc during gameplay: 0 B/frame
- Physics: primitive colliders only, max 30 Rigidbodies, max 50 Colliders
- Particles: max 200 simultaneous
- Audio: < 16 simultaneous AudioSources

## Architecture
- Data-driven weapons (ScriptableObject WeaponDefinition)
- Data-driven enemies (ScriptableObject EnemyDefinition)
- State-machine AI (IEnemyState)
- Event-driven combat (IDamageable + events)
- Object pooling (projectiles, effects, enemies)
- Strategy pattern weapons (IWeapon + WeaponFactory)

## Forbidden
- FindObjectOfType / FindObjectsByType in Update
- Camera.main in hot paths
- Instantiate/Destroy during combat
- LINQ in Update
- new List<>() / new GameObject() in Update
- String concatenation in Update
- Debug.Log in Update (release)
- Empty Update() methods
- Runtime shader creation
- Realtime GI / SSAO / Motion Blur / HDR
- MeshCollider
- Standard shader
- Multi-Pass rendering
- OpenGLES

## Automated Test Results
- Environment only: exact installed Editor 6000.3.25f1_e1dba0a9aba4; license entitlement resolved; isolated-from-repository batch startup log reports successful exit (0).
- M0-001: compile/configuration validation PASS, PlayMode 1/1 PASS; separate gameplay unit tests N/A (no gameplay logic). TEST-M0-001 PASS. Prior preflight-only block was a corrected applicability error.
- M0-002: Android-target compile and settings assertions PASS; PlayMode 1/1 PASS. M0-004 through M0-013 remain TODO. M0-003 package import/compile and PlayMode 1/1 PASS. TEST-M0-002 (APK build) not run yet; TEST-M0-003 through TEST-M0-005/device performance remain pending with no available Quest.

## Human Feedback
(none yet - see docs/HUMAN_FEEDBACK.md)

## Blockers
- Deferred device dependency ENV-QUEST-NO-DEVICE: no ADB device returned by the successful host-level query. Blocks M0-010 onward and Phase handoff, not M0-001 through M0-009 solely on that basis. Prior stop interpretation is corrected above, with history retained.
- Meta VR CLI was not discoverable on PATH; no callable Unity/XR Operator integration was exposed. XR Simulator/Meta SDK are not installed in this foundation repository; installation and compatibility remain unattempted M0 work, not PASS.
- Parent explicitly authorized M0-001 resume and eligible work through M0-009. M0-010 onward still requires Quest connectivity. Human Gates remain NOT_READY; M1 is not authorized.
