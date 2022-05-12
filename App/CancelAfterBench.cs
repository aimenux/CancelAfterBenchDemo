using BenchmarkDotNet.Attributes;
using Polly;
using Polly.Timeout;

namespace App;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Large))]
public class CancelAfterBench
{
    public TimeSpan Timeout => TimeSpan.FromSeconds(TimeoutInSeconds);

    public Task LongRunningTask { get; set; }

    [Params(1, 3, 5)]
    public int TimeoutInSeconds { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        LongRunningTask = Task.Delay(GetLongDuration());
    }

    [Benchmark]
    public async Task<Exception> UsingWaitAsync()
    {
        try
        {
            await LongRunningTask.WaitAsync(Timeout);
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    [Benchmark]
    public async Task<Exception> UsingCancellationTokenSourceAsync()
    {
        try
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(LongRunningTask, Task.Delay(Timeout, cancellationTokenSource.Token));
            if (completedTask != LongRunningTask) throw new TimeoutException("The operation has timed out.");
            cancellationTokenSource.Cancel();
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    [Benchmark]
    public async Task<Exception> UsingTaskCompletionSourceAsync()
    {
        try
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(Timeout);
            var cancellationToken = cancellationTokenSource.Token;
            var taskCompletionSource = new TaskCompletionSource();
            await using (cancellationToken.Register(() => taskCompletionSource.TrySetCanceled()))
            {
                await Task.WhenAny(LongRunningTask, taskCompletionSource.Task);
                cancellationToken.ThrowIfCancellationRequested();
                return null;
            }
        }
        catch (OperationCanceledException ex)
        {
            return new TimeoutException("The operation has timed out.", ex);
        }
    }

    [Benchmark]
    public async Task<Exception> UsingPollyAsync()
    {
        static Task OnTimeoutAsync(Context context, TimeSpan delay, Task task, Exception exception)
        {
            throw new TimeoutException("The operation has timed out.");
        }

        try
        {
            await Policy
                .TimeoutAsync(Timeout, TimeoutStrategy.Pessimistic, OnTimeoutAsync)
                .ExecuteAsync(() => LongRunningTask);

            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private TimeSpan GetLongDuration() => TimeSpan.FromSeconds(2 * TimeoutInSeconds);
}