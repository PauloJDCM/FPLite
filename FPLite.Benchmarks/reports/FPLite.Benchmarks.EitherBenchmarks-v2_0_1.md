```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4037/23H2/2023Update/SunValley3)
AMD Ryzen 9 7940HS w/ Radeon 780M Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 6.0.425
  [Host]     : .NET 6.0.33 (6.0.3324.36610), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.33 (6.0.3324.36610), X64 RyuJIT AVX2


```
| Method         | Count | Mean         | Error      | StdDev     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|--------------- |------ |-------------:|-----------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| **BaselineInt**    | **10**    |     **30.12 ns** |   **0.128 ns** |   **0.114 ns** |  **1.00** |    **0.01** | **0.0134** |      **-** |     **112 B** |        **1.00** |
| BaselineString | 10    |     81.11 ns |   0.710 ns |   0.664 ns |  2.69 |    0.02 | 0.0372 |      - |     312 B |        2.79 |
| EitherInt      | 10    |     70.54 ns |   0.155 ns |   0.121 ns |  2.34 |    0.01 | 0.0564 |      - |     472 B |        4.21 |
| EitherString   | 10    |    111.23 ns |   0.534 ns |   0.473 ns |  3.69 |    0.02 | 0.0755 | 0.0001 |     632 B |        5.64 |
| MatchInt       | 10    |     81.10 ns |   0.408 ns |   0.382 ns |  2.69 |    0.02 | 0.0516 |      - |     432 B |        3.86 |
| MatchString    | 10    |    143.51 ns |   0.577 ns |   0.540 ns |  4.76 |    0.02 | 0.0753 |      - |     632 B |        5.64 |
|                |       |              |            |            |       |         |        |        |           |             |
| **BaselineInt**    | **1000**  |  **1,542.06 ns** |   **9.550 ns** |   **8.933 ns** |  **1.00** |    **0.01** | **0.4864** |      **-** |    **4072 B** |        **1.00** |
| BaselineString | 1000  |  8,455.13 ns |  81.970 ns |  76.675 ns |  5.48 |    0.06 | 4.7607 | 0.6104 |   39912 B |        9.80 |
| EitherInt      | 1000  |  5,266.61 ns | 104.858 ns | 169.327 ns |  3.42 |    0.11 | 4.7836 | 0.5951 |   40072 B |        9.84 |
| EitherString   | 1000  | 11,994.99 ns | 112.940 ns | 105.644 ns |  7.78 |    0.08 | 8.5907 | 1.4343 |   71912 B |       17.66 |
| MatchInt       | 1000  |  6,868.28 ns |  70.636 ns |  66.073 ns |  4.45 |    0.05 | 4.3106 | 0.0610 |   36072 B |        8.86 |
| MatchString    | 1000  | 15,578.89 ns | 106.766 ns |  94.645 ns | 10.10 |    0.08 | 8.5754 | 0.9460 |   71912 B |       17.66 |
