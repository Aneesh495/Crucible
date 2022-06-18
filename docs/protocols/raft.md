# Raft in Crucible

Implementation follows the usual roles (follower, candidate, leader) with
simulated timers for election and heartbeat.

## Messages

- `VoteRequest` / `VoteResponse` — leader election
- `AppendEntriesRequest` / `AppendEntriesResponse` — replication + heartbeat
- `ClientRequest` / `ClientResponse` — KV-style commands (`set`, `del`)
- `InstallSnapshot` — stub for snapshot transfer

## State

Durable state is stored under `raft.state` in `ISimStorage` as JSON:

- `currentTerm`, `votedFor`
- log entries `(term, index, command)`
- `commitIndex`, `lastApplied`
- materialized `stateMachine` map

## Safety checks

| Invariant | Property |
|-----------|----------|
| `raft-election-safety` | At most one leader per term |
| `raft-log-matching` | Identical index/term/command on all nodes |
| `raft-state-machine-safety` | Applied KV maps agree |

## Fault injection

`RaftScenario` applies mild link chaos and a split-brain partition/heal pair
mid-run. Exploration modes vary scheduling around delivery and timer ordering.

## Limits

This is a simulation reference, not a production Raft deployment. Snapshot
install is minimal; cluster membership changes are not modeled.
