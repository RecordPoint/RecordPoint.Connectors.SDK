#nullable enable
using System;
using System.Threading.Tasks;
using RecordPoint.Connectors.SDK.Observability;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Observability
{
    public class ObservabilityScopeExtensionsTests
    {
        private static Dimensions SampleDimensions() => new() { ["Test1"] = "1" };

        [Fact]
        public void Invoke_Action_RunsActionInScope()
        {
            var scope = new ObservabilityScope();
            var ran = false;
            scope.Invoke(SampleDimensions(), () =>
            {
                ran = true;
                Assert.Equal("1", scope.Dimensions["Test1"]);
            });
            Assert.True(ran);
            // Scope disposed after Invoke.
            Assert.Empty(scope.Dimensions);
        }

        [Fact]
        public void Invoke_Action_DecoratesExceptionAndRethrows()
        {
            var scope = new ObservabilityScope();
            var thrown = Assert.Throws<InvalidOperationException>(() =>
                scope.Invoke(SampleDimensions(), () => throw new InvalidOperationException("boom")));
            Assert.True(thrown.HasScope());
            Assert.Equal("1", thrown.GetDimensions()["Test1"]);
        }

        [Fact]
        public void Invoke_Func_ReturnsValue()
        {
            var scope = new ObservabilityScope();
            var result = scope.Invoke(SampleDimensions(), () => 42);
            Assert.Equal(42, result);
        }

        [Fact]
        public void Invoke_Func_DecoratesExceptionAndRethrows()
        {
            var scope = new ObservabilityScope();
            var thrown = Assert.Throws<InvalidOperationException>(() =>
                scope.Invoke<int>(SampleDimensions(), () => throw new InvalidOperationException("boom")));
            Assert.True(thrown.HasScope());
        }

        [Fact]
        public async Task InvokeAsync_Action_Runs()
        {
            var scope = new ObservabilityScope();
            var ran = false;
            await scope.InvokeAsync(SampleDimensions(), () =>
            {
                ran = true;
                return Task.CompletedTask;
            });
            Assert.True(ran);
        }

        [Fact]
        public async Task InvokeAsync_Action_DecoratesException()
        {
            var scope = new ObservabilityScope();
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                scope.InvokeAsync(SampleDimensions(), () => throw new InvalidOperationException("boom")));
            Assert.True(thrown.HasScope());
        }

        [Fact]
        public async Task InvokeAsync_Func_ReturnsValue()
        {
            var scope = new ObservabilityScope();
            var result = await scope.InvokeAsync(SampleDimensions(), () => Task.FromResult("value"));
            Assert.Equal("value", result);
        }

        [Fact]
        public async Task InvokeAsync_Func_DecoratesException()
        {
            var scope = new ObservabilityScope();
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                scope.InvokeAsync<int>(SampleDimensions(), () => throw new InvalidOperationException("boom")));
            Assert.True(thrown.HasScope());
        }

        [Fact]
        public void GetMetricDimensions_ReturnsService_WhenPresent()
        {
            var scope = new ObservabilityScope();
            using (scope.BeginScope(new Dimensions { [StandardDimensions.SERVICE] = "MyService", ["Other"] = "x" }))
            {
                var metricDimensions = scope.GetMetricDimensions();
                Assert.Single(metricDimensions);
                Assert.Equal("MyService", metricDimensions[StandardDimensions.SERVICE]);
            }
        }

        [Fact]
        public void GetMetricDimensions_ReturnsEmpty_WhenServiceMissing()
        {
            var scope = new ObservabilityScope();
            var metricDimensions = scope.GetMetricDimensions();
            Assert.Empty(metricDimensions);
        }

        [Fact]
        public void GetMetricDimensions_ReturnsEmpty_WhenServiceEmpty()
        {
            var scope = new ObservabilityScope();
            using (scope.BeginScope(new Dimensions { [StandardDimensions.SERVICE] = "" }))
            {
                Assert.Empty(scope.GetMetricDimensions());
            }
        }
    }
}
