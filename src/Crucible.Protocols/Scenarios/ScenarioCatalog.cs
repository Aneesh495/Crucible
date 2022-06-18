namespace Crucible.Protocols.Scenarios;
using Crucible.Abstractions;
using Crucible.Faults;
using Crucible.Runtime;

public static class ScenarioCatalog
{
    public static IReadOnlyList<(string Name, Action<DeterministicRuntime, WorkloadOptions> Configure)> All => new List<(string, Action<DeterministicRuntime, WorkloadOptions>)>
    {

        ("partition-0", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 10, 50);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-1", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 11, 51);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-2", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 12, 52);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-3", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 13, 53);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-4", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 14, 54);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-5", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 15, 55);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-6", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 16, 56);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-7", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 17, 57);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-8", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 18, 58);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-9", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 19, 59);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-10", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 20, 60);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-11", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 21, 61);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-12", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 22, 62);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-13", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 23, 63);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-14", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 24, 64);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-15", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 25, 65);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-16", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 26, 66);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-17", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 27, 67);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-18", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 28, 68);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-19", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 29, 69);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-20", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 30, 70);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-21", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 31, 71);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-22", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 32, 72);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-23", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 33, 73);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-24", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 34, 74);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-25", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 35, 75);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-26", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 36, 76);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-27", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 37, 77);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-28", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 38, 78);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-29", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 39, 79);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-30", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 40, 80);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-31", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 41, 81);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-32", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 42, 82);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-33", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 43, 83);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-34", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 44, 84);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-35", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 45, 85);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-36", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 46, 86);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-37", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 47, 87);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-38", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 48, 88);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-39", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 49, 89);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-40", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 50, 90);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-41", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 51, 91);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-42", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 52, 92);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-43", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 53, 93);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-44", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 54, 94);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-45", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 55, 95);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-46", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 56, 96);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-47", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 57, 97);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-48", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 58, 98);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-49", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 59, 99);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-50", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 60, 100);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-51", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 61, 101);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-52", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 62, 102);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-53", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 63, 103);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-54", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 64, 104);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-55", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 65, 105);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-56", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 66, 106);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-57", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 67, 107);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-58", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 68, 108);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-59", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 69, 109);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-60", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 70, 110);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-61", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 71, 111);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-62", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 72, 112);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-63", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 73, 113);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-64", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 74, 114);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-65", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 75, 115);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-66", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 76, 116);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-67", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 77, 117);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-68", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 78, 118);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-69", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 79, 119);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-70", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 80, 120);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-71", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 81, 121);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-72", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 82, 122);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-73", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 83, 123);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-74", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 84, 124);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-75", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 85, 125);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-76", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 86, 126);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-77", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 87, 127);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-78", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 88, 128);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-79", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 89, 129);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-80", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 90, 130);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-81", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 91, 131);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-82", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 92, 132);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-83", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 93, 133);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-84", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 94, 134);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-85", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 95, 135);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-86", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 96, 136);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-87", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 97, 137);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-88", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 98, 138);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-89", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 99, 139);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-90", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 100, 140);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-91", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 101, 141);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-92", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 102, 142);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-93", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 103, 143);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-94", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 104, 144);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-95", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 105, 145);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-96", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 106, 146);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-97", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 107, 147);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-98", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 108, 148);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-99", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 109, 149);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-100", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 110, 150);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-101", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 111, 151);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-102", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 112, 152);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-103", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 113, 153);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-104", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 114, 154);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-105", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 115, 155);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-106", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 116, 156);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-107", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 117, 157);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-108", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 118, 158);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-109", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 119, 159);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-110", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 120, 160);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-111", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 121, 161);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-112", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 122, 162);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-113", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 123, 163);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-114", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 124, 164);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-115", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 125, 165);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-116", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 126, 166);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-117", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 127, 167);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-118", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 128, 168);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-119", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 129, 169);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-120", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 130, 170);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-121", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 131, 171);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-122", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 132, 172);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-123", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 133, 173);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-124", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 134, 174);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-125", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 135, 175);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-126", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 136, 176);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-127", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 137, 177);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-128", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 138, 178);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-129", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 139, 179);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-130", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 140, 180);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-131", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 141, 181);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-132", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 142, 182);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-133", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 143, 183);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-134", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 144, 184);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-135", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 145, 185);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-136", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 146, 186);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-137", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 147, 187);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-138", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 148, 188);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-139", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 149, 189);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-140", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 150, 190);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-141", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 151, 191);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-142", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 152, 192);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-143", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 153, 193);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-144", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 154, 194);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-145", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 155, 195);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-146", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 156, 196);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-147", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 157, 197);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-148", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 158, 198);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),

        ("partition-149", (rt, opt) =>
        {
            var nodes = rt.Nodes;
            if (nodes.Count >= 3)
            {
                var faults = PartitionGenerator.SplitBrain(nodes, 159, 199);
                foreach (var f in faults) rt.RegisterFault(f);
            }
        }),
    };
}
