using BenchmarkDotNet.Attributes;
using FPLite.Option;
using FPLite.Result;

namespace FPLite.Benchmarks;

[MemoryDiagnoser]
public class AllocationStressTestBenchmarks
{
    [Params(10, 100, 1_000)] public int Iterations { get; set; }

    public int[] Array { get; set; }

    [GlobalSetup]
    public void GlobalSetup() => Array = Enumerable.Range(0, Iterations).ToArray();

    [Benchmark(Baseline = true)]
    public int Baseline() => Array.Aggregate(1, (acc, x) => acc + x);

    [Benchmark]
    public int Option() => Array.Aggregate(Option<int>.Some(1), (acc, x) => acc.Bind(i => i + x)).Unwrap();

    [Benchmark]
    public int Result() => Array.Aggregate(Result<int, Error>.Ok(1), (acc, x) => acc.Bind(i => i + x)).Unwrap();
}