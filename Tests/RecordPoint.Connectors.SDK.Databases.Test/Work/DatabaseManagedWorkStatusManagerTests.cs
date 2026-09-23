#nullable enable
using Moq;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Work.Test
{
    public class DatabaseManagedWorkStatusManagerTests
    {
        private static Mock<IObservabilityScope> CreateScope()
        {
            var scope = new Mock<IObservabilityScope>();
            scope.Setup(x => x.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Returns(Mock.Of<IDisposable>());
            return scope;
        }

        private static ManagedWorkStatusModel CreateModel(string workType)
            => new()
            {
                Id = Guid.NewGuid().ToString(),
                WorkId = Guid.NewGuid().ToString(),
                WorkType = workType
            };

        [Fact]
        public async Task GetWorkStatusesAsync_ReturnsMatchingStatuses()
        {
            var provider = new MockConnectorDatabaseProvider();
            var client = new ConnectorDatabaseClient(provider);

            var target = CreateModel("TypeA");
            using (var seedContext = client.CreateDbContext())
            {
                seedContext.ManagedWorkStatuses.Add(target);
                seedContext.ManagedWorkStatuses.Add(CreateModel("TypeB"));
                await seedContext.SaveChangesAsync(CancellationToken.None);
            }

            var manager = new DatabaseManagedWorkStatusManager(client, CreateScope().Object);

            var results = await manager.GetWorkStatusesAsync(w => w.WorkType == "TypeA", CancellationToken.None);

            Assert.Single(results);
            Assert.Equal(target.Id, results[0].Id);
        }

        [Fact]
        public async Task GetWorkStatusesAsync_ReturnsEmpty_WhenNoMatch()
        {
            var provider = new MockConnectorDatabaseProvider();
            var client = new ConnectorDatabaseClient(provider);

            using (var seedContext = client.CreateDbContext())
            {
                seedContext.ManagedWorkStatuses.Add(CreateModel("TypeA"));
                await seedContext.SaveChangesAsync(CancellationToken.None);
            }

            var manager = new DatabaseManagedWorkStatusManager(client, CreateScope().Object);

            var results = await manager.GetWorkStatusesAsync(w => w.WorkType == "DoesNotExist", CancellationToken.None);

            Assert.Empty(results);
        }
    }
}
