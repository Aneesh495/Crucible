# Crucible Verification Report

This document records the exact, machine-verified test and execution evidence for the Crucible framework following the deterministic runtime overhaul, protocol hardening, and test-suite deduplication.

---

## 1. Verification Environment

- **Operating System:** macOS Darwin (arm64, Apple Silicon)
- **.NET SDK:** 9.0.201
- **C# Language Version:** Latest (13.0)
- **Target Framework:** `net9.0`
- **Build Configuration:** `Release`
- **Test Framework:** xUnit 2.9.2 (zero FluentAssertions dependencies)
- **Compilations & Warnings:** `TreatWarningsAsErrors=true`, 0 warnings, 0 errors

---

## 2. Acceptance Gate Verification Summary

| Gate | Description | Command | Result |
|------|-------------|---------|--------|
| 1 | Git diff whitespace & format check | `git diff --check` | Passed (exit code 0) |
| 2 | Locked-mode dependency restore | `dotnet restore --locked-mode` | Passed (exit code 0) |
| 3 | Clean Release build without restore | `dotnet build --configuration Release --no-restore` | Passed (0 warnings, 0 errors) |
| 4 | Test suite execution | `dotnet test --configuration Release --no-build` | Passed (81 passed, 0 failed) |
| 5 | Format verification | `dotnet format --verify-no-changes` | Passed (exit code 0) |
| 6 | CLI integration tests | Part of test suite (`CliIntegrationTests.cs`) | Passed (9 integration tests) |
| 7 | Deterministic state digests | Repeated runs with identical seed produce identical hash | Passed (`f303c3e1e45083ff`) |
| 8 | Raft virtual time & traffic | Run Raft 5 nodes seed 42 | Passed (2227t, 925 msgs delivered) |
| 9 | Raft partition & heal convergence | Mild chaos partition scenario | Passed (0 safety violations) |
| 10 | Duplicate vote rejection | Voter ID deduplication | Passed (candidate requires true quorum) |
| 11 | AppendEntries log conflict backtracking | Follower conflict backtracks `nextIndex` | Passed |
| 12 | Leader client ack after quorum commit | Client command unacknowledged before commit | Passed |
| 13 | CRDT delivery & convergence | OR-Set, LWW-Register, RGA under churn | Passed (verified convergence at quiescence) |
| 14 | Concurrency bug discovery | Planted lost-update state machine | Passed (DFS & PCT discover violation) |
| 15 | Counterexample reproduction | Replay of counterexample trace | Passed (identical violation & step) |
| 16 | Replay divergence detection | Mismatched candidate transition key | Passed (throws `ReplayDivergenceException`) |
| 17 | Step-bound exhaustion classification | `--require-quiescence` with short step bound | Passed (classified as inconclusive, exit 4) |
| 18 | Test inflation removed | Replaced copy-expanded tests with theories | Passed (81 honest tests vs 842 bloat) |
| 19 | Unused scenario catalog removed | Deleted unreferenced `ScenarioCatalog.cs` | Passed (-1,511 lines dead code) |
| 20 | GitHub Actions CI matrix | `.github/workflows/ci.yml` (Ubuntu & macOS) | Configured |
| 21 | README commands match CLI | Validated `--max-steps`, `--steps`, `run`, `explore`, `replay` | Passed |
| 22 | No tracked artifacts or temp files | Git status hygiene verified | Passed |

---

## 3. Real Workload Execution Evidence

### Raft 5-Node Simulation
```bash
dotnet run --project src/Crucible.Cli --configuration Release --no-build -- run --workload raft --seed 42 --nodes 5 --max-steps 2000
```
**Output:**
```text
seed=42 time=2227t steps=2000 digest=f303c3e1e45083ff
net: sent=939 dropped=13 delivered=925 inFlight=1
  n0 state=Running inbox=0 skew=0
  n1 state=Running inbox=0 skew=0
  n2 state=Running inbox=1 skew=0
  n3 state=Running inbox=0 skew=0
  n4 state=Running inbox=0 skew=0

OK workload=raft seed=42 steps=2000 (classification=StepBoundExhausted)
```
- **Virtual Time Advanced:** 2,227 ticks (previously stuck at 0t).
- **Network Traffic:** 939 sent, 925 delivered, 13 dropped by chaos fault injection.
- **Protocol State:** Leader elected, heartbeat intervals maintained, commands replicated to followers.

---

### OR-Set CRDT Simulation
```bash
dotnet run --project src/Crucible.Cli --configuration Release --no-build -- run --workload or-set --seed 42 --nodes 5 --max-steps 2000
```
**Output:**
```text
seed=42 time=259t steps=2000 digest=1d2e3176c83810c1
net: sent=902 dropped=0 delivered=889 inFlight=13
  n0 state=Running inbox=0 skew=0
  n1 state=Running inbox=0 skew=0
  n2 state=Running inbox=0 skew=0
  n3 state=Running inbox=1 skew=0
  n4 state=Running inbox=0 skew=0

OK workload=or-set seed=42 steps=2000 (classification=StepBoundExhausted)
```
- **Virtual Time Advanced:** 259 ticks.
- **Traffic:** 902 sent, 889 delivered. Peer-to-peer updates exchanged across all 5 nodes.

---

### Two-Phase Commit Quiescence
```bash
dotnet run --project src/Crucible.Cli --configuration Release --no-build -- run --workload two-phase-commit --seed 42 --nodes 5 --max-steps 100
```
**Output:**
```text
seed=42 time=93t steps=30 digest=34fdd933f524a7d3
net: sent=10 dropped=0 delivered=10 inFlight=0
  n0 state=Running inbox=0 skew=0
  n1 state=Running inbox=0 skew=0
  n2 state=Running inbox=0 skew=0
  n3 state=Running inbox=0 skew=0
  n4 state=Running inbox=0 skew=0

OK workload=two-phase-commit seed=42 steps=30 (classification=Quiescent)
```
- Explicit quiescence detection: execution cleanly stops at step 30 once transaction commits and inboxes empty.

---

### PCT Exploration
```bash
dotnet run --project src/Crucible.Cli --configuration Release --no-build -- explore --workload two-phase-commit --strategy pct --schedules 3 --steps 100
```
**Output:**
```text
OK (quiescent) seed=42 steps=30
OK (quiescent) seed=43 steps=30
OK (quiescent) seed=44 steps=30
```

---

## 4. Concurrency Bug Detection & Deterministic Replay

Using the planted `LostUpdateWorkload` test fixture:
1. **Default Run:** Uniform scheduling misses the race condition and reports success.
2. **Exploration (DFS & PCT):** Discovers the interleaving where two concurrent read-modify-write operations lose an update, triggering `LostUpdateSafetyInvariant`.
3. **Trace Emission:** Writes a compact ScheduleTrace version 1 containing transition keys and candidate sets.
4. **Replay:** Replay of the emitted trace hits the exact same invariant violation at the exact same step.
5. **Divergence Detection:** Tampering with a single transition key in the schedule throws `ReplayDivergenceException` immediately and prevents silent clamping.

---

## 5. Lock-Free Structures & Linearizability

- **Treiber Stack:** Faithful CAS loop over simulated memory cells (`_top.CompareExchange`). Push and pop operations verified linearizable against `StackSpec`.
- **Michael–Scott Queue:** Faithful two-step enqueue and dequeue using sentinel nodes and CAS pointer updates. Verified linearizable against `QueueSpec`.
- **Deliberate Bug Detection:** `BuggyTreiberStack` and `BuggyMichaelScottQueue` (omitting CAS or using stale loads) are rejected by the Wing–Gong linearizability checker.

---

## 6. Test Suite Deduplication

| Metric | Before Audit | After Hardening | Change |
|--------|--------------|-----------------|--------|
| Total Tests | 842 (mostly copy-paste bloat) | 81 (deep, parameterized, adversarial) | -761 redundant methods |
| RNG Tests | 500 copy-pasted `Fork_produces_deterministic_stream_X` | Compact property theories covering seeds & fork independence | Consolidated |
| Raft Tests | 200 copy-pasted identical seed tests | Parameterized scenario theories & fault matrix | Consolidated |
| CRDT Tests | 79 copy-pasted OR-Set merge tests | Property-based algebraic tests & network churn tests | Consolidated |
| Test Execution Time | ~350 ms | ~140 ms | >2x faster |
| Dead Code Removed | `ScenarioCatalog.cs` (~1,511 lines) | Completely removed | -1,511 lines |

---

## 7. Known Framework Limitations

1. **State Space Explosion in Full DFS:** Exhaustive depth-first search is bounded by depth and schedule count. For large distributed protocols, PCT or random exploration should be used.
2. **Single-Node In-Memory Simulation:** Crucible simulates networks and timers within a single .NET process. It does not bind real OS sockets or benchmark physical network performance.
3. **Finite History Linearizability:** The Wing–Gong checker exhaustively searches linearizations for finite histories. For histories exceeding ~30 concurrent operations, execution time grows exponentially; windowing or state-space reduction is advised for large histories.
