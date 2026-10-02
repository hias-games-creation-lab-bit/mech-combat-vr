# ARCHITECTURE DECISION RECORDS

Record of key decisions and their reasoning. AI must not reverse these without Director approval.

---

## ADR-001: Unity 6.3 LTS (6000.3.25f1)
- Date: 2026-10-02
- Decision: Use exactly Unity 6.3 LTS (6000.3.25f1).
- Reason: Keep the intended 6.3 LTS line and pin one reproducible Editor patch instead of mixing release series or selecting a moving "latest" version.
- Release reference: https://unity.com/releases/editor/whats-new/6000.3.25f1
- Enforcement: M0-001 creates the project with this exact Editor and records it in ProjectSettings/ProjectVersion.txt. AGENTS.md, PROJECT_STATUS.md, and README.md mirror this decision.
- Validation: M0 must verify the chosen SDK/package combination, Android build, Vulkan launch, and runtime logs. Compatibility is not assumed from the version number. Record failures and STOP rather than silently switching Editor versions.
- Known issue to assess in M0: Unity lists Vulkan swapchain timeout crash UUM-153744 for this release. This is a risk to validate, not a claim that the project reproduces it.
- Change policy: Any later Editor change requires Director approval and updates to this ADR, the three reference documents, and ProjectVersion.txt.

## ADR-002: URP (not Built-in)
- Date: 2026-10-02
- Decision: URP with Forward rendering only. Forward+ and Deferred are not approved.
- Reason: Use one rendering path for the prototype so configuration, tests, and performance measurements are comparable.
- Enforcement: M0-006 and TEST-M0-001 verify Forward on every active URP renderer/quality configuration. A change requires Director approval.

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
- Decision: Request 90Hz and require sustained application rendering at 90Hz, with application CPU and GPU frame times each < 11.1ms.
- Reason: Use a single performance acceptance target for cockpit combat.
- Emergency policy: Application FPS < 72 or CPU/GPU frame time > 13.9ms triggers STOP/BLOCKED. Remaining above 72 FPS does not constitute PASS.
- Validation: TEST_PLAN.md Performance Acceptance defines measurement and classification. Do not lower refresh rate or count reprojection as a way to pass.

## ADR-006: All weapons use physics projectiles (no hitscan for player)
- Date: 2026-10-02
- Decision: Pooled physics projectiles for all player weapons
- Reason: Meta AI stated "VR needs visible bullet travel." Rifle at 80m/s is near-instant but visible.

## ADR-007: State Machine AI (not Behavior Tree)
- Date: 2026-10-02
- Decision: ScriptableObject State Machine for enemy AI
- Reason: Simplest for Codex to generate. Behavior Trees are overkill and harder to debug.

## ADR-008: Vehicle-locked cockpit with independent tracked head motion
- Date: 2026-10-02
- Decision: The cockpit reference shell and seat anchor are fixed to the mech/vehicle, not to the tracked head or camera. The player's head moves freely within the cockpit using tracked position and rotation.
- Hierarchy: MechRoot owns CockpitRoot and SeatAnchor; SeatAnchor owns the XR Origin and tracked camera. Neither the cockpit nor the mech is parented to the camera. Keep cockpit/tracking scale at 1:1.
- Motion: Artificial locomotion moves/turns MechRoot. Physical head translation creates parallax against the cockpit; looking around does not rotate the cockpit. Movement-induced pitch/roll is prohibited, not tracked head pitch/roll.
- Effects: Bob/shake may animate only designated secondary visual parts. Never apply them to the tracked camera, XR Origin, seat anchor, or cockpit reference shell. See GAME_DESIGN.md VR Comfort Limits.
- Reason: Preserve a vehicle-relative cockpit reference while allowing natural head movement and lean parallax. Comfort still requires a human Quest 3 test.
- Supersedes: The previous head-locked cockpit instruction. M1-003 and TEST-M1-001/002 must implement and verify this corrected model.

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
