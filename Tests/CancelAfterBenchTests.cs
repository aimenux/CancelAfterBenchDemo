using System;
using System.Threading.Tasks;
using App;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class CancelAfterBenchTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GivenLongDurationWhenUsingWaitThenShouldGetTimeoutException(int timeoutInSeconds)
        {
            // arrange
            var duration = TimeSpan.FromMilliseconds(timeoutInSeconds * 2 * 1000);
            var bench = new CancelAfterBench
            {
                TimeoutInSeconds = timeoutInSeconds,
                LongRunningTask = Task.Delay(duration)
            };

            // act
            var exception = await bench.UsingWaitAsync();

            // assert
            exception.Should().BeOfType<TimeoutException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GivenLongDurationWhenUsingCancellationTokenSourceThenShouldGetTimeoutException(int timeoutInSeconds)
        {
            // arrange
            var duration = TimeSpan.FromMilliseconds(timeoutInSeconds * 2 * 1000);
            var bench = new CancelAfterBench
            {
                TimeoutInSeconds = timeoutInSeconds,
                LongRunningTask = Task.Delay(duration)
            };

            // act
            var exception = await bench.UsingCancellationTokenSourceAsync();

            // assert
            exception.Should().BeOfType<TimeoutException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GivenLongDurationWhenUsingTaskCompletionSourceThenShouldGetTimeoutException(int timeoutInSeconds)
        {
            // arrange
            var duration = TimeSpan.FromMilliseconds(timeoutInSeconds * 2 * 1000);
            var bench = new CancelAfterBench
            {
                TimeoutInSeconds = timeoutInSeconds,
                LongRunningTask = Task.Delay(duration)
            };

            // act
            var exception = await bench.UsingTaskCompletionSourceAsync();

            // assert
            exception.Should().BeOfType<TimeoutException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GivenLongDurationWhenUsingPollyThenShouldGetTimeoutException(int timeoutInSeconds)
        {
            // arrange
            var duration = TimeSpan.FromMilliseconds(timeoutInSeconds * 2 * 1000);
            var bench = new CancelAfterBench
            {
                TimeoutInSeconds = timeoutInSeconds,
                LongRunningTask = Task.Delay(duration)
            };

            bench.Setup();

            // act
            var exception = await bench.UsingPollyAsync();

            // assert
            exception.Should().BeOfType<TimeoutException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GivenShortDurationWhenUsingWaitThenShouldNotGetNull(int timeoutInSeconds)
        {
            // arrange
            var duration = TimeSpan.FromMilliseconds(timeoutInSeconds / 2 * 1000);
            var bench = new CancelAfterBench
            {
                TimeoutInSeconds = timeoutInSeconds,
                LongRunningTask = Task.Delay(duration)
            };

            // act
            var exception = await bench.UsingWaitAsync();

            // assert
            exception.Should().Be(null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GivenShortDurationWhenUsingCancellationTokenSourceThenShouldGetNull(int timeoutInSeconds)
        {
            // arrange
            var duration = TimeSpan.FromMilliseconds(timeoutInSeconds / 2 * 1000);
            var bench = new CancelAfterBench
            {
                TimeoutInSeconds = timeoutInSeconds,
                LongRunningTask = Task.Delay(duration)
            };

            // act
            var exception = await bench.UsingCancellationTokenSourceAsync();

            // assert
            exception.Should().Be(null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GivenShortDurationWhenUsingTaskCompletionSourceThenShouldGetNull(int timeoutInSeconds)
        {
            // arrange
            var duration = TimeSpan.FromMilliseconds(timeoutInSeconds / 2 * 1000);
            var bench = new CancelAfterBench
            {
                TimeoutInSeconds = timeoutInSeconds,
                LongRunningTask = Task.Delay(duration)
            };

            // act
            var exception = await bench.UsingTaskCompletionSourceAsync();

            // assert
            exception.Should().Be(null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GivenShortDurationWhenUsingPollyThenShouldGetNull(int timeoutInSeconds)
        {
            // arrange
            var duration = TimeSpan.FromMilliseconds(timeoutInSeconds / 2 * 1000);
            var bench = new CancelAfterBench
            {
                TimeoutInSeconds = timeoutInSeconds,
                LongRunningTask = Task.Delay(duration)
            };

            // act
            var exception = await bench.UsingPollyAsync();

            // assert
            exception.Should().Be(null);
        }
    }
}