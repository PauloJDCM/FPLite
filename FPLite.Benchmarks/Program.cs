using BenchmarkDotNet.Running;
using FPLite.Benchmarks;

BenchmarkRunner.Run<OptionBenchmarks>();
BenchmarkRunner.Run<EitherBenchmarks>();