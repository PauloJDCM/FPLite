using BenchmarkDotNet.Running;
using FPLite.Benchmarks;

BenchmarkRunner.Run<OptionBenchmarks>();
BenchmarkRunner.Run<EitherBenchmarks>();
BenchmarkRunner.Run<AllocationStressTestBenchmarks>();