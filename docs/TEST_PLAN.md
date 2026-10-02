# TEST PLAN

## Test Ownership and Evidence

- Technical assertions are checked by Codex/operator using tests, logs, telemetry, and captures. Human assertions belong to the Director/Device Gates in PROJECT_STATUS.md.
- For sensory tests (haptics, sound, comfort, feel), automated evidence verifies the produced command/signal/event; only the Director confirms the perceived result on Quest 3.
- Cases with both parts report them separately. A technical PASS never implies human PASS. Missing required technical evidence is BLOCKED, not PASS or N/A.
- Save sanitized reports under `docs/validation/<phase>/`. Record test IDs, source revision/fingerprint, build checksum when applicable, environment, commands, timestamps, measurements, results, and evidence location.
- Task-level applicability follows AGENTS.md and AUTONOMOUS_DEVELOPMENT.md. A required Phase technical test cannot be waived because a tool/device is unavailable.

## Performance Acceptance

This section defines how the 90Hz acceptance target and 72 FPS emergency floor are interpreted. Other budgets remain those in AGENTS.md.

### Measurement
- Quest 3 standalone measurements are authoritative; Editor/XR Simulator FPS is not evidence of Quest performance.
- Use a 90Hz display mode and measure application rendering, not only the compositor refresh rate or reprojected frames.
- Record application CPU and GPU work time separately. Each must meet the frame-time limit; do not add them together or use a frame interval containing refresh waits as CPU work time.
- For a five-minute session, warm up/loading finishes before the measured continuous 300-second gameplay interval. Do not remove gameplay spikes or substitute a whole-session average for checking breaches.
- Record capture tool/version, build/source identity, scene/load, rendering settings, sampling resolution, FPS/dropped-frame data, CPU/GPU times, draw calls, GC, and memory. Include time-series evidence or a reproducible reference, not just a PASS label.

### Technical PASS
- Sustained application rendering at 90Hz with no application-missed frames during the measured interval.
- Application CPU and GPU frame times each < 11.1ms throughout the measured gameplay samples.
- Other applicable acceptance budgets are met, including draw calls < 150/eye, GC allocation 0 B/frame during gameplay, and texture memory < 400MB.

### FAIL versus immediate STOP/BLOCKED
- Missing 90Hz or exceeding an acceptance budget is FAIL: no feature commit and no Phase Technical PASS, even when application FPS remains at or above 72.
- Application FPS < 72, or application CPU/GPU frame time > 13.9ms, is an emergency breach: stop the test safely, record BLOCKED, and report to the Director.
- Existing hard-stop rules also apply: draw calls > 300/eye, gameplay GC allocation > 0 B/frame, texture memory > 512MB, or another hard-limit breach in AGENTS.md.
- Exactly 72 FPS is not PASS. A 72-89 FPS result is still FAIL even if it never crosses the emergency floor.
- Unavailable required performance telemetry/device is BLOCKED. Do not invent results, lower refresh rate, relax limits, or count reprojection as a substitute for application performance.

## M0 - Technical Foundation

### TEST-M0-001: Unity Project
Launch Unity project.
Expected: Exact Editor version 6000.3.25f1 matches ProjectSettings/ProjectVersion.txt and ADR-001; no errors; URP Forward is active for every used renderer/quality configuration (not Forward+ or Deferred).

### TEST-M0-002: Android Build
Build for Android/Quest 3.
Expected: APK generated, no build errors.

### TEST-M0-003: Quest Deploy
Install APK on Quest 3.
Expected: App appears in Unknown Sources.

### TEST-M0-004: Quest Launch
Launch app on Quest 3.
Expected: VR environment renders, head tracking works.

### TEST-M0-005: Controllers
Both controllers visible in VR.
Expected: Controller models appear, buttons responsive.

---

## M1 - VR Cockpit

### TEST-M1-001: Cockpit Presence
With the vehicle stationary, apply tracked head rotation and translation while seated.
Expected: Cockpit walls are visible; cockpit/seat transforms stay vehicle-relative. Head rotation does not rotate the cockpit. Leaning changes camera-to-cockpit position and produces parallax. Tracked head pitch/roll remains active (ADR-008).

### TEST-M1-002: Left Hand Movement
Move the left-hand virtual lever forward as defined in GAME_DESIGN.md.
Expected: Mech moves forward. Cockpit and seat remain fixed to the vehicle, not the head. Head motion remains independent; the world moves relative to the vehicle/seat viewpoint.

### TEST-M1-003: Left Hand Turning
Move the left-hand virtual lever to command a turn as defined in GAME_DESIGN.md.
Expected: Mech turns at <= 45 deg/sec. No sudden snap.

### TEST-M1-004: Right Hand Weapon
Pull right trigger.
Expected: Projectile fires from weapon barrel position (not from eyes).

### TEST-M1-005: Five-Minute Stability and Human Comfort
Technical part (AUTO, M1-012): Run XR Simulator for five minutes with movement and tracked head pose changes.
Expected: No crash/runtime error, no unintended cockpit/head coupling, and no artificial camera pitch/roll/bob. This is not a comfort verdict or Quest performance proof.
Quest technical performance checks run with M1-013 and must satisfy Performance Acceptance before the Phase Technical Gate passes.
Human part (HG-M1-DEVICE): After Technical Gate PASS, the Director wears Quest 3 and evaluates the same candidate for five minutes, stopping if uncomfortable. Record comfort/tracking feedback and an explicit decision in HUMAN_FEEDBACK.md. Agents leave this part WAITING until the Director decides.

---

## M2 - Combat Core

### TEST-M2-001: Enemy Spawn
Start mission.
Expected: Enemy appears at designated distance.

### TEST-M2-002: Enemy Movement
Enemy is active.
Expected: Enemy moves toward player or strafes. Not static.

### TEST-M2-003: Hit Detection
Shoot enemy.
Expected: Hit registers. Enemy HP decreases exactly once per hit.

### TEST-M2-004: Enemy Reaction
Hit enemy.
Expected: Visual feedback (sparks/stagger). Not just HP decrease.

### TEST-M2-005: Enemy Death
Reduce enemy HP to 0.
Expected: Enemy dies with feedback. Removed from scene (returned to pool).

### TEST-M2-006: Player Damage
Enemy shoots player.
Expected: Player HP decreases. Basic haptic feedback on both controllers (single pattern, detailed tuning in M3).

### TEST-M2-007: Player Death
Player HP reaches 0.
Expected: Death state. Restart within 3 seconds.

### TEST-M2-008: No Duplicate Damage
Projectile hits same frame.
Expected: Damage applied exactly once.

### TEST-M2-009: Basic Shoot Haptic
Fire weapon.
Expected: Controller vibrates on trigger pull (single basic pattern).

### TEST-M2-010: 5-Minute Session
Technical part (AUTO, M2-015; requires the M2-016 Quest build/deploy): Run the full combat loop on Quest 3 for a measured continuous 300 seconds after warm-up.
Expected: No crash/runtime errors; sustained application rendering at 90Hz; application CPU and GPU frame times each < 11.1ms; all applicable Performance Acceptance budgets met. Save the measured evidence.
FAIL: A result below the 90Hz acceptance target is not PASS merely because it remains at or above 72 FPS.
Emergency: Application FPS < 72 or CPU/GPU frame time > 13.9ms -> stop safely, BLOCKED, diagnostic/state-only commit, Director report.
Human part (HG-M2-DIRECTOR and HG-M2-DEVICE): After Technical Gate PASS, the Director wears Quest 3 and evaluates shooting feel and comfort for the same candidate. Record separate decisions; technical metrics cannot establish "no sickness".

### TEST-M2-011: GC Allocation
During combat gameplay.
Expected: 0 B/frame GC allocation (check with Profiler).

---

## M3 - Feel

### TEST-M3-001: Haptics Fire
Fire each weapon.
Expected: Distinct haptic pattern per weapon type.

### TEST-M3-002: Haptics Hit
Take damage.
Expected: Both controllers vibrate. Different from firing.

### TEST-M3-003: Sound
Fire weapon, hit enemy.
Expected: Muzzle sound, impact sound, enemy reaction sound.

### TEST-M3-004: Muzzle Flash
Fire weapon.
Expected: Brief light flash on cockpit interior.

### TEST-M3-005: Enemy Stagger
Hit enemy with cannon.
Expected: Visible stagger/knockback.

---

## M4 - Vertical Slice

### TEST-M4-001: 3 Weapons
Switch between all 3 weapons.
Expected: Each weapon fires differently. Haptics differ.

### TEST-M4-002: 3 Enemy Types
All 3 enemy types spawn.
Expected: Each behaves differently (range/melee/sniper pattern).

### TEST-M4-003: Boss Fight
Reach boss.
Expected: 3 phases. Targetable parts. Telegraphed attacks.

### TEST-M4-004: Full Stage
Complete 5-minute stage.
Expected: Start -> enemies -> boss -> victory -> result.

### TEST-M4-005: Performance Under Load
Boss fight with max particles/enemies.
Expected: Performance Acceptance is satisfied, including sustained application rendering at 90Hz and application CPU/GPU frame times each < 11.1ms. Record the actual enemy/particle counts and capture evidence. The 72 FPS emergency floor cannot be used as a PASS threshold.
