using BenchmarkDotNet.Attributes;
using FPLite.Option;

namespace FPLite.Benchmarks;

[MemoryDiagnoser]
public class OptionBenchmarks
{
    [Params(10, 1_000)] public int Count { get; set; }

    public int[] Array { get; set; }

    [GlobalSetup]
    public void GlobalSetup() => Array = Enumerable.Range(0, Count).ToArray();

    [Benchmark(Baseline = true)]
    public int[] BaselineInt() => Array.Select(i => i + i).ToArray();

    [Benchmark]
    public string[] BaselineString() => Array.Select(i => (i + i).ToString()).ToArray();

    [Benchmark]
    public Option<int>[] SomeInt() => Array.Select(i => Option<int>.Some(i + i)).ToArray();

    [Benchmark]
    public Option<int>[] NoneInt() => Array.Select(_ => Option<int>.None()).ToArray();

    [Benchmark]
    public Option<string>[] SomeString() => Array.Select(i => Option<string>.Some((i + i).ToString())).ToArray();

    [Benchmark]
    public Option<string>[] NoneString() => Array.Select(_ => Option<string>.None()).ToArray();

    [Benchmark]
    public int[] MatchInt() => Array.Select(i => Option<int>.Some(i + i).Match(i1 => i1, () => 0)).ToArray();

    [Benchmark]
    public string[] MatchString() =>
        Array.Select(i => Option<string>.Some((i + i).ToString()).Match(s => s, () => "")).ToArray();
}