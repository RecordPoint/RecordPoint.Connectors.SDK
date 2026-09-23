namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// No-op implementation of <see cref="IDirectChannelAccess"/>.
    /// Always returns <c>IsEnabled = false</c>, causing the caller to use the standard EF Core path.
    /// Registered by default so that consumers don't need to configure direct reads unless they opt in.
    /// </summary>
    public class NullDirectChannelAccess : IDirectChannelAccess
    {
        /// <inheritdoc/>
        public bool IsEnabled => false;

        /// <inheritdoc/>
        public Task<ChannelModel?> ReadChannelAsync(string connectorId, string externalId, CancellationToken cancellationToken)
        {
            throw new NotSupportedException("Direct channel access is not enabled. This method should not be called when IsEnabled is false.");
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ChannelClassificationModel> ReadChannelClassificationsAsync(string connectorId, int pageSize, CancellationToken cancellationToken)
        {
            return new EmptyChannelClassificationEnumerable(cancellationToken);
        }

        private sealed class EmptyChannelClassificationEnumerable(CancellationToken initialCancellationToken) : IAsyncEnumerable<ChannelClassificationModel>, IAsyncEnumerator<ChannelClassificationModel>
        {
            public ChannelClassificationModel Current => default;

            public IAsyncEnumerator<ChannelClassificationModel> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new EmptyChannelClassificationEnumerable(
                    cancellationToken.CanBeCanceled
                        ? cancellationToken
                        : initialCancellationToken);
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return initialCancellationToken.IsCancellationRequested
                    ? ValueTask.FromCanceled<bool>(initialCancellationToken)
                    : ValueTask.FromResult(false);
            }

            public ValueTask DisposeAsync()
            {
                return ValueTask.CompletedTask;
            }
        }
    }
}
