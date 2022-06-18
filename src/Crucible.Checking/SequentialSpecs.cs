namespace Crucible.Checking;

using Crucible.Abstractions;

public sealed class RegisterSpec : ISequentialSpec<int?>
{
    public string Name => "register";

    public int? InitialState => null;

    public bool TryApply(int? state, HistoryInvocation invocation, out int? next, out object? result)
    {
        result = null;
        next = state;
        switch (invocation.Method)
        {
            case "write":
                if (invocation.Argument is not int v)
                {
                    result = null;
                    return false;
                }
                next = v;
                result = true;
                return true;
            case "read":
                result = state;
                return true;
            case "compareAndSwap":
                if (invocation.Argument is not (int expected, int update))
                {
                    result = null;
                    return false;
                }
                if (state == expected)
                {
                    next = update;
                    result = true;
                }
                else
                {
                    result = false;
                }
                return true;
            default:
                result = null;
                return false;
        }
    }
}

public sealed class QueueSpec : ISequentialSpec<List<int>>
{
    public string Name => "queue";

    public List<int> InitialState => new();

    public bool TryApply(List<int> state, HistoryInvocation invocation, out List<int> next, out object? result)
    {
        next = new List<int>(state);
        result = null;
        switch (invocation.Method)
        {
            case "enqueue":
                if (invocation.Argument is not int v)
                    return false;
                next.Add(v);
                result = true;
                return true;
            case "dequeue":
                if (next.Count == 0)
                {
                    result = null;
                    return true;
                }
                var head = next[0];
                next.RemoveAt(0);
                result = head;
                return true;
            case "size":
                result = next.Count;
                return true;
            default:
                result = null;
                next = state;
                return false;
        }
    }
}

public sealed class SetSpec : ISequentialSpec<HashSet<string>>
{
    public string Name => "set";

    public HashSet<string> InitialState => new(StringComparer.Ordinal);

    public bool TryApply(HashSet<string> state, HistoryInvocation invocation, out HashSet<string> next, out object? result)
    {
        next = new HashSet<string>(state, StringComparer.Ordinal);
        result = null;
        switch (invocation.Method)
        {
            case "add":
                if (invocation.Argument is not string s)
                    return false;
                var added = next.Add(s);
                result = added;
                return true;
            case "remove":
                if (invocation.Argument is not string r)
                    return false;
                var removed = next.Remove(r);
                result = removed;
                return true;
            case "contains":
                if (invocation.Argument is not string c)
                    return false;
                result = next.Contains(c);
                return true;
            default:
                result = null;
                next = state;
                return false;
        }
    }
}
