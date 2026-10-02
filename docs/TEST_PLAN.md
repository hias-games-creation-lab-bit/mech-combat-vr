# TEST PLAN

## M0 - Technical Foundation

### TEST-M0-001: Unity Project
Launch Unity project.
Expected: No errors, URP active.

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
Seated in cockpit, look around.
Expected: Cockpit walls visible in all directions. Parallax when leaning.

### TEST-M1-002: Left Hand Movement
Push left stick forward.
Expected: Mech moves forward. Cockpit stays locked to head. World moves.

### TEST-M1-003: Left Hand Turning
Push left stick left/right.
Expected: Mech turns at <= 45 deg/sec. No sudden snap.

### TEST-M1-004: Right Hand Weapon
Pull right trigger.
Expected: Projectile fires from weapon barrel position (not from eyes).

### TEST-M1-005: Comfort Test
Play for 5 minutes continuously.
Expected: No motion sickness. No frame drops.

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
Play full combat loop for 5 minutes.
Expected: No crash, no sickness, no frame drops below 72Hz.

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
Expected: 90Hz maintained. No frame drops.
