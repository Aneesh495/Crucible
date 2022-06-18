# CRDT workloads

Three CRDT types are included as gossip-friendly replicated state:

## OR-Set

Observed-remove set with unique tags per add. Remove tombstones `(element, tag)`.
Merge is set union on tags; read returns elements with a live tag.

## LWW-Register

Last-writer-wins by `(timestamp, nodeId)` tuple ordering. Suitable for single-value
convergence tests under reordering.

## RGA

Simplified replicated growable array: inserts carry `(origin, counter)` ids;
materialization sorts by id (demo-grade, not a full RGA positioning tree).

## Convergence invariants

Each workload registers a cluster invariant comparing `Read()` / `Materialize()`
across running nodes. Violations indicate merge or delivery bugs under the
current schedule and fault profile.
