#nullable enable
using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Context
{
    public class SystemContextTests
    {
        private static SystemContext CreateContext(SystemOptions options, string contentRoot = "C:\\content")
        {
            var env = new Mock<IHostEnvironment>();
            env.SetupGet(x => x.ContentRootPath).Returns(contentRoot);
            return new SystemContext(env.Object, Options.Create(options));
        }

        [Fact]
        public void Getters_ReturnOptionValues()
        {
            var options = new SystemOptions
            {
                CompanyName = "Acme",
                ConnectorName = "MyConnector",
                ShortName = "MC",
                ServiceName = "MyService"
            };
            var context = CreateContext(options);

            Assert.Equal("Acme", context.GetCompanyName());
            Assert.Equal("MyConnector", context.GetConnectorName());
            Assert.Equal("MC", context.GetShortName());
            Assert.Equal("MyService", context.GetServiceName());
        }

        [Fact]
        public void GetContentRootPath_ReturnsEnvironmentPath()
        {
            var context = CreateContext(new SystemOptions(), "C:\\myroot");
            Assert.Equal("C:\\myroot", context.GetContentRootPath());
        }

        [Fact]
        public void GetDefaultDataRootPath_CombinesContentRootWithData()
        {
            var context = CreateContext(new SystemOptions(), "C:\\myroot");
            Assert.Equal(Path.Combine("C:\\myroot", "Data"), context.GetDefaultDataRootPath());
        }

        [Fact]
        public void GetDataRootPath_UsesOption_WhenSet()
        {
            var context = CreateContext(new SystemOptions { DataPathRoot = "C:\\customdata" }, "C:\\myroot");
            Assert.Equal("C:\\customdata", context.GetDataRootPath());
        }

        [Fact]
        public void GetDataRootPath_FallsBackToDefault_WhenNotSet()
        {
            var context = CreateContext(new SystemOptions { DataPathRoot = "" }, "C:\\myroot");
            Assert.Equal(Path.Combine("C:\\myroot", "Data"), context.GetDataRootPath());
        }
    }

    [Collection("EnvironmentVariable")]
    public class EnvironmentExtensionsTests
    {
        [Fact]
        public void IsDevelopmentEnvironment_TrueForDevelopment_IgnoreCase()
        {
            Assert.True(EnvironmentExtensions.IsDevelopmentEnvironment("Development"));
            Assert.True(EnvironmentExtensions.IsDevelopmentEnvironment("development"));
        }

        [Fact]
        public void IsDevelopmentEnvironment_FalseForOthers()
        {
            Assert.False(EnvironmentExtensions.IsDevelopmentEnvironment("Production"));
            Assert.False(EnvironmentExtensions.IsDevelopmentEnvironment(""));
        }

        [Fact]
        public void GetASPNetCoreEnvironmentVariable_ReflectsEnvironment()
        {
            var original = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            try
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
                Assert.Equal("Development", EnvironmentExtensions.GetASPNetCoreEnvironmentVariable());
                Assert.True(EnvironmentExtensions.IsDevelopmentEnvironment());

                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", null);
                Assert.Equal(string.Empty, EnvironmentExtensions.GetASPNetCoreEnvironmentVariable());
            }
            finally
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", original);
            }
        }
    }

    public class StopSystemExceptionTests
    {
        [Fact]
        public void Constructors_SetMessageAndInner()
        {
            Assert.NotNull(new StopSystemException());

            var withMessage = new StopSystemException("stop");
            Assert.Equal("stop", withMessage.Message);

            var inner = new InvalidOperationException("inner");
            var withInner = new StopSystemException("stop", inner);
            Assert.Equal("stop", withInner.Message);
            Assert.Same(inner, withInner.InnerException);
        }
    }
}
