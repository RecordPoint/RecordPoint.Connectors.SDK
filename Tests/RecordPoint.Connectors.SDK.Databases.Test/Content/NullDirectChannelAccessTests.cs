#nullable enable
using RecordPoint.Connectors.SDK.Content;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Content
{
    public class NullDirectChannelAccessTests
    {
        [Fact]
        public void IsEnabled_IsFalse()
        {
            var sut = new NullDirectChannelAccess();
            Assert.False(sut.IsEnabled);
        }

        [Fact]
        public async Task ReadChannelAsync_Throws()
        {
            var sut = new NullDirectChannelAccess();
            await Assert.ThrowsAsync<NotSupportedException>(() =>
                sut.ReadChannelAsync("connector", "external", CancellationToken.None));
        }

        [Fact]
        public async Task ReadChannelClassificationsAsync_ReturnsEmpty()
        {
            var sut = new NullDirectChannelAccess();

            var items = new List<ChannelClassificationModel>();
            await foreach (var item in sut.ReadChannelClassificationsAsync("connector", 10, CancellationToken.None))
            {
                items.Add(item);
            }

            Assert.Empty(items);
        }

        [Fact]
        public async Task ReadChannelClassificationsAsync_HonoursCancellableToken()
        {
            var sut = new NullDirectChannelAccess();
            using var cts = new CancellationTokenSource();

            var items = new List<ChannelClassificationModel>();
            await foreach (var item in sut
                .ReadChannelClassificationsAsync("connector", 10, CancellationToken.None)
                .WithCancellation(cts.Token))
            {
                items.Add(item);
            }

            Assert.Empty(items);
        }

        [Fact]
        public async Task ReadChannelClassificationsAsync_WhenInitialTokenCancelled_ThrowsOnEnumeration()
        {
            var sut = new NullDirectChannelAccess();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var enumerable = sut.ReadChannelClassificationsAsync("connector", 10, cts.Token);

            await Assert.ThrowsAnyAsync<OperationCanceledException>(async () =>
            {
                await foreach (var _ in enumerable)
                {
                }
            });
        }

        [Fact]
        public async Task Enumerator_Current_And_Dispose_AreAccessible()
        {
            var sut = new NullDirectChannelAccess();
            var enumerable = sut.ReadChannelClassificationsAsync("connector", 10, CancellationToken.None);

            var enumerator = enumerable.GetAsyncEnumerator(CancellationToken.None);

            // Current is default before any successful MoveNext
            Assert.Null(enumerator.Current);
            Assert.False(await enumerator.MoveNextAsync());
            await enumerator.DisposeAsync();
        }
    }
}
