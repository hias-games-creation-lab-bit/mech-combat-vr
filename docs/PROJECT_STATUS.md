# PROJECT STATUS

## Platform
- Unity 6.3 LTS
- Quest 3 only
- Android ARM64
- IL2CPP
- Vulkan
- OpenXR + Multi-View
- URP (Forward)
- Meta XR SDK v207+

## Current Phase
M0

## Current Task
M0-001 Unity project creation

## Autonomous
YES

## Human Gate
NO

## Tasks

### M0 - Technical Foundation
- [ ] M0-001 Create Unity 6 project (Universal 3D / URP)
- [ ] M0-002 Configure Android build settings (IL2CPP, ARM64, Vulkan, minSdk 32, targetSdk 34)
- [ ] M0-003 Install OpenXR Plugin (compatible latest stable)
- [ ] M0-004 Install Meta XR SDK All-in-One v207+
- [ ] M0-005 Run Meta XR SDK Project Setup Tool - apply all fixes
- [ ] M0-006 Configure URP Asset (Forward, MSAA 4x, HDR Off, Post Processing Off)
- [ ] M0-007 Set 90Hz target in OVRManager
- [ ] M0-008 Install Meta VR CLI + XR Operator via Meta XR SDK AI Tools
- [ ] M0-009 Build APK for Quest 3
- [ ] M0-010 Deploy to Quest 3 and verify launch
- [ ] M0-011 Verify head tracking works
- [ ] M0-012 Verify both controllers detected
- [ ] M0-013 Git initial commit

### M1 - VR Cockpit
- [ ] M1-001 Create cockpit geometry (box prototype, 4 walls + ceiling)
- [ ] M1-002 Set cockpit scale (seat at ~15m height, interior 1:1 human)
- [ ] M1-003 Lock cockpit to head tracking (100% locked, world moves)
- [ ] M1-004 Left hand input -> mech movement (forward/back/turn)
- [ ] M1-005 Implement yaw turn rate limit (25 deg/sec default, inspector tunable)
- [ ] M1-006 Implement pitch lock (no pitch from movement)
- [ ] M1-007 Implement acceleration limit (< 5 m/s^2, inspector tunable)
- [ ] M1-008 Right hand weapon mount (controller forward = barrel)
- [ ] M1-009 Trigger -> fire pooled projectile (80 m/s)
- [ ] M1-010 Add ground plane (200m x 200m) + scale cue boxes (3-5)
- [ ] M1-011 Basic vignette on boost (inspector tunable)
- [ ] M1-012 5-minute comfort test on XR Simulator
- [ ] M1-013 Quest 3 build + deploy
- [ ] M1-014 Git commit

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
- [ ] M2-015 5-minute combat session test
- [ ] M2-016 Quest 3 build + deploy
- [ ] M2-017 Git commit

### M3 - Feel
- [ ] M3-001 Weapon-specific haptic patterns (Rifle/Cannon/Missile distinct)
- [ ] M3-002 Directional hit haptics (left/right controller based on hit direction)
- [ ] M3-003 Boost haptic (continuous low amplitude pulsing)
- [ ] M3-004 Muzzle flash (point light 0.05 sec on cockpit)
- [ ] M3-005 Enemy stagger on cannon hit
- [ ] M3-006 Placeholder SFX (Freesound.org CC0): fire, impact, enemy react, cockpit ambient
- [ ] M3-007 Cockpit bob on mech walk (2-4cm, 1-2 deg roll, inspector tunable)
- [ ] M3-008 Speed lines / dust particles on boost
- [ ] M3-009 Quest 3 build + feel test
- [ ] M3-010 Git commit

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
- [ ] M4-012 Full 5-minute stage playthrough test
- [ ] M4-013 Performance check (draw calls, tri, GC, frame time)
- [ ] M4-014 Quest 3 build + full playtest
- [ ] M4-015 Git commit

## Completed Tasks
(none yet)

## Performance Targets
- Target: 90Hz (11.1ms frame budget, aim for 9ms)
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
(not yet tested)

## Human Feedback
(none yet - see docs/HUMAN_FEEDBACK.md)

## Blockers
(none)
