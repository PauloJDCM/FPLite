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
    public IOption<int>[] SomeInt() => Array.Select(i => FPLite.Some(i + i)).ToArray();

    [Benchmark]
    public IOption<int>[] NoneInt() => Array.Select(_ => FPLite.None<int>()).ToArray();

    [Benchmark]
    public IOption<string>[] SomeString() => Array.Select(i => FPLite.Some((i + i).ToString())).ToArray();

    [Benchmark]
    public IOption<string>[] NoneString() => Array.Select(_ => FPLite.None<string>()).ToArray();

    [Benchmark]
    public int[] MatchInt() => Array.Select(i => FPLite.Some(i + i).Match(i1 => i1, () => 0)).ToArray();

    [Benchmark]
    public string[] MatchString() =>
        Array.Select(i => FPLite.Some((i + i).ToString()).Match(s => s, () => "")).ToArray();
}