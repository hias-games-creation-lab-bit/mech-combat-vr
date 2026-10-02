# ARCHITECTURE DECISION RECORDS

Record of key decisions and their reasoning. AI must not reverse these without Director approval.

---

## ADR-001: Unity 6.3 LTS
- Date: 2026-10-02
- Decision: Use Unity 6000.0.x LTS (not Tech Stream)
- Reason: Meta XR SDK v207 requires 6000.0.66f2+. LTS = most stable.

## ADR-002: URP (not Built-in)
- Date: 2026-10-02
- Decision: URP with Forward rendering
- Reason: Meta official requirement for Quest. Built-in is deprecated.

## ADR-003: OpenXR (not Oculus XR Plugin)
- Date: 2026-10-02
- Decision: OpenXR + Meta Quest Support
- Reason: Oculus XR Plugin deprecated. OpenXR is Meta's current recommendation.

## ADR-004: Vulkan (not OpenGLES)
- Date: 2026-10-02
- Decision: Vulkan as primary graphics API
- Reason: 15-20% faster on Quest 3 vs OpenGLES.

## ADR-005: 90Hz target (not 72Hz or 120Hz)
- Date: 2026-10-02
- Decision: Lock to 90Hz
- Reason: 72Hz too laggy for cockpit turning. 120Hz budget too tight for solo dev.

## ADR-006: All weapons use physics projectiles (no hitscan for player)
- Date: 2026-10-02
- Decision: Pooled physics projectiles for all player weapons
- Reason: Meta AI stated "VR needs visible bullet travel." Rifle at 80m/s is near-instant but visible.

## ADR-007: State Machine AI (not Behavior Tree)
- Date: 2026-10-02
- Decision: ScriptableObject State Machine for enemy AI
- Reason: Simplest for Codex to generate. Behavior Trees are overkill and harder to debug.

## ADR-008: Cockpit camera is 100% head-locked
- Date: 2026-10-02
- Decision: Cockpit never moves independently of player head
- Reason: VR comfort. World moves, cockpit stays fixed.

## ADR-009: No hand tracking (controller only)
- Date: 2026-10-02
- Decision: Touch Controller only for initial release
- Reason: Cockpit combat needs trigger, stick, haptics. Hand tracking loses tracking near cockpit frame.

## ADR-010: Mech scale 18m
- Date: 2026-10-02
- Decision: Player and enemy mechs are 18m tall
- Reason: Gundam/Bonds of the Battlefield scale. 3-layer scale design (cockpit 1:1, arms 5-10m, world 20m+).

## ADR-011: Dots as PM, Codex as Engineer
- Date: 2026-10-02
- Decision: Dots manages project, Codex writes code. Not reversed.
- Reason: Dots is designed for orchestration, Codex for implementation.

## ADR-012: Git is Source of Truth (not Dots memory, not Space)
- Date: 2026-10-02
- Decision: All project state lives in Git repository markdown files
- Reason: AI memory is volatile. Git survives thread/session changes.

## ADR-013: Art pipeline uses best tool per asset (not locked to single tool)
- Date: 2026-10-02
- Decision: Choose from ChatGPT, Meshy, Tripo, Blender 5.0, Asset Store based on quality
- Reason: Quality is priority. No single tool is best for everything.

## ADR-014: Animation postponed until M3+
- Date: 2026-10-02
- Decision: M2 enemies use translate/rotate/aim only. No walking animation.
- Reason: VR cockpit view = enemies at distance. Simple animation sufficient for combat prototype.
