```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4112/23H2/2023Update/SunValley3)
AMD Ryzen 9 7940HS w/ Radeon 780M Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 6.0.425
  [Host]     : .NET 6.0.33 (6.0.3324.36610), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.33 (6.0.3324.36610), X64 RyuJIT AVX2


```
| Method   | Iterations | Mean         | Error     | StdDev     | Ratio | RatioSD | Gen0    | Allocated | Alloc Ratio |
|--------- |----------- |-------------:|----------:|-----------:|------:|--------:|--------:|----------:|------------:|
| **Baseline** | **10**         |     **46.20 ns** |  **0.579 ns** |   **0.452 ns** |  **1.00** |    **0.01** |  **0.0038** |      **32 B** |        **1.00** |
| Option   | 10         |    160.67 ns |  3.217 ns |   3.575 ns |  3.48 |    0.08 |  0.1090 |     912 B |       28.50 |
| Result   | 10         |    135.43 ns |  1.158 ns |   1.084 ns |  2.93 |    0.04 |  0.1090 |     912 B |       28.50 |
|          |            |              |           |            |       |         |         |           |             |
| **Baseline** | **100**        |    **380.16 ns** |  **7.532 ns** |   **9.794 ns** |  **1.00** |    **0.04** |  **0.0038** |      **32 B** |        **1.00** |
| Option   | 100        |  1,160.01 ns |  6.128 ns |   5.732 ns |  3.05 |    0.08 |  1.0548 |    8832 B |      276.00 |
| Result   | 100        |  1,230.37 ns |  9.810 ns |   9.177 ns |  3.24 |    0.09 |  1.0548 |    8832 B |      276.00 |
|          |            |              |           |            |       |         |         |           |             |
| **Baseline** | **1000**       |  **3,658.19 ns** | **72.864 ns** | **115.569 ns** |  **1.00** |    **0.04** |  **0.0038** |      **32 B** |        **1.00** |
| Option   | 1000       | 11,484.02 ns | 42.127 ns |  39.406 ns |  3.14 |    0.10 | 10.5133 |   88032 B |    2,751.00 |
| Result   | 1000       | 12,052.63 ns | 77.679 ns |  72.661 ns |  3.30 |    0.11 | 10.5133 |   88032 B |    2,751.00 |
