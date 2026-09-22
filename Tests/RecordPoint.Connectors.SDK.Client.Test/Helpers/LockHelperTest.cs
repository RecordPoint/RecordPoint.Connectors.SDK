using RecordPoint.Connectors.SDK.Helpers;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Helpers
{
    public class LockHelperTest
    {
        [Fact]
        public void LockedGetOrCreate_CreatesValue_WhenNull()
        {
            object syncLock = new();
            string value = null;

            var result = LockHelper.LockedGetOrCreate(ref syncLock, ref value, () => "created");

            Assert.Equal("created", result);
            Assert.Equal("created", value);
        }

        [Fact]
        public void LockedGetOrCreate_DoesNotOverwrite_WhenNotNull()
        {
            object syncLock = new();
            string value = "existing";
            var factoryCalled = false;

            var result = LockHelper.LockedGetOrCreate(ref syncLock, ref value, () =>
            {
                factoryCalled = true;
                return "created";
            });

            Assert.Equal("existing", result);
            Assert.False(factoryCalled);
        }

        [Fact]
        public void LockedCreateOrUpdate_UpdatesValue_WhenConditionTrue()
        {
            object syncLock = new();
            var value = 1;

            var result = LockHelper.LockedCreateOrUpdate(
                ref syncLock,
                ref value,
                current => current < 5,
                current => current + 10);

            Assert.Equal(11, result);
            Assert.Equal(11, value);
        }

        [Fact]
        public void LockedCreateOrUpdate_DoesNotUpdate_WhenConditionFalse()
        {
            object syncLock = new();
            var value = 42;
            var factoryCalled = false;

            var result = LockHelper.LockedCreateOrUpdate(
                ref syncLock,
                ref value,
                current => false,
                current =>
                {
                    factoryCalled = true;
                    return current + 1;
                });

            Assert.Equal(42, result);
            Assert.False(factoryCalled);
        }

        [Fact]
        public void LockedRead_ReturnsCurrentValue()
        {
            object syncLock = new();
            var value = "hello";

            var result = LockHelper.LockedRead(ref syncLock, ref value);

            Assert.Equal("hello", result);
        }
    }
}
