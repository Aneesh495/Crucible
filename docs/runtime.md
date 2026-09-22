# Runtime model

## Virtual time

`VirtualClock` advances only when the scheduler jumps to the next delivery or
timer deadline. Processes read `ISimClock.Now`; they cannot observe wall time.

## Network

`SimNetwork` models:

- Per-link latency ranges (sampled from `ISimRandom`)
- Drop and duplicate probabilities
- FIFO or reorderable delivery
- Symmetric partitions via blocked `(from, to)` pairs

Messages become deliverable when `DeliverAt <= Now`. Reorderable links may
permute due messages to the same inbox; the schedule oracle chooses permutations
during exploration.

## Processes

Each `ISimProcess` has a lifecycle:

```
Stopped -> Running -> (Crashed -> Restart)*
```

`ISimContext` exposes timers, network send, storage, logging, and explicit
`Choose` points for internal nondeterminism.

## Enabled transitions

On each step the runtime collects:

| Kind | When |
|------|------|
| `ApplyFault` | Fault due at current step or time |
| `DeliverMessages` | In-flight messages are due |
| `FireTimers` | Timer heap head is due |
| `RunProcessStep` | Inbox non-empty for a running process |
| `DriveClient` | Workload registered a client driver |

The oracle picks one transition index. This is the primary exploration surface
for DFS and PCT.

## Shared memory

`SimSharedMemory` backs lock-free structure tests. Each load, store, and CAS
records a history invocation and yields a scheduling point, so explorers can
interleave shared-memory operations without OS threads.

## Persistence

`ISimStorage` is a per-node byte store. Raft persists JSON snapshots of term,
vote, log, and applied state machine keys.
