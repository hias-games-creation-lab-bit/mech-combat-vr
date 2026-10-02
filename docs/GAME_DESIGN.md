# GAME DESIGN

## Concept
"Bonds of the Battlefield"-inspired VR cockpit combat game
Quest 3 exclusive, single-player, seated

## Game Overview
18mの巨大メカのコックピットに座り、左手でレバー操作、右手で武器を構えて敵メカと戦うVR戦闘ゲーム。
3種類の武器（ライフル/キャノン/ミサイル）を使い分け、3種類の敵（射撃型/近接型/スナイパー型）とボスを倒して5分間のステージをクリアする。
コックピットの没入感、敵との駆け引き、射撃の爽快感がすべて等しく重要。
グラフィックやストーリーより「操縦している感覚」と「撃って気持ちいい」を最優先する。

## Core Experience
Piloting a giant mech from inside its cockpit.
The game is about FEEL, not features.

## Mech Scale
- Mech height: 18m (Gundam scale)
- Cockpit seat height from ground: ~15m
- Cockpit interior: 1:1 human scale (0.5-1.2m from eyes)
- Mech arms/legs visible: 5-10m from eyes
- Ground buildings: 5-15m (visible below)
- Enemy mechs: same 18m scale

## Controls
- Left hand: mech movement/steering (virtual lever mapped to controller position)
- Right hand: weapons (controller forward = gun barrel, trigger = fire)
- Buttons: weapon switch

## Weapons (3 types)

### Rifle
- Role: Workhorse
- Speed: 80 m/s pooled physics projectile (NOT hitscan - VR needs visible bullet travel)
- Damage: Medium
- Heat: Low
- Haptic: amplitude 0.7, 30ms
- Fire rate: 150ms interval

### Cannon
- Role: Anti-armor
- Speed: 25 m/s (must lead target)
- Damage: High
- Heat: High
- Haptic: amplitude 1.0, 120ms + visual-only secondary-part shake (see VR Comfort Limits; never shake the tracked camera)
- Fire rate: 800ms interval

### Missile Pod
- Role: Stagger/utility
- Speed: 15 m/s homing (1.5 sec lock-on)
- Damage: Low (but staggers enemy)
- Haptic: Lock-on feel
- Fire rate: 1200ms interval

## Enemies (3 types)

### Grunt (Ranged)
- Behavior: Strafes at 15-25m, stays in front 120-degree arc
- Teaches: Aiming
- HP: Low-Medium

### Lancer (Melee)
- Behavior: Boosts to 5m, circles at 60 deg/sec
- Teaches: Turning/tracking
- HP: Low

### Support (Sniper)
- Behavior: Stays at 40m+, telegraphed 2-sec charge shot
- Teaches: Spacing/cover
- HP: Medium

## Boss
- 3 phases x 90 seconds
- Targetable parts (arms, core) - each break changes behavior
- Attacks telegraphed: 1.5 sec sound + laser pointer
- Big, slow, readable - NOT fast-moving
- NOT a bullet sponge

## Stage
- ~5 minutes per stage
- Flow: 2 enemies -> 3 enemies -> strong enemy -> enemy group -> boss -> clear

## TTK (Time-to-Kill)
- Grunt: 5-8 seconds of accurate fire (10-12 sec with 50% miss rate)
- Boss total: ~4.5 minutes (3 phases x 90 sec)

## Balance Rule
If a value affects gameplay balance and is not specified:
1. Use a temporary placeholder value
2. Record it in PROJECT_STATUS.md
3. Do not treat it as final design
4. Director tunes all feel values in VR headset

## VR Comfort Limits

### Reference frame and tracking
- The cockpit reference shell and seat anchor are fixed to the mech/vehicle, NOT the tracked head or camera (ADR-008).
- The XR Origin is anchored to the seat. Preserve tracked head translation and rotation at 1:1 scale within the cockpit.
- Leaning changes the head's position relative to the cockpit and produces parallax. Looking around does not rotate the cockpit with the head.
- Artificial locomotion moves/turns the mech and its seat/cockpit together. The world appears to move relative to the seated player; do not implement this by attaching the cockpit to the camera.
- Seated play does not mean frozen head position. Never clamp physical head pitch, roll, yaw, or lean to enforce locomotion limits.

### Artificial locomotion and effects
- Yaw turn rate: 25-45 deg/sec max (25 for heavy mech feel).
- Pitch/roll from artificial vehicle movement: LOCKED. Tracked head pitch/roll remains active.
- Forward acceleration: < 5 m/s^2.
- Max speed: 8-12 m/s (boost with vignette only).
- Walk bob: optional visual-only motion of designated secondary mech/cockpit parts, 2-4cm up/down and 1-2 deg roll max when enabled. It must not move the reference shell, canopy, seat anchor, XR Origin, or camera.
- Shake: visual-only secondary-part effect, 10-20Hz, < 0.5cm, < 0.5 deg, bursts < 0.4 sec. The same exclusions apply to cannon recoil and damage feedback.
- Artificial camera/head bob and camera shake: forbidden. Physical tracked head motion is not a bob/shake effect and is not limited by these effect amplitudes.
- Vignette on boost: darken 15% periphery, 0.3 sec fade.
- Continuous shake > 2 sec = forbidden.
- Bob/shake effects default to off until Director tuning in headset. Keep effect amplitudes Inspector-tunable.
- Numerical limits are design constraints, not a guarantee against sickness. Simulator tests cannot approve comfort; the Director must evaluate the candidate on Quest 3 and stop if uncomfortable.

## Cockpit UI Layout
- Layer 1 (0.8m): Diegetic instruments (ammo, heat) - world-space canvas
- Layer 2 (1.5m): Canopy HUD (reticle, lock-on, distance) - center 20% FOV
- Layer 3 (infinity): Environmental markers
- Color: bright cyan #00FFCC (avoid pure red/blue)
- No critical info at > 40 degrees from center (FFR blur)

## Stage (Prototype)
- Arena: 200m x 200m flat plane
- Scale cues: 3-5 building-shaped boxes (5-15m tall)
- Full stage design deferred to M4+

## Sound Asset Strategy
- M2: Placeholder SFX from Freesound.org (CC0/CC-BY)
- M4+: Evaluate AI-generated audio (Stable Audio etc.)
- No self-recording (solo dev time constraint)

## Weapon Implementation
- ALL weapons use pooled physics projectiles (no hitscan for player)
- Enemy weapons use hitscan with tracer line (cheaper)
- Projectile tunneling prevention: raycast between prev and current pos

## What Director Must Tune in VR (not AI)
- All haptic amplitudes and durations
- Turn rate, bob amount, shake amount, vignette strength
- Reticle size, HUD distance, audio volume balance
- TTK - play 50 times, adjust by 10% increments
