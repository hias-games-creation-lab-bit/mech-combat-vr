# GAME DESIGN

## Concept
"Bonds of the Battlefield"-inspired VR cockpit combat game
Quest 3 exclusive, single-player, seated

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
- Haptic: amplitude 1.0, 120ms + cockpit shake
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
- Yaw turn rate: 25-45 deg/sec max (25 for heavy mech feel)
- Pitch: LOCKED (no pitch from movement)
- Forward acceleration: < 5 m/s^2
- Max speed: 8-12 m/s (boost with vignette only)
- Cockpit bob: 2-4cm up/down, 1-2 deg roll max
- Shake: 10-20Hz, < 0.5cm, < 0.5 deg, bursts < 0.4 sec
- Vignette on boost: darken 15% periphery, 0.3 sec fade
- Head bob LIMIT: > 5cm or > 5 deg = instant sickness
- Continuous shake > 2 sec = forbidden

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
