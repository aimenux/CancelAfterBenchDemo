# CancelAfterBenchDemo
```
Benchmarking various ways to cancel tasks after some execution period
```

In this demo, i m using [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) library in order to benchmark various ways of canceling tasks after some execution period.
>
>
> :one: `UsingWaitAsync` : a bench based on [WaitAsync](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync)
>
> :two: `UsingCancellationTokenSourceAsync` : a bench based on [CancellationTokenSource](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource)
>
> :three: `UsingTaskCompletionSourceAsync` : a bench based on [TaskCompletionSource](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource)
>
> :four: `UsingPollyAsync` : a bench based on [polly timeout policy](https://github.com/App-vNext/Polly#timeout)
>

In order to run benchmarks, type this command in your favorite terminal : `.\App.exe`

```
|                            Method | TimeoutInSeconds |      Mean |     Error |    StdDev |    Median |       Min |       Max | Rank | Allocated |
|---------------------------------- |----------------- |----------:|----------:|----------:|----------:|----------:|----------:|-----:|----------:|
|                    UsingWaitAsync |                1 |  4.524 μs |  1.132 μs |  3.194 μs |  3.100 μs |  1.700 μs |  14.20 μs |    1 |     672 B |
| UsingCancellationTokenSourceAsync |                1 | 13.453 μs |  3.197 μs |  9.174 μs | 10.200 μs |  5.100 μs |  41.00 μs |    2 |   1,200 B |
|    UsingTaskCompletionSourceAsync |                1 | 17.184 μs |  4.564 μs | 12.948 μs | 12.100 μs |  4.700 μs |  56.80 μs |    2 |   1,208 B |
|                   UsingPollyAsync |                1 | 61.258 μs | 14.876 μs | 42.922 μs | 45.200 μs | 17.900 μs | 188.40 μs |    3 |   2,552 B |
|                                   |                  |           |           |           |           |           |           |      |           |
|                    UsingWaitAsync |                3 | 10.077 μs |  2.632 μs |  7.551 μs |  6.800 μs |  2.400 μs |  31.00 μs |    1 |     672 B |
| UsingCancellationTokenSourceAsync |                3 | 19.960 μs |  4.646 μs | 13.179 μs | 15.000 μs |  5.300 μs |  57.00 μs |    2 |   1,200 B |
|    UsingTaskCompletionSourceAsync |                3 | 20.223 μs |  4.927 μs | 13.976 μs | 15.900 μs |  5.400 μs |  59.40 μs |    2 |   1,208 B |
|                   UsingPollyAsync |                3 | 34.679 μs |  6.982 μs | 19.348 μs | 28.500 μs | 13.000 μs |  94.50 μs |    3 |   2,552 B |
|                                   |                  |           |           |           |           |           |           |      |           |
|                    UsingWaitAsync |                5 |  8.454 μs |  1.664 μs |  4.747 μs |  7.250 μs |  2.900 μs |  23.80 μs |    1 |     672 B |
|    UsingTaskCompletionSourceAsync |                5 |  8.833 μs |  1.902 μs |  5.206 μs |  6.600 μs |  4.200 μs |  26.40 μs |    1 |   1,208 B |
| UsingCancellationTokenSourceAsync |                5 |  9.567 μs |  1.761 μs |  4.881 μs |  8.200 μs |  4.800 μs |  25.70 μs |    2 |   1,200 B |
|                   UsingPollyAsync |                5 | 30.033 μs |  5.690 μs | 15.767 μs | 25.300 μs | 12.300 μs |  83.10 μs |    3 |   2,552 B |
```

>
**`Tools`** : vs22, net 6.0, polly, xunit, fluent-assertions, benchmark-dotnet
