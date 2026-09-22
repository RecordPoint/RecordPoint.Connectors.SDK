#nullable enable
using Moq;
using RecordPoint.Connectors.SDK.Status;
using RecordPoint.Connectors.SDK.WebHost.Controllers;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
    public class StatusControllerTests
    {
        [Fact]
        public async Task Get_ReturnsStatusModelsFromManager()
        {
            var expected = new List<StatusModel> { new(), new() };
            var statusManager = new Mock<IStatusManager>();
            statusManager
                .Setup(m => m.GetStatusModelAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);
            var sut = new StatusController(statusManager.Object);

            var result = await sut.Get();

            Assert.Same(expected, result);
            statusManager.Verify(m => m.GetStatusModelAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
