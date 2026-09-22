namespace Crucible.Runtime;

using Crucible.Abstractions;

/// <summary>xorshift64* deterministic RNG. Same seed → same stream forever.</summary>
public sealed class SeededRng : ISimRandom
{
    private ulong _state;

    public int Seed { get; }

    public SeededRng(int seed)
    {
        Seed = seed;
        _state = seed == 0 ? 0xDEADBEEFCAFEBABEUL : (ulong)(uint)seed * 0x9E3779B97F4A7C15UL;
        if (_state == 0)
            _state = 1;
        // Warm up a few rounds so nearby seeds diverge quickly.
        for (var i = 0; i < 8; i++)
            NextUInt64();
    }

    public SeededRng Fork(string label)
    {
        unchecked
        {
            var mixed = Seed;
            foreach (var c in label)
                mixed = mixed * 31 + c;
            return new SeededRng(mixed);
        }
    }

    private ulong NextUInt64()
    {
        var x = _state;
        x ^= x >> 12;
        x ^= x << 25;
        x ^= x >> 27;
        _state = x;
        return x * 0x2545F4914F6CDD1DUL;
    }

    public int Next() => (int)(NextUInt64() >> 33);

    public int Next(int maxExclusive)
    {
        if (maxExclusive <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxExclusive));
        return (int)(NextUInt64() % (ulong)maxExclusive);
    }

    public int Next(int minInclusive, int maxExclusive)
    {
        if (maxExclusive <= minInclusive)
            throw new ArgumentOutOfRangeException(nameof(maxExclusive));
        return minInclusive + Next(maxExclusive - minInclusive);
    }

    public long NextLong(long minInclusive, long maxExclusive)
    {
        if (maxExclusive <= minInclusive)
            throw new ArgumentOutOfRangeException(nameof(maxExclusive));
        var range = (ulong)(maxExclusive - minInclusive);
        return minInclusive + (long)(NextUInt64() % range);
    }

    public double NextDouble() => (NextUInt64() >> 11) * (1.0 / (1UL << 53));

    public bool NextBool(double probability = 0.5)
    {
        if (probability <= 0) return false;
        if (probability >= 1) return true;
        return NextDouble() < probability;
    }

    public void Shuffle<T>(IList<T> list)
    {
        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public T Choose<T>(IReadOnlyList<T> items)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Cannot choose from an empty list.");
        return items[Next(items.Count)];
    }
}
