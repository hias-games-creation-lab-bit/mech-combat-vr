# ART ASSET REGISTRY

Project: BFTK VR - Mech Cockpit Combat
Target Platform: Meta Quest 3
Engine: Unity 6
Primary DCC: Blender 5.0
Art Automation: Art Thread + Codex

---

## Global Art Rules

### Performance
- No fixed triangle-count limit per asset. Optimize based on visible distance, simultaneous instances, material count, draw calls, texture memory, shader complexity, overdraw, CPU/GPU frame time.
- All final assets must be tested on Quest 3.

### Default Texture Targets
- Hero cockpit: 2K
- Player exterior: 1K-2K
- Enemy mech: 1K-2K
- Boss: 2K
- Background: 512-1K
- No 4K unless explicitly approved

### Default LOD Policy
- LOD0: hero/high detail
- LOD1: medium
- LOD2: low
- LOD3: silhouette only
- Thresholds based on camera distance + Quest 3 profiling

### Materials
- Minimize material slots
- Simple mobile-friendly shaders
- Avoid unnecessary transparency
- Avoid expensive real-time effects

---

## Asset Status Values
CONCEPT | GENERATING | GENERATED | RETOPOLOGY | TEXTURING | RIGGING | ANIMATION | UNITY_IMPORT | QUEST_TEST | APPROVED | NEEDS_REVIEW | REJECTED

---

## Assets

### PLAYER_MECH_001
- Purpose: Main player combat mech (CRITICAL)
- Height: 18m
- Cockpit visibility: CRITICAL
- Style: Military-industrial SF
- LOD0: 40k-80k tri, LOD1: 20k-40k, LOD2: 8k-20k, LOD3: 3k-8k
- Texture: 2K primary
- Source: AI concept -> 3D generation -> Blender
- Status: CONCEPT

### PLAYER_COCKPIT_001
- Purpose: First-person VR cockpit interior (CRITICAL)
- Requirements: 1:1 human scale, VR comfort, controls within reach, clear forward visibility
- LOD: 40k-80k tri (constant visibility, aggressive culling behind player)
- Texture: 2K
- Status: CONCEPT

### ENEMY_MECH_A_001 (Grunt/Ranged)
- LOD0: 12k-20k, LOD1: 8k-12k, LOD2: 3k-6k, LOD3: 1k-3k
- Animation MVP: rotation, translation, aiming, recoil, simple attack (no walking required)
- Status: CONCEPT

### ENEMY_MECH_B_001 (Lancer/Melee)
- Same tri targets as Enemy A
- Status: CONCEPT

### ENEMY_MECH_C_001 (Support/Sniper)
- Same tri targets as Enemy A
- Status: CONCEPT

### BOSS_MECH_001
- Height: 18m+
- 3 combat phases, targetable parts, damage states
- LOD0: 50k-100k, LOD1: 30k-50k, LOD2: 10k-20k, LOD3: 3k-8k
- Texture: 2K
- Status: CONCEPT

### ARENA_001
- Total visible scene target: 100k-200k tri
- Modular components, aggressive LOD, occlusion opportunities
- Status: CONCEPT

---

## Automation Pipeline

```
Concept → Candidate generation → AI evaluation → Human selection
→ Image-to-3D / Text-to-3D → Remesh / Retopology
→ Blender cleanup → LOD generation → Texture optimization
→ FBX/GLB export → Unity import → Prefab generation
→ Quest 3 test → Approval
```

## AI Evaluation (Auto-reject criteria)
- Wrong silhouette / scale / visual style
- Excessive complexity / broken geometry
- Missing major components
- Inconsistent design language
- Poor first-person readability

## Approval Gate
- [ ] Visual design approved
- [ ] Geometry validated
- [ ] Materials validated
- [ ] LOD validated
- [ ] Unity import validated
- [ ] Quest 3 performance tested
- [ ] License recorded
- [ ] Source files archived

## File Naming
`CATEGORY_NAME_VERSION` (e.g., PLAYER_MECH_001_v03.fbx)
Never overwrite approved assets.

## Source/License Record (per asset)
Source, Generator, Account/Plan, Creation date, License, Commercial-use status

## Art Pipeline Principle
AI generates. Codex automates. Blender transforms. Unity integrates. Quest validates. Director approves.
