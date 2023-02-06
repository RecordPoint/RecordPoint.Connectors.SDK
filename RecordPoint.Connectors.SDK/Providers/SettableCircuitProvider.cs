using RecordPoint.Connectors.SDK.Helpers;
using System;

namespace RecordPoint.Connectors.SDK.Providers
{
    /// <summary>
    /// See ISettableCircuitProvider
    /// </summary>
    public class SettableCircuitProvider : ISettableCircuitProvider
    {
        public IDateTimeProvider DateTimeProvider { get; set; }
        private DateTimeOffset? _disabledUntil = null;
        private object _disabledUntilLock = new object();

        /// <summary>
        /// See ICircuitProvider
        /// </summary>
        /// <param name="waitFor"></param>
        /// <returns></returns>
        public bool IsCircuitClosed(out TimeSpan waitFor)
        {
            var isClosed = CheckNotPastLockedDateTime(out TimeSpan waitForInternal, DateTimeProvider.UtcNow);
            waitFor = waitForInternal;
            return isClosed;
        }

        /// <summary>
        /// See ISettableCircuitProvider
        /// </summary>
        public void SetOpenUntil(DateTime newTime)
        {
            // If lockedDateTime does not have a value OR it does have a value but that value is less than 
            // the newTime passed in, set lockedDateTime to the value passed in.
            // Otherwise, leave lockedDateTime with the current value
            LockHelper.LockedCreateOrUpdate(
                ref _disabledUntilLock,
                ref _disabledUntil,
                (currentValue) => !currentValue.HasValue || (newTime > currentValue),
                (currentValue) => newTime
            );
        }

        private bool CheckNotPastLockedDateTime(out TimeSpan waitFor, DateTimeOffset timeToCheck)
        {
            // If lockedDateTime doesn't have a value, storedLockedDateTime is null
            // If lockedDateTime has a value, but it is before the timeToCheck, set it to null, and set storedLockedDateTime to null
            // Otherwise, set storedLockedDateTime to the value of lockedDateTime
            var storedLockedDateTime = LockHelper.LockedCreateOrUpdate(
                ref _disabledUntilLock,
                ref _disabledUntil,
                (currentValue) => currentValue.HasValue && timeToCheck > currentValue,
                (currentValue) =>
                {
                    return null;
                }
            );

            // If storedLockedDateTime is not null, we return false (the time to check is before the lockedDateTime) and we out the remaining time difference.
            if (storedLockedDateTime.HasValue)
            {
                waitFor = storedLockedDateTime.Value - timeToCheck;
                return false;
            }

            // Otherwise, we return false, and out 0 (as we've already passed the time stored in lockedDateTime)
            waitFor = TimeSpan.Zero;
            return true;
        }
    }
}
