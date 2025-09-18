using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Test.Mock.R365;
using RecordPoint.Connectors.SDK.Work;
using System.Text.Json;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class SynchronousSubmitRecordOperationSut : ContentManagerSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .UseMockR365Client()
                .ConfigureServices(svcs => svcs.AddTransient<SynchronousSubmitRecordOperation>());
        }

        #region Synchronous Record Submission Work Request

        public string SubmitRecordOperationWorkId1 { get; set; } = "SubmitRecordOperationWorkId1";

        public DateTime SubmitRecordSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public ManagedWorkStatusModel CreateSynchronousSubmitRecordManagedWorkStatusModel(ConnectorConfigModel connector) => CreateSynchronousSubmitRecordManagedWorkStatusModel(connector.Id);

        public ManagedWorkStatusModel CreateSynchronousSubmitRecordManagedWorkStatusModel(string connectorConfigurationId) => new()
        {
            WorkId = SubmitRecordOperationWorkId1,
            WorkType = SynchronousSubmitRecordOperation.WORK_TYPE,
            ConnectorId = connectorConfigurationId,
            Id = SubmitRecordOperationWorkId1
        };

        public WorkRequest CreateSynchronousSubmitRecordRequest(ManagedWorkStatusModel workMessage, Record record) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = SynchronousSubmitRecordOperation.WORK_TYPE,
            SubmitDateTime = SubmitRecordSubmitTime1,
            Body = JsonSerializer.Serialize(record)
        };

        public static Record CreateRecord(int binaryCount = 1)
        {
            var binaries = new List<BinaryMetaInfo>();
            for (int i = 1; i <= binaryCount; i++)
            {
                binaries.Add(new BinaryMetaInfo
                {
                    ExternalId = $"BinaryExternalId{i}",
                    ItemExternalId = "RecordExternalId1",
                    MimeType = "application/text"
                });
            }

            return new Record
            {
                ExternalId = "RecordExternalId1",
                ParentExternalId = "ParentExternalId1",
                Location = "https://example.com/record1",
                Binaries = binaries
            };
        }


        public async Task SetWorkRunning(ManagedWorkStatusModel workMessage)
        {
            await Services.GetRequiredService<IManagedWorkStatusManager>()
                .AddWorkStatusAsync(workMessage, CancellationToken.None);
        }

        public async Task SetWorkContinue(ManagedWorkStatusModel workMessage)
        {
            await Services.GetRequiredService<IManagedWorkStatusManager>()
                .SetWorkContinueAsync(workMessage.WorkId, workMessage.WorkId, string.Empty, CancellationToken.None);
        }
        #endregion

    }
}
