using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using ReferenceConnector.Common;

namespace ReferenceConnector.ContentSynchronisation
{
    internal class ContentSynchronisationAction : IContentSynchronisationAction
    {
        private readonly ReferenceConnectorOptions _options;
        private readonly IRandomHelper _randomHelper;
        private readonly ILogger<ContentSynchronisationAction> _logger;

        public ContentSynchronisationAction(
            IOptions<ReferenceConnectorOptions> options,
            IRandomHelper randomHelper,
            ILogger<ContentSynchronisationAction> logger)
        {
            _options = options.Value;
            _randomHelper = randomHelper;
            _logger = logger;
        }

        public Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, DateTimeOffset startDate, CancellationToken cancellationToken)
        {
            return Task.FromResult(GetContentOutcome(channel, string.Empty));
        }

        public Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken)
        {
            return Task.FromResult(GetContentOutcome(channel, cursor));
        }

        public Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private ContentResult GetContentOutcome(Channel channel, string cursor)
        {
            var cursorDate = string.IsNullOrEmpty(cursor) ? DateTimeOffset.UtcNow.AddDays(-1) : new DateTimeOffset(long.Parse(cursor), TimeSpan.FromSeconds(0));

            if (_randomHelper.CalculateLikelihood(_options.RemoveChannelLikelihood))
            {
                _logger.LogInformation("Randomly deciding to remove channel");
                return new ContentResult
                {
                    Reason = "Channel no longer exists",
                    ResultType = ContentResultType.Abandonded
                };
            }

            var records = new List<Record>();
            if (_randomHelper.CalculateLikelihood(_options.NewRecordLikelihood))
            {
                var numRecords = _randomHelper.GenerateRandomInteger(1, _options.MaxNewRecords);
                _logger.LogInformation("Creating {numReocrds} random records", numRecords);
                for (var i = 0; i < numRecords; i++)
                {
                    records.Add(CreateRandomRecord(channel, cursorDate, string.Empty));
                }
            }

            var aggregations = new List<Aggregation>();
            if (_randomHelper.CalculateLikelihood(_options.NewAggregationLikelihood))
            {
                _logger.LogInformation("Creating random Aggregation");
                var (aggregation, aggRecords) = CreateAggregation(channel, cursorDate);
                aggregations.Add(aggregation);
                records.AddRange(aggRecords);
            }


            return new ContentResult()
            {
                Records = records,
                Aggregations = aggregations,
                ResultType = ContentResultType.Complete,
                Cursor = DateTimeOffset.UtcNow.Ticks.ToString()
            };
        }

        private Record CreateRandomRecord(Channel channel, DateTimeOffset cursorDate, string parentExternalId)
        {
            const string mimeType = "text/plain";
            var metadataItems = new List<MetaDataItem>
            {
                new MetaDataItem {Name = "Channel Name", Type = "String", Value = channel.Title}
            };

            var author = _options.RecordAuthors.Random(_randomHelper);
            var createdBy = _options.RecordAuthors.Random(_randomHelper);
            var modifiedBy = _options.RecordAuthors.Random(_randomHelper);

            var createdDate = _randomHelper.GenerateRandomDateTimeOffset(cursorDate);
            var modifiedDate = _randomHelper.GenerateRandomDateTimeOffset(createdDate);

            var fileSize = _randomHelper.GenerateRandomInteger(2048, 10240);

            var externalId = _randomHelper.GenerateRandomWord(15);
            var title = _randomHelper.GenerateRandomWords(3);
            return new Record
            {
                Author = author,
                Binaries = new List<BinaryMetaInfo> {
                    BuildContentBinary(mimeType, externalId, modifiedDate)
                },
                ContentVersion = DateTimeOffset.UtcNow.Ticks.ToString(),
                ExternalId = externalId,
                FileSize = fileSize,
                MediaType = string.Empty,
                MetaDataItems = metadataItems,
                MimeType = mimeType,
                Title = title,
                ParentExternalId = string.IsNullOrEmpty(parentExternalId) ? Guid.NewGuid().ToString() : parentExternalId,
                Location = Guid.NewGuid().ToString(),
                SourceCreatedBy = createdBy,
                SourceCreatedDate = createdDate,
                SourceLastModifiedBy = modifiedBy,
                SourceLastModifiedDate = createdDate
            };
        }

        private static BinaryMetaInfo BuildContentBinary(string mimeType, string externalId, DateTimeOffset modifiedDate)
        {
            var metaInfo = new BinaryMetaInfo
            {
                ItemExternalId = externalId,
                ExternalId = externalId,
                //FileHash = GetMD5Hash(fileInfo),
                MimeType = mimeType,
                ContentTokenType = mimeType,
                ContentToken = Guid.NewGuid().ToString(),
                FileName = $"{Guid.NewGuid()}.txt",
                SourceLastModifiedDate = modifiedDate
            };

            return metaInfo;
        }

        public (Aggregation Aggregation, List<Record> Records) CreateAggregation(Channel channel, DateTimeOffset cursorDate)
        {
            var metadataItems = new List<MetaDataItem>
            {
                new MetaDataItem {Name = "Channel Name", Type = "String", Value = channel.Title}
            };

            var author = _options.RecordAuthors.Random(_randomHelper);
            var createdBy = _options.RecordAuthors.Random(_randomHelper);
            var modifiedBy = _options.RecordAuthors.Random(_randomHelper);

            var createdDate = _randomHelper.GenerateRandomDateTimeOffset(cursorDate);
            var modifiedDate = _randomHelper.GenerateRandomDateTimeOffset(createdDate);

            var externalId = _randomHelper.GenerateRandomWord(15);
            var title = _randomHelper.GenerateRandomWords(3);
            var aggregation = new Aggregation
            {
                Author = author,
                ContentVersion = DateTimeOffset.UtcNow.Ticks.ToString(),
                ExternalId = externalId,
                Location = Guid.NewGuid().ToString(),
                MetaDataItems = metadataItems,
                SourceCreatedBy = createdBy,
                SourceLastModifiedBy = modifiedBy,
                SourceCreatedDate = createdDate,
                SourceLastModifiedDate = modifiedDate,
                Title = title
            };

            var records = new List<Record>();
            var numRecords = _randomHelper.GenerateRandomInteger(1, _options.MaxNewRecords);
            _logger.LogInformation("Creating {numReocrds} random records for aggregation", numRecords);
            for (var i = 0; i < numRecords; i++)
            {
                records.Add(CreateRandomRecord(channel, cursorDate, string.Empty));
            }

            return (aggregation, records);
        }
    }
}
