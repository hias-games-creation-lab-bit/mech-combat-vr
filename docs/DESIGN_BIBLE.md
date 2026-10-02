# DESIGN BIBLE

Visual identity and design language for BFTK VR.
All art generation (AI concept, 3D models, UI, effects) must follow this document.

---

## World Setting

- Near-future military science fiction
- Giant mechs (18m class) are primary weapons
- Industrial, utilitarian aesthetic - these are machines of war, not fashion
- Atmosphere: tense, high-stakes combat
- No fantasy/magic elements

## Design Language

### Mechanical Style
- Industrial-military engineering (bolts, panels, vents, cables visible)
- Functional design - every part looks like it has a purpose
- Heavy, grounded, weight is visible in the design
- Prototype/experimental feel (not mass-produced toy)
- Weathering: light battle damage, dust, scratches (NOT rusty/broken/tattered)

### Silhouette Rules
- Every mech must have a unique, instantly readable silhouette at 20m+ distance
- Wide shoulders, defined waist, heavy legs as baseline
- Asymmetric weapon loadout encouraged (different arms = different weapons)
- No wings, no capes, no flowing cloth
- Boss must be visually dominant - larger silhouette than player mech

### Scale Cues
- Mech height: 18m
- Cockpit: human-scale interior (1:1)
- Ground objects (buildings, vehicles, trees) must reinforce giant scale
- Enemy mechs: same 18m class (boss may be larger)

## Color Palette

### Player Mech
- Primary: Dark gray / charcoal
- Secondary: White / light gray
- Accent: Yellow-orange (warnings, highlights)
- Cockpit interior: Dark with cyan (#00FFCC) instrument glow

### Enemy Mechs
- Grunt: Military green / olive drab
- Lancer: Dark red / maroon (aggressive, fast)
- Support: Blue-gray / steel (distant, cold)
- Must be distinguishable from player at combat distance

### Boss
- Primary: Black / dark metallic
- Accent: Red-orange (threat, heat, danger)
- Damage states: exposed internals glow orange/yellow

### Forbidden Colors/Styles
- No bright pink, neon green, or pastel colors
- No tattered/ragged clothing or organic decay
- No rusty brown as primary color
- No overly clean/shiny chrome (too toylike)

## HUD / UI

- Primary color: Bright cyan #00FFCC
- Warning color: Amber/orange
- Critical color: Red
- Background: Translucent dark, never fully opaque
- Font style: Military/technical monospace
- All UI must be diegetic (part of the cockpit world, not screen overlay)

## Cockpit Design

### Must Have
- Physical window frames with thickness (5cm+)
- Visible instrument panels (left: mech status, right: weapons)
- Physical console below (switches, warning lights)
- Scratches on canopy glass
- Cables and structural elements visible
- Ambient cockpit lighting (dim, functional)

### Must NOT Have
- Transparent/invisible floor (breaks presence)
- Floating holographic UI with no physical projector
- Clean/sterile lab aesthetic
- Overly cluttered - must maintain forward visibility

## Weapons

### Visual Differentiation
Each weapon must look and feel different, not just different damage numbers.

| Weapon | Visual Style | Muzzle Effect |
|--------|-------------|---------------|
| Rifle | Compact, barrel-mounted | Quick flash, tracer line |
| Cannon | Large bore, heavy mount | Massive flash + smoke + cockpit shake |
| Missile Pod | Multi-tube shoulder rack | Lock-on HUD + trail + explosion |

## Enemy Visual Design

### Behavioral Readability
Player must be able to identify enemy type by silhouette alone.

| Enemy | Visual Cue | Why |
|-------|-----------|-----|
| Grunt | Standard build, visible weapon | "I can fight this" |
| Lancer | Lean, forward-tilted, blade/claw | "This one's fast and close" |
| Support | Wide stance, large backpack/cannon | "Stay away from that" |

### Boss Visual Phases
| Phase | Visual Change |
|-------|--------------|
| 1 | Full armor, imposing |
| 2 | Armor breaking, internals visible, new weapon exposed |
| 3 | Heavy damage, core exposed, desperation attacks |

## Effects

### Muzzle Flash
- Point light on cockpit interior (0.05 sec)
- Simple additive particles (4 quads, not volumetric)
- Weapon-specific color/size

### Explosions
- Bright core + expanding smoke
- Short duration (< 1 sec)
- No overlapping transparent particles > 15

### Hit Feedback
- Sparks on armor (small, bright)
- Enemy staggers 0.2m
- Damage state change at HP thresholds

### Environment
- Dust particles on boost
- Speed lines at high speed
- Rain/debris on canopy (optional, M5+)

## Animation Priority

### Must Have (M4)
- Enemy: idle, aim, fire, recoil, stagger, death
- Boss: phase transitions, attack telegraphs, damage states
- Player weapons: fire, reload

### Nice to Have (M5+)
- Mech walking animation
- Cockpit instrument animations
- Environmental animations (flags, smoke stacks)

### NOT Required for MVP
- Facial animation
- Cutscene animation
- Complex particle physics

## AI Generation Guidelines

### Concept Art Prompts Must Include
- "military industrial science fiction mech"
- Specific height/scale reference
- Weapon type and placement
- "readable silhouette for VR game"
- "no copyrighted IP resemblance"

### Concept Art Prompts Must NOT Include
- Specific copyrighted mech names (Gundam, Armored Core, etc.)
- "anime style" (unless explicitly approved)
- "cute" or "chibi"

### 3D Generation Guidelines
- Target poly count in specification
- Specify asymmetric weapon if applicable
- Request industrial/military surface detail
- Avoid organic/smooth surfaces for mechs

## Reference Mood (NOT to copy, for direction only)

### Feel References
- "Bonds of the Battlefield" cockpit immersion
- "Iron Rebellion" physical cockpit + VR controls
- "Vox Machinae" scale and weight
- "Armored Core" industrial mech design language (NOT to copy designs)

### Visual Tone
- Grounded military SF, not super robot
- "War machine" not "hero robot"
- Functional, not fashionable

---

## Version History
- v1.0 (2026-10-02): Initial design bible
