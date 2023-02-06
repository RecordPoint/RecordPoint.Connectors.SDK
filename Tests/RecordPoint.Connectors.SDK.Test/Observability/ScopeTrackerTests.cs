﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RecordPoint.Connectors.SDK.Observability;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Observability
{

    /// <summary>
    /// Observability scope tracker tests
    /// </summary>
    public class ScopeTrackerTests
    {

        private readonly NullLoggerFactory _nullLoggerFactory = new NullLoggerFactory();

        private ILogger<ScopeManager> CreateTestLogger()
        {
            return _nullLoggerFactory.CreateLogger<ScopeManager>();
        }


        [Fact]
        public void Dimensions_EmptyByDefault()
        {
            var scope = new ScopeManager(CreateTestLogger());
            Assert.Empty(scope.Dimensions);
        }

        [Fact]
        public void Dimensions_SetByInitialScope()
        {
            var tracker = new ScopeManager(CreateTestLogger());
            var dimensions = new Dimensions()
            {
                ["Test1"] = "1"
            };
            using var scope = tracker.BeginScope(dimensions);
            Assert.Equal(dimensions, tracker.Dimensions);
        }

        [Fact]
        public void Dimensions_ResetByScopeDiscard()
        {
            var tracker = new ScopeManager(CreateTestLogger());
            var dimensions = new Dimensions()
            {
                ["Test1"] = "1"
            };
            {
                using var scope = tracker.BeginScope(dimensions);
            }
            Assert.Empty(tracker.Dimensions);
        }


        [Fact]
        public void Dimensions_AreAsyncLocal()
        {
            var dimensions1 = new Dimensions()
            {
                ["Test1"] = "1"
            };
            var dimensions2 = new Dimensions()
            {
                ["Test2"] = "2"
            };
            async Task CheckScopeProperties1(ScopeManager tracker)
            {
                using var scope = tracker.BeginScope(dimensions1);
                await Task.Delay(100); // Wait for the other task to be running
                Assert.Equal(dimensions1, tracker.Dimensions);
                await Task.Delay(100); // Wait for the other task to check
            }
            async Task CheckScopeProperties2(ScopeManager tracker)
            {
                using var scope = tracker.BeginScope(dimensions2);
                await Task.Delay(100); // Wait for the other task to be running
                Assert.Equal(dimensions2, tracker.Dimensions);
                await Task.Delay(100); // Wait for the other task to check
            }
            var tracker = new ScopeManager(CreateTestLogger());
            var task1 = CheckScopeProperties1(tracker);
            var task2 = CheckScopeProperties2(tracker);
            Task.WaitAll(task1, task2);
        }

        [Fact]
        public async Task DimensionsEmpty_WhenTrackerCreatedByOtherAsync()
        {
            ScopeManager scopeTracker;
            async Task<ScopeManager> CreateTracker()
            {
                await Task.Delay(1).ConfigureAwait(false) ; // Make sure we are in a differnet s
                return new ScopeManager(CreateTestLogger());
            }
            async Task CheckDimensions()
            {
                await Task.Delay(100).ConfigureAwait(false); // Wait for the other task to be running
                Assert.NotNull(scopeTracker.Dimensions);
            }
            scopeTracker = await CreateTracker();
            await CheckDimensions();
        }

        [Fact]
        public void NestedScopeInheritsDimensions()
        {
            var tracker = new ScopeManager(CreateTestLogger());
            var dimensions1 = new Dimensions()
            {
                ["Test1"] = "1"
            };

            var dimensions2 = new Dimensions()
            {
                ["Test2"] = "2"
            };

            using var scope1 = tracker.BeginScope(dimensions1);
            using var scope2 = tracker.BeginScope(dimensions2);
            Assert.Equal(new string[] { "Test1", "Test2" }, tracker.Dimensions.Keys.OrderBy(k => k).ToArray());

        }

        [Fact]
        public void DiscardNestedScopeRevertsDimensions()
        {
            var tracker = new ScopeManager(CreateTestLogger());
            var dimensions1 = new Dimensions()
            {
                ["Test1"] = "1"
            };

            var dimensions2 = new Dimensions()
            {
                ["Test2"] = "2"
            };

            using var scope1 = tracker.BeginScope(dimensions1);
            {
                using var scope2 = tracker.BeginScope(dimensions2);
            }
            Assert.Equal(dimensions1, tracker.Dimensions);
        }

        [Fact]
        public void ExtendingDimensions_Happy()
        {
            var tracker = new ScopeManager(CreateTestLogger());
            var dimensions = new Dimensions()
            {
                ["TestA"] = "A",
                ["TestB"] = "B"
            };
            using var scope1 = tracker.BeginScope(dimensions);

            Assert.Equal("A", tracker.Dimensions["TestA"].ToString());
            Assert.Equal("B", tracker.Dimensions["TestB"].ToString());

            using var scope2 = tracker.BeginScope(new Dimensions()
            {
                ["TestC"] = "C"
            });
            Assert.Equal("C", tracker.Dimensions["TestC"].ToString());
            using var scope3 = tracker.BeginScope(new Dimensions()
            {
                ["TestC"] = "D"
            });
            Assert.Equal("C", tracker.Dimensions["TestC"].ToString());
        }

    }
}
