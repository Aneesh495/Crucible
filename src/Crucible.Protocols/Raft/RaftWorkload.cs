namespace Crucible.Protocols.Raft;

using Crucible.Abstractions;
using Crucible.Faults;
using Crucible.Runtime;

public sealed class RaftWorkload : IWorkload
{
    public string Name => "raft";

    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var nodes = new List<ISimProcess>(nodeCount);
        for (var i = 0; i < nodeCount; i++)
            nodes.Add(new RaftNode(new NodeId(i), nodeCount, options));
        return nodes;
    }

    public IReadOnlyList<IInvariant> GlobalInvariants { get; } = new IInvariant[]
    {
        new RaftElectionSafetyInvariant(),
        new RaftLogMatchingInvariant(),
        new RaftStateMachineSafetyInvariant()
    };

    public void DriveClient(ISimContext clientContext, IReadOnlyList<NodeId> nodes, int step)
    {
        if (nodes.Count == 0) return;
        var target = nodes[step % nodes.Count];
        var req = new ClientRequest($"set key{step} val{step}", 1, step);
        clientContext.Network.Send(new NodeId(999), target, req);
    }
}

public static class RaftScenario
{
    public static void Configure(DeterministicRuntime runtime, WorkloadOptions options)
    {
        var workload = new RaftWorkload();
        foreach (var p in workload.CreateProcesses(options.NodeCount, options))
            runtime.Register(p);

        ChaosProfile.Mild.ConfigureNetwork(runtime.Network, runtime.Nodes);
        var nodes = runtime.Nodes;
        if (nodes.Count >= 3)
        {
            var faults = PartitionGenerator.SplitBrain(nodes, partitionStep: 50, healStep: 120);
            foreach (var f in faults)
                runtime.RegisterFault(f);
        }

        runtime.SetClientDriver(workload.DriveClient);
    }
}
