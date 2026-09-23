using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using RecordPoint.Connectors.SDK.Content;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test
{
    /// <summary>
    /// Tests for <see cref="CosmosDirectChannelAccess.ParseChannelDocument"/> using real
    /// Cosmos document payloads from the rpfabricde2c-cosmos channels container.
    /// The parser is casing-resilient: it tries PascalCase first, then camelCase.
    /// Covers both naming conventions, null/missing fields, and DateTimeOffset edge cases.
    /// </summary>
    public class CosmosDirectChannelAccessParsingTests
    {
        private static MemoryStream ToStream(string json)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        #region Real Cosmos payloads — PascalCase

        [Fact]
        public void ParseChannelDocument_RealPayload_NotSoImportantDocuments()
        {
            // Real document from a test channels container (PascalCase)
            var json = """
            {
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "ExternalId": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZojkYxzo40aMTb8v6jT8ITVb",
                "CreatedDate": "2026-04-16T18:54:39.7170184+10:00",
                "MetaData": "[{\"Name\":\"ListId\",\"Type\":\"String\",\"Value\":\"e81c63e4-46e3-4d8c-bf2f-ea34fc21355b\",\"MetaDataItemType\":1},{\"Name\":\"DriveId\",\"Type\":\"String\",\"Value\":\"b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZojkYxzo40aMTb8v6jT8ITVb\",\"MetaDataItemType\":1},{\"Name\":\"SiteId\",\"Type\":\"String\",\"Value\":\"tenant3load.sharepoint.com,4c10855f-1ba7-4b24-9767-f9ed8ef66d07,3f1cabdd-ac2c-44f3-982f-a36971db6688\",\"MetaDataItemType\":1},{\"Name\":\"WebUrl\",\"Type\":\"String\",\"Value\":\"https://tenant3load.sharepoint.com/Not%20so%20Important%20Documents\",\"MetaDataItemType\":1},{\"Name\":\"ChannelType\",\"Type\":\"Nullable`1\",\"Value\":\"Library\",\"MetaDataItemType\":1}]",
                "Title": "Not so Important Documents",
                "id": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZojkYxzo40aMTb8v6jT8ITVb",
                "_rid": "-SM4AMgBXL4HAAAAAAAAAA==",
                "_self": "dbs/-SM4AA==/colls/-SM4AMgBXL4=/docs/-SM4AMgBXL4HAAAAAAAAAA==/",
                "_etag": "\"7d00e0a4-0000-1a00-0000-69e0a4300000\"",
                "_attachments": "attachments/",
                "_ts": 1776329776
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZojkYxzo40aMTb8v6jT8ITVb", result.ExternalId);
            Assert.Equal("127635af-fff0-4b7b-b183-9c35a185b560", result.ConnectorId);
            Assert.Equal("Not so Important Documents", result.Title);
            Assert.Contains("ListId", result.MetaData);
            Assert.Contains("Not%20so%20Important%20Documents", result.MetaData);
            Assert.Equal(new DateTimeOffset(2026, 4, 16, 18, 54, 39, TimeSpan.FromHours(10)).AddTicks(7170184), result.CreatedDate);
        }

        [Fact]
        public void ParseChannelDocument_RealPayload_ShortTitle()
        {
            // Real document — shortest title in test ("jf")
            var json = """
            {
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "ExternalId": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZoikWe-rTYdsRKTAjRcF7DVn",
                "CreatedDate": "2026-04-16T18:54:39.7166179+10:00",
                "MetaData": "[{\"Name\":\"ListId\",\"Type\":\"String\",\"Value\":\"abef59a4-874d-446c-a4c0-8d1705ec3567\",\"MetaDataItemType\":1},{\"Name\":\"ListDisplayName\",\"Type\":\"String\",\"Value\":\"jf\",\"MetaDataItemType\":1}]",
                "Title": "jf",
                "id": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZoikWe-rTYdsRKTAjRcF7DVn",
                "_rid": "-SM4AMgBXL4FAAAAAAAAAA==",
                "_self": "dbs/-SM4AA==/colls/-SM4AMgBXL4=/docs/-SM4AMgBXL4FAAAAAAAAAA==/",
                "_etag": "\"7d008da4-0000-1a00-0000-69e0a42c0000\"",
                "_attachments": "attachments/",
                "_ts": 1776329772
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZoikWe-rTYdsRKTAjRcF7DVn", result.ExternalId);
            Assert.Equal("jf", result.Title);
        }

        [Fact]
        public void ParseChannelDocument_RealPayload_SharedDocuments()
        {
            // Real document — default SharePoint "Shared Documents" library
            var json = """
            {
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "ExternalId": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZojHcJGDycx3Rb1YID8jPRTW",
                "CreatedDate": "2026-04-16T18:54:39.7153083+10:00",
                "MetaData": "[{\"Name\":\"ListId\",\"Type\":\"String\",\"Value\":\"839170c7-ccc9-4577-bd58-203f233d14d6\",\"MetaDataItemType\":1},{\"Name\":\"WebUrl\",\"Type\":\"String\",\"Value\":\"https://tenant3load.sharepoint.com/Shared%20Documents\",\"MetaDataItemType\":1},{\"Name\":\"Template\",\"Type\":\"String\",\"Value\":\"documentLibrary\",\"MetaDataItemType\":2}]",
                "Title": "Shared Documents",
                "id": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZojHcJGDycx3Rb1YID8jPRTW",
                "_rid": "-SM4AMgBXL4CAAAAAAAAAA==",
                "_self": "dbs/-SM4AA==/colls/-SM4AMgBXL4=/docs/-SM4AMgBXL4CAAAAAAAAAA==/",
                "_etag": "\"7d0068a4-0000-1a00-0000-69e0a42b0000\"",
                "_attachments": "attachments/",
                "_ts": 1776329771
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZojHcJGDycx3Rb1YID8jPRTW", result.ExternalId);
            Assert.Equal("127635af-fff0-4b7b-b183-9c35a185b560", result.ConnectorId);
            Assert.Equal("Shared Documents", result.Title);
            Assert.Contains("Shared%20Documents", result.MetaData);
        }

        [Fact]
        public void ParseChannelDocument_RealPayload_UatTestLibrary()
        {
            // Real document — UAT test library with underscores and Description in metadata
            var json = """
            {
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "ExternalId": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZoh-rWhy7_EcSpIJFyxCwlU_",
                "CreatedDate": "2026-04-16T18:54:39.7009332+10:00",
                "MetaData": "[{\"Name\":\"ListId\",\"Type\":\"String\",\"Value\":\"7268ad7e-f1ef-4a1c-9209-172c42c2553f\",\"MetaDataItemType\":1},{\"Name\":\"ListDisplayName\",\"Type\":\"String\",\"Value\":\"UAT_TEST_DL_1_JULY\",\"MetaDataItemType\":1},{\"Name\":\"Description\",\"Type\":\"String\",\"Value\":\"UAT_TEST_DL_1_JULY\",\"MetaDataItemType\":1}]",
                "Title": "UAT_TEST_DL_1_JULY",
                "id": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZoh-rWhy7_EcSpIJFyxCwlU_",
                "_rid": "-SM4AMgBXL4BAAAAAAAAAA==",
                "_self": "dbs/-SM4AA==/colls/-SM4AMgBXL4=/docs/-SM4AMgBXL4BAAAAAAAAAA==/",
                "_etag": "\"7d00cca4-0000-1a00-0000-69e0a42f0000\"",
                "_attachments": "attachments/",
                "_ts": 1776329775
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZoh-rWhy7_EcSpIJFyxCwlU_", result.ExternalId);
            Assert.Equal("UAT_TEST_DL_1_JULY", result.Title);
            Assert.Contains("UAT_TEST_DL_1_JULY", result.MetaData);
        }

        [Fact]
        public void ParseChannelDocument_RealPayload_SubSecondPrecisionDate()
        {
            // Verifies that sub-second precision (.7170184) in CreatedDate is preserved
            var json = """
            {
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "ExternalId": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZogbbEXsodEITKXqQCX2oHme",
                "CreatedDate": "2026-04-16T18:54:39.7163952+10:00",
                "MetaData": "[{\"Name\":\"ListDisplayName\",\"Type\":\"String\",\"Value\":\"Extremely Important Documents\",\"MetaDataItemType\":1}]",
                "Title": "Extremely Important Documents",
                "id": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZogbbEXsodEITKXqQCX2oHme",
                "_rid": "-SM4AMgBXL4EAAAAAAAAAA==",
                "_self": "dbs/-SM4AA==/colls/-SM4AMgBXL4=/docs/-SM4AMgBXL4EAAAAAAAAAA==/",
                "_etag": "\"7d005ca3-0000-1a00-0000-69e0a4230000\"",
                "_attachments": "attachments/",
                "_ts": 1776329763
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("Extremely Important Documents", result.Title);
            // Verify sub-second precision is preserved
            Assert.Equal(2026, result.CreatedDate.Year);
            Assert.Equal(4, result.CreatedDate.Month);
            Assert.Equal(16, result.CreatedDate.Day);
            Assert.True(result.CreatedDate.Ticks % TimeSpan.TicksPerSecond > 0, "Sub-second precision should be preserved");
        }

        #endregion

        #region camelCase naming — auto-detected via fallback

        [Fact]
        public void ParseChannelDocument_CamelCase_FullPayload_ReturnsAllFields()
        {
            // camelCase document (as stored when UseCamelCaseNamingPolicy = true).
            // Parser tries PascalCase first, misses, then finds via camelCase fallback.
            var json = """
            {
                "id": "b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZoj9log4Ad6WQpIgw4FsCqR_",
                "connectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "title": "DifferentCaseUrLs",
                "metaData": "[{\"Name\":\"ListId\",\"Type\":\"String\",\"Value\":\"388896fd-de01-4296-9220-c3816c0aa47f\",\"MetaDataItemType\":1},{\"Name\":\"WebUrl\",\"Type\":\"String\",\"Value\":\"https://tenant3load.sharepoint.com/DifferentCaseUrLs\",\"MetaDataItemType\":1}]",
                "createdDate": "2026-04-16T18:54:39.7168126+10:00",
                "_rid": "-SM4AMgBXL4GAAAAAAAAAA==",
                "_self": "dbs/-SM4AA==/colls/-SM4AMgBXL4=/docs/-SM4AMgBXL4GAAAAAAAAAA==/",
                "_etag": "\"7d00bca4-0000-1a00-0000-69e0a42e0000\"",
                "_attachments": "attachments/",
                "_ts": 1776329774
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!X4UQTKcbJEuXZ_ntjvZtB92rHD8srPNEmC-jaXHbZoj9log4Ad6WQpIgw4FsCqR_", result.ExternalId);
            Assert.Equal("127635af-fff0-4b7b-b183-9c35a185b560", result.ConnectorId);
            Assert.Equal("DifferentCaseUrLs", result.Title);
            Assert.Contains("DifferentCaseUrLs", result.MetaData);
        }

        [Fact]
        public void ParseChannelDocument_CamelCase_DateParsedViaFallback()
        {
            // Validates that camelCase CreatedDate is found via fallback
            var json = """
            {
                "id": "b!test-camel-date",
                "connectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "title": "Camel Date Test",
                "createdDate": "2026-04-16T18:54:39.1234567+10:00"
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal(new DateTimeOffset(2026, 4, 16, 18, 54, 39, TimeSpan.FromHours(10)).AddTicks(1234567), result.CreatedDate);
        }

        #endregion

        #region Edge cases — null, missing, and mixed fields

        [Fact]
        public void ParseChannelDocument_NullTitle_ReturnsNullTitle()
        {
            var json = """
            {
                "id": "b!test-null-title",
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "Title": null,
                "MetaData": "[]",
                "CreatedDate": "2026-04-16T18:54:39+10:00"
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!test-null-title", result.ExternalId);
            Assert.Null(result.Title);
        }

        [Fact]
        public void ParseChannelDocument_NullMetaData_ReturnsNullMetaData()
        {
            var json = """
            {
                "id": "b!test-null-metadata",
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "Title": "Documents",
                "MetaData": null,
                "CreatedDate": "2026-04-16T18:54:39+10:00"
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Null(result.MetaData);
        }

        [Fact]
        public void ParseChannelDocument_MissingTitle_ReturnsNullTitle()
        {
            // Field entirely absent from the document
            var json = """
            {
                "id": "b!test-missing-title",
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "MetaData": "[]",
                "CreatedDate": "2026-04-16T18:54:39+10:00"
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Null(result.Title);
        }

        [Fact]
        public void ParseChannelDocument_MissingCreatedDate_ReturnsMinValue()
        {
            var json = """
            {
                "id": "b!test-missing-date",
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "Title": "Reports"
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal(DateTimeOffset.MinValue, result.CreatedDate);
        }

        [Fact]
        public void ParseChannelDocument_NullCreatedDate_ReturnsMinValue()
        {
            var json = """
            {
                "id": "b!test-null-date",
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "Title": "Wiki",
                "CreatedDate": null
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal(DateTimeOffset.MinValue, result.CreatedDate);
        }

        [Fact]
        public void ParseChannelDocument_MinimalDocument_OnlyIdAndConnectorId()
        {
            // Minimal valid document — only the fields used for point-read lookup
            var json = """
            {
                "id": "b!test-minimal",
                "ConnectorId": "127635af-fff0-4b7b-b183-9c35a185b560"
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!test-minimal", result.ExternalId);
            Assert.Equal("127635af-fff0-4b7b-b183-9c35a185b560", result.ConnectorId);
            Assert.Null(result.Title);
            Assert.Null(result.MetaData);
            Assert.Equal(DateTimeOffset.MinValue, result.CreatedDate);
        }

        [Fact]
        public void ParseChannelDocument_MixedCasing_PascalTitleCamelConnectorId()
        {
            // Hypothetical mixed-casing document — both naming conventions detected
            var json = """
            {
                "id": "b!test-mixed-casing",
                "connectorId": "127635af-fff0-4b7b-b183-9c35a185b560",
                "Title": "Mixed Casing Library",
                "metaData": "[{\"Name\":\"Test\",\"Type\":\"String\",\"Value\":\"value\",\"MetaDataItemType\":1}]",
                "CreatedDate": "2026-04-16T18:54:39+10:00"
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!test-mixed-casing", result.ExternalId);
            Assert.Equal("127635af-fff0-4b7b-b183-9c35a185b560", result.ConnectorId);
            Assert.Equal("Mixed Casing Library", result.Title);
            Assert.Contains("Test", result.MetaData);
            Assert.Equal(new DateTimeOffset(2026, 4, 16, 18, 54, 39, TimeSpan.FromHours(10)), result.CreatedDate);
        }

        [Fact]
        public void ParseChannelDocument_NeitherCasing_ReturnsNulls()
        {
            // Properties exist but with unexpected casing — neither PascalCase nor camelCase
            var json = """
            {
                "id": "b!test-weird-casing",
                "CONNECTORID": "127635af-fff0-4b7b-b183-9c35a185b560",
                "TITLE": "ALL CAPS"
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.NotNull(result);
            Assert.Equal("b!test-weird-casing", result.ExternalId);
            Assert.Null(result.ConnectorId);
            Assert.Null(result.Title);
        }

        #endregion

        #region Regression guard — catches new ChannelModel properties missing from ParseChannelDocument

        /// <summary>
        /// TRIPWIRE: Ensures ParseChannelDocument covers every property on ChannelModel.
        ///
        /// If you add a new property to ChannelModel, this test fails immediately.
        /// Fix: update ParseChannelDocument to map the new property, then add it to
        /// the knownProperties array below and to the round-trip tests.
        /// </summary>
        [Fact]
        public void ParseChannelDocument_PropertyCoverage_AllChannelModelPropertiesAreKnown()
        {
            var actualProperties = typeof(ChannelModel)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .Select(p => p.Name)
                .OrderBy(n => n)
                .ToArray();

            // If this assertion fails, a new property was added to ChannelModel.
            // You MUST update ParseChannelDocument in CosmosDirectChannelAccess.cs
            // to map the new property, then add it here.
            var knownProperties = new[]
            {
                "ConnectorId",
                "ContentSynchronisationWorkId",
                "CreatedDate",
                "ExternalId",
                "MetaData",
                "Title"
            };

            Assert.Equal(knownProperties, actualProperties);
        }

        /// <summary>
        /// Full round-trip: every property set to a distinctive non-default value in PascalCase.
        /// Verifies that ParseChannelDocument maps ALL fields, not just the commonly-used ones.
        /// </summary>
        [Fact]
        public void ParseChannelDocument_PascalCase_AllPropertiesRoundTrip()
        {
            var json = """
            {
                "id": "ext-roundtrip-pascal",
                "ConnectorId": "conn-roundtrip-456",
                "Title": "Round Trip PascalCase Test",
                "MetaData": "[{\"Name\":\"RoundTrip\",\"Type\":\"String\",\"Value\":\"PascalValue\",\"MetaDataItemType\":1}]",
                "CreatedDate": "2026-03-15T14:30:45.1234567+05:30",
                "ContentRegistrationWorkId": "reg-work-pascal",
                "ContentSynchronisationWorkId": "sync-work-pascal",
                "_rid": "test-rid",
                "_self": "test-self",
                "_etag": "test-etag",
                "_attachments": "attachments/",
                "_ts": 1776000000
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.Equal("ext-roundtrip-pascal", result.ExternalId);
            Assert.Equal("conn-roundtrip-456", result.ConnectorId);
            Assert.Equal("Round Trip PascalCase Test", result.Title);
            Assert.Equal("[{\"Name\":\"RoundTrip\",\"Type\":\"String\",\"Value\":\"PascalValue\",\"MetaDataItemType\":1}]", result.MetaData);
            Assert.Equal(new DateTimeOffset(2026, 3, 15, 14, 30, 45, TimeSpan.FromHours(5.5)).AddTicks(1234567), result.CreatedDate);
            Assert.Equal("sync-work-pascal", result.ContentSynchronisationWorkId);

            // Guard: every settable property must have a non-default value after parsing.
            // If this fails, a property exists on ChannelModel that ParseChannelDocument doesn't set.
            Assert.NotNull(result.ExternalId);
            Assert.NotNull(result.ConnectorId);
            Assert.NotNull(result.Title);
            Assert.NotNull(result.MetaData);
            Assert.NotEqual(default, result.CreatedDate);
            Assert.NotNull(result.ContentSynchronisationWorkId);
        }

        /// <summary>
        /// Full round-trip: every property set to a distinctive non-default value in camelCase.
        /// Mirrors the PascalCase test to ensure fallback mapping is also complete.
        /// </summary>
        [Fact]
        public void ParseChannelDocument_CamelCase_AllPropertiesRoundTrip()
        {
            var json = """
            {
                "id": "ext-roundtrip-camel",
                "connectorId": "conn-roundtrip-789",
                "title": "Round Trip camelCase Test",
                "metaData": "[{\"Name\":\"RoundTrip\",\"Type\":\"String\",\"Value\":\"CamelValue\",\"MetaDataItemType\":1}]",
                "createdDate": "2025-12-25T00:00:00.9876543+00:00",
                "contentRegistrationWorkId": "reg-work-camel",
                "contentSynchronisationWorkId": "sync-work-camel",
                "_rid": "test-rid",
                "_self": "test-self",
                "_etag": "test-etag",
                "_attachments": "attachments/",
                "_ts": 1776000000
            }
            """;

            using var stream = ToStream(json);
            var result = CosmosDirectChannelAccess.ParseChannelDocument(stream);

            Assert.Equal("ext-roundtrip-camel", result.ExternalId);
            Assert.Equal("conn-roundtrip-789", result.ConnectorId);
            Assert.Equal("Round Trip camelCase Test", result.Title);
            Assert.Equal("[{\"Name\":\"RoundTrip\",\"Type\":\"String\",\"Value\":\"CamelValue\",\"MetaDataItemType\":1}]", result.MetaData);
            Assert.Equal(new DateTimeOffset(2025, 12, 25, 0, 0, 0, TimeSpan.Zero).AddTicks(9876543), result.CreatedDate);
            Assert.Equal("sync-work-camel", result.ContentSynchronisationWorkId);

            Assert.NotNull(result.ExternalId);
            Assert.NotNull(result.ConnectorId);
            Assert.NotNull(result.Title);
            Assert.NotNull(result.MetaData);
            Assert.NotEqual(default, result.CreatedDate);
            Assert.NotNull(result.ContentSynchronisationWorkId);
        }

        [Fact]
        public void ParseChannelClassificationDocument_PascalCaseProjection_ReturnsExternalIdAndMetadata()
        {
            using var document = JsonDocument.Parse("""
            {
                "ExternalId": "channel-123",
                "MetaData": "[{\"Name\":\"ListId\",\"Value\":\"abc\"}]"
            }
            """);

            var result = CosmosDirectChannelAccess.ParseChannelClassificationDocument(document.RootElement);

            Assert.Equal("channel-123", result.ExternalId);
            Assert.Equal("[{\"Name\":\"ListId\",\"Value\":\"abc\"}]", result.MetaData);
        }

        [Fact]
        public void ParseChannelClassificationDocument_Throws_WhenProjectionShapeIsInvalid()
        {
            using var document = JsonDocument.Parse("""
            {
                "id": "channel-456",
                "metaData": "[{\"Name\":\"ListId\",\"Value\":\"xyz\"}]"
            }
            """);

            var ex = Assert.Throws<InvalidOperationException>(() => CosmosDirectChannelAccess.ParseChannelClassificationDocument(document.RootElement));
            Assert.Contains("ExternalId", ex.Message);
        }

        #endregion
    }
}
