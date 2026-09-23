using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    /// <summary>
    /// Covers the input-model building branches (populated core/source metadata,
    /// relationships and aggregation status) of the item and aggregation elements.
    /// </summary>
    public class SubmissionModelBuildingTests
    {
        private readonly Mock<ISubmission> _next = new();
        private readonly Mock<IApiClient> _mockClient = new();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new();

        public SubmissionModelBuildingTests()
        {
            var mockAuth = new Mock<IAuthenticationProvider>();
            mockAuth.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>()))
                .ReturnsAsync(new AuthenticationResult { AccessToken = "token", AccessTokenType = "Bearer" });

            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);
            _mockClientFactory.Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>())).Returns(mockAuth.Object);
        }

        private static List<SubmissionMetaDataModel> FullCoreMetaData() => new()
        {
            new() { Name = Fields.ExternalId, Type = nameof(String), Value = "ext-1" },
            new() { Name = Fields.Title, Type = nameof(String), Value = "Title" },
            new() { Name = Fields.Author, Type = nameof(String), Value = "Author" },
            new() { Name = Fields.MimeType, Type = nameof(String), Value = "text/plain" },
            new() { Name = Fields.SourceLastModifiedDate, Type = nameof(DateTime), Value = DateTime.UtcNow.ToString("O") },
            new() { Name = Fields.SourceLastModifiedBy, Type = nameof(String), Value = "modby" },
            new() { Name = Fields.SourceCreatedDate, Type = nameof(DateTime), Value = DateTime.UtcNow.ToString("O") },
            new() { Name = Fields.SourceCreatedBy, Type = nameof(String), Value = "createdby" },
            new() { Name = Fields.ContentVersion, Type = nameof(String), Value = "1" },
            new() { Name = Fields.Location, Type = nameof(String), Value = "loc" },
            new() { Name = Fields.MediaType, Type = nameof(String), Value = "Electronic" },
            new() { Name = Fields.ParentExternalId, Type = nameof(String), Value = "parent-1" },
            new() { Name = Fields.BarcodeType, Type = nameof(String), Value = "code128" },
            new() { Name = Fields.BarcodeValue, Type = nameof(String), Value = "12345" },
            new() { Name = Fields.SecurityProfileIdentifier, Type = nameof(String), Value = "sec-1" },
            new() { Name = Fields.RecordCategoryID, Type = nameof(String), Value = "cat-1" }
        };

        [Fact]
        public async Task ItemElement_BuildsModel_FromPopulatedMetadata_AndSetsAggregationFound()
        {
            ItemSubmissionInputModel captured = null;
            _mockClient.Setup(x => x.POST.ApiItemsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ItemSubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .Callback<string, ItemSubmissionInputModel, Dictionary<string, List<string>>, CancellationToken>(
                    (_, model, _, _) => captured = model)
                .ReturnsAsync(new HttpOperationResponse<object>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK),
                    Body = new ItemAcceptanceModel { AggregationStatus = "Found" }
                });

            var element = new HttpSubmitItemPipelineElement(_next.Object) { ApiClientFactory = _mockClientFactory.Object };

            var context = new ItemSubmitContext
            {
                ConnectorConfigId = Guid.NewGuid(),
                CoreMetaData = FullCoreMetaData(),
                SourceMetaData = new List<SubmissionMetaDataModel> { new() { Name = "s", Type = nameof(String), Value = "v" } },
                Relationships = new List<RelationshipDataModel> { new() { RelationshipType = "child" } },
                BinariesSubmitted = new List<DirectBinarySubmissionInputModel> { new() { BinaryExternalId = "b1" } }
            };

            await element.Submit(context);

            Assert.NotNull(captured);
            Assert.Equal("Title", captured.Title);
            Assert.Equal("Author", captured.Author);
            Assert.Equal("parent-1", captured.ParentExternalId);
            Assert.Single(captured.SourceProperties);
            Assert.Single(captured.Relationships);
            Assert.Single(captured.BinariesSubmitted);
            Assert.True(context.AggregationFoundDuringItemSubmission);
        }

        [Fact]
        public async Task ItemElement_SetsAggregationFoundFalse_WhenStatusNotFound()
        {
            _mockClient.Setup(x => x.POST.ApiItemsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ItemSubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<object>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK),
                    Body = new ItemAcceptanceModel { AggregationStatus = "NotFound" }
                });

            var element = new HttpSubmitItemPipelineElement(_next.Object) { ApiClientFactory = _mockClientFactory.Object };
            var context = new SubmitContext { CoreMetaData = FullCoreMetaData() };

            await element.Submit(context);

            Assert.False(context.AggregationFoundDuringItemSubmission);
        }

        [Fact]
        public async Task AggregationElement_BuildsModel_FromPopulatedMetadata()
        {
            AggregationSubmissionInputModel captured = null;
            _mockClient.Setup(x => x.POST.ApiAggregationsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<AggregationSubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .Callback<string, AggregationSubmissionInputModel, Dictionary<string, List<string>>, CancellationToken>(
                    (_, model, _, _) => captured = model)
                .ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK)
                });

            var element = new HttpSubmitAggregationPipelineElement(_next.Object) { ApiClientFactory = _mockClientFactory.Object };

            var context = new SubmitContext
            {
                ConnectorConfigId = Guid.NewGuid(),
                ItemTypeId = 1,
                CoreMetaData = FullCoreMetaData(),
                SourceMetaData = new List<SubmissionMetaDataModel> { new() { Name = "s", Type = nameof(String), Value = "v" } },
                Relationships = new List<RelationshipDataModel> { new() { RelationshipType = "child" } }
            };

            await element.Submit(context);

            Assert.NotNull(captured);
            Assert.Equal("Title", captured.Title);
            Assert.Equal("cat-1", captured.RecordCategoryId);
            Assert.Single(captured.SourceProperties);
            Assert.Single(captured.Relationships);
            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task AggregationElement_Handles_Conflict_Thrown()
        {
            _mockClient.Setup(x => x.POST.ApiAggregationsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<AggregationSubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new HttpOperationException("conflict")
                {
                    Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.Conflict), string.Empty)
                });

            var element = new HttpSubmitAggregationPipelineElement(_next.Object) { ApiClientFactory = _mockClientFactory.Object };
            var context = new SubmitContext { CoreMetaData = FullCoreMetaData() };

            await element.Submit(context);

            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task AggregationElement_Rethrows_UnknownHttpOperationException()
        {
            _mockClient.Setup(x => x.POST.ApiAggregationsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<AggregationSubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new HttpOperationException("server")
                {
                    Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.InternalServerError), string.Empty)
                });

            var element = new HttpSubmitAggregationPipelineElement(_next.Object) { ApiClientFactory = _mockClientFactory.Object };

            var act = async () => await element.Submit(new SubmitContext { CoreMetaData = FullCoreMetaData() });

            await Assert.ThrowsAsync<HttpOperationException>(act);
        }
    }
}
