# Schedule exploration

Crucible ships three explorers:

## Random (`RandomExplorer`)

Runs many seeds with a `RecordingOracle`. Cheap smoke coverage.

## PCT (`PctExplorer`)

Implements a probabilistic concurrency testing strategy: at randomly chosen
steps, priority orders over enabled transitions are reshuffled. Useful for finding
depth bugs without exhaustive DFS.

## DFS (`DfsExplorer`)

Bounded depth-first search over alternate oracle choices at the first branching
point beyond a prefix schedule. Produces alternate schedules until `MaxSchedules`
or a counterexample is found.

## Replay

`ReplayOracle` consumes a serialized `ScheduleTrace` (JSON). Divergence between
recorded and live choice points throws, catching drift after code changes.

## Artifacts

Failed runs optionally write:

```
out/pct-<seed>.schedule.json
out/dfs-<seed>-<n>.schedule.json
```

Replay with:

```bash
dotnet run --project src/Crucible.Cli -- replay --schedule out/pct-42.schedule.json
```

## Configuration

| Flag | Meaning |
|------|---------|
| `--steps` | Max runtime steps per schedule |
| `--schedules` | Cap for random/DFS batch |
| `--mode` | `pct`, `dfs`, or `random` |
| `--out` | Directory for schedule files |
