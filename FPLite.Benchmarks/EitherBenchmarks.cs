﻿using BenchmarkDotNet.Attributes;
using FPLite.Either;

namespace FPLite.Benchmarks;

[MemoryDiagnoser]
public class EitherBenchmarks
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
    public IEither<int, string>[] EitherInt() => Array.Select(i => FPLite.Left<int, string>(i + i)).ToArray();

    [Benchmark]
    public IEither<int, string>[] EitherString() =>
        Array.Select(i => FPLite.Right<int, string>((i + i).ToString())).ToArray();

    [Benchmark]
    public int[] MatchInt() =>
        Array.Select(i => FPLite.Left<int, string>(i + i).Match(i1 => i1, _ => 0, () => 0, (_, _) => 0)).ToArray();

    [Benchmark]
    public string[] MatchString() =>
        Array.Select(i => FPLite.Right<int, string>((i + i).ToString()).Match(_ => "", s => s, () => "", (_, _) => ""))
            .ToArray();
}