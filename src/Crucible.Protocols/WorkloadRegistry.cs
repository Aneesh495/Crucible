namespace Crucible.Protocols;

using Crucible.Abstractions;
using Crucible.Faults;
using Crucible.Protocols.Crdt;
using Crucible.Protocols.Gossip;
using Crucible.Protocols.Paxos;
using Crucible.Protocols.Raft;
using Crucible.Protocols.TwoPhaseCommit;
using Crucible.Runtime;
using Crucible.Structures;

public delegate void ScenarioConfigurator(DeterministicRuntime runtime, WorkloadOptions options);

public sealed record WorkloadEntry(IWorkload Workload, ScenarioConfigurator Configure);

public sealed class WorkloadRegistry
{
    private readonly Dictionary<string, WorkloadEntry> _entries = new(StringComparer.OrdinalIgnoreCase);

    public WorkloadRegistry()
    {
        Register(new RaftWorkload(), RaftScenario.Configure);
        Register(new OrSetWorkload(), SimpleConfigure);
        Register(new LwwRegisterWorkload(), SimpleConfigure);
        Register(new RgaWorkload(), SimpleConfigure);
        Register(new TwoPcWorkload(), SimpleConfigure);
        Register(new MultiPaxosWorkload(), SimpleConfigure);
        Register(new GossipWorkload(), SimpleConfigure);
        Register(new StackWorkload(), StructureScenario.Configure);
    }

    public IReadOnlyCollection<string> Names => _entries.Keys.OrderBy(k => k).ToArray();

    public WorkloadEntry Get(string name) =>
        _entries.TryGetValue(name, out var e)
            ? e
            : throw new KeyNotFoundException($"Unknown workload '{name}'. Known: {string.Join(", ", Names)}");

    public void Register(IWorkload workload, ScenarioConfigurator configure) =>
        _entries[workload.Name] = new WorkloadEntry(workload, configure);

    private static void SimpleConfigure(DeterministicRuntime runtime, WorkloadOptions options)
    {
        // Resolved through ScenarioHost.ConfigureWorkload for generic workloads.
    }
}

public static class ScenarioHost
{
    private static readonly WorkloadRegistry Registry = new();

    public static WorkloadRegistry RegistryView => Registry;

    public static (DeterministicRuntime Runtime, IReadOnlyList<IInvariant> Invariants) Build(
        string workloadName,
        int seed,
        WorkloadOptions options,
        IScheduleOracle? oracle = null,
        ChaosProfile? chaos = null)
    {
        var entry = Registry.Get(workloadName);
        var runtime = new DeterministicRuntime(seed, oracle);
        ConfigureWorkload(runtime, entry, options);
        chaos ??= ChaosProfile.Mild;
        chaos.ConfigureNetwork(runtime.Network, runtime.Nodes);
        return (runtime, entry.Workload.GlobalInvariants);
    }

    public static void ConfigureWorkload(
        DeterministicRuntime runtime,
        WorkloadEntry entry,
        WorkloadOptions options)
    {
        if (entry.Workload.Name == "raft")
        {
            RaftScenario.Configure(runtime, options);
            return;
        }

        if (entry.Workload.Name == "treiber-stack")
        {
            StructureScenario.Configure(runtime, options);
            return;
        }

        foreach (var p in entry.Workload.CreateProcesses(options.NodeCount, options))
            runtime.Register(p);
        runtime.SetClientDriver(entry.Workload.DriveClient);
    }
}
