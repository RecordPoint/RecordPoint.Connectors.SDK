#nullable enable
using System;
using System.Collections.Immutable;
using Moq;
using RecordPoint.Connectors.SDK.Observability;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Observability
{
    public class ExceptionExtensionsTests
    {
        [Fact]
        public void GetDimensions_EmptyByDefault_AndPersists()
        {
            var ex = new Exception();
            var dimensions = ex.GetDimensions();
            Assert.Empty(dimensions);
            // Calling again returns the same stored value.
            Assert.Same(dimensions, ex.GetDimensions());
        }

        [Fact]
        public void GetMeasures_EmptyByDefault_AndPersists()
        {
            var ex = new Exception();
            var measures = ex.GetMeasures();
            Assert.Empty(measures);
            Assert.Same(measures, ex.GetMeasures());
        }

        [Fact]
        public void HasScope_FalseByDefault()
        {
            var ex = new Exception();
            Assert.False(ex.HasScope());
        }

        [Fact]
        public void ScopeTo_SetsDimensionsMeasuresAndScopeFlag()
        {
            var ex = new Exception();
            var scope = new Mock<IObservabilityScope>();
            var dimensions = ImmutableDictionary<string, string?>.Empty.Add("a", "1");
            var measures = ImmutableDictionary<string, double>.Empty.Add("m", 2.0);
            scope.SetupGet(x => x.Dimensions).Returns(dimensions);
            scope.SetupGet(x => x.Measures).Returns(measures);

            ex.ScopeTo(scope.Object);

            Assert.True(ex.HasScope());
            Assert.Equal(dimensions, ex.GetDimensions());
            Assert.Equal(measures, ex.GetMeasures());
        }

        [Fact]
        public void ScopeTo_DoesNothing_WhenAlreadyScoped()
        {
            var ex = new Exception();
            var firstScope = new Mock<IObservabilityScope>();
            firstScope.SetupGet(x => x.Dimensions).Returns(ImmutableDictionary<string, string?>.Empty.Add("first", "1"));
            firstScope.SetupGet(x => x.Measures).Returns(ImmutableDictionary<string, double>.Empty);
            ex.ScopeTo(firstScope.Object);

            var secondScope = new Mock<IObservabilityScope>();
            secondScope.SetupGet(x => x.Dimensions).Returns(ImmutableDictionary<string, string?>.Empty.Add("second", "2"));
            secondScope.SetupGet(x => x.Measures).Returns(ImmutableDictionary<string, double>.Empty);
            ex.ScopeTo(secondScope.Object);

            // Should keep the first scope's dimensions.
            Assert.True(ex.GetDimensions().ContainsKey("first"));
            Assert.False(ex.GetDimensions().ContainsKey("second"));
        }

        [Fact]
        public void GetLogMessage_NullByDefault()
        {
            var ex = new Exception();
            Assert.Null(ex.GetLogMessage());
        }

        [Fact]
        public void SetLogMessage_And_GetLogMessage()
        {
            var ex = new Exception();
            ex.SetLogMessage("hello");
            Assert.Equal("hello", ex.GetLogMessage());
        }

        [Fact]
        public void EnsureLogMessage_SetsWhenAbsent_KeepsWhenPresent()
        {
            var ex = new Exception();
            ex.EnsureLogMessage("first");
            Assert.Equal("first", ex.GetLogMessage());
            ex.EnsureLogMessage("second");
            Assert.Equal("first", ex.GetLogMessage());
        }
    }
}
