# DEVELOPMENT RULES

## Code Quality
1. Zero GC Alloc in Update/LateUpdate/FixedUpdate. Cache everything in Awake().
2. No Camera.main, no FindObjectOfType, no GameObject.Find in hot paths.
3. No empty Update() - delete it.
4. All public floats for feel must be [Header("DIRECTOR TUNES IN VR")]. Never hardcode motion/camera/haptics/UI distance values.
5. Use ObjectPool for bullets/effects/enemies, StringBuilder for UI text.
6. Single Pass Instanced, URP/Lit or URP/Simple Lit only, Vulkan, IL2CPP ARM64.
7. Physics: primitive colliders only, layer-based collision matrix.
8. Frame budget: < 11.1ms at 90Hz. Profile with metavr perf capture.
9. No SendMessage or GetComponent in damage loops. Cache IDamageable on Start.
10. 1 ray per gun per frame maximum for raycasts.

## Project Safety
1. Never change Unity/SDK/package versions without director approval.
2. Never modify ProjectSettings blindly.
3. Never replace OpenXR with another XR runtime.
4. Never introduce PC-only rendering features.
5. Never optimize based on Editor performance. Quest hardware profiling is authoritative.
6. Never rewrite unrelated files.

## Design Boundaries
1. Every gameplay feature must have a test.
2. Every new system must expose tunable parameters via Inspector.
3. Every bug fix must include a regression test when practical.
4. If a value affects gameplay balance and is not specified, use a temporary placeholder, record it in PROJECT_STATUS.md, and do not treat it as final design.
5. Never silently change game design values.

## Error Recovery
If the same fundamental error occurs 3 times:
STOP.
Report:
- Error description
- What was attempted
- Why it failed
- Current project state
- Recommended human action

## Change Scope
When given a task, only modify files within the specified scope.
Do not modify files outside the scope without explicit approval.

## Testing Order
1. Compile
2. Unit Test
3. PlayMode Test
4. XR Simulator
5. Quest 3 Build
6. Quest 3 Real Device Test
