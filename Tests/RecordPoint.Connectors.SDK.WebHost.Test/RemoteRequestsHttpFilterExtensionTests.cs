#nullable enable
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.WebHost.MiddleWare;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
    public class RemoteRequestsHttpFilterExtensionTests
    {
        [Fact]
        public void IsLocal_WhenRemoteEqualsLocal_ReturnsTrue()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Parse("10.0.0.5");
            context.Connection.LocalIpAddress = IPAddress.Parse("10.0.0.5");

            Assert.True(context.Request.IsLocal());
        }

        [Fact]
        public void IsLocal_WhenRemoteDiffersFromLocal_ReturnsFalse()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Parse("10.0.0.5");
            context.Connection.LocalIpAddress = IPAddress.Parse("10.0.0.6");

            Assert.False(context.Request.IsLocal());
        }

        [Fact]
        public void IsLocal_WhenRemoteIsLoopbackAndLocalNull_ReturnsTrue()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Loopback;
            context.Connection.LocalIpAddress = null;

            Assert.True(context.Request.IsLocal());
        }

        [Fact]
        public void IsLocal_WhenRemoteIsNotLoopbackAndLocalNull_ReturnsFalse()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Parse("8.8.8.8");
            context.Connection.LocalIpAddress = null;

            Assert.False(context.Request.IsLocal());
        }

        [Fact]
        public void IsLocal_WhenBothNull_ReturnsTrue()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = null;
            context.Connection.LocalIpAddress = null;

            Assert.True(context.Request.IsLocal());
        }

        [Fact]
        public void IsLocal_WhenRemoteNullAndLocalSet_ReturnsFalse()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = null;
            context.Connection.LocalIpAddress = IPAddress.Parse("10.0.0.6");

            Assert.False(context.Request.IsLocal());
        }

        [Fact]
        public async Task UseRemoteRequestFilter_WhenLocal_CallsNext()
        {
            var nextCalled = false;
            var middleware = BuildMiddleware(() => nextCalled = true);

            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = null;
            context.Connection.LocalIpAddress = null;

            await middleware(context);

            Assert.True(nextCalled);
            Assert.Equal(200, context.Response.StatusCode);
        }

        [Fact]
        public async Task UseRemoteRequestFilter_WhenRemote_Returns403AndDoesNotCallNext()
        {
            var nextCalled = false;
            var middleware = BuildMiddleware(() => nextCalled = true);

            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Parse("8.8.8.8");
            context.Connection.LocalIpAddress = null;

            await middleware(context);

            Assert.False(nextCalled);
            Assert.Equal(403, context.Response.StatusCode);
        }

        private static RequestDelegate BuildMiddleware(Action onNext)
        {
            var services = new ServiceCollection().BuildServiceProvider();
            var appBuilder = new ApplicationBuilder(services);
            appBuilder.UseRemoteRequestFilter();
            appBuilder.Run(_ =>
            {
                onNext();
                return Task.CompletedTask;
            });
            return appBuilder.Build();
        }
    }
}
