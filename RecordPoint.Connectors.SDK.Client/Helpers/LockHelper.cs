namespace RecordPoint.Connectors.SDK.Helpers
{
    public static class LockHelper
    {
        /// <summary>
        /// Assists with writing code that requires a lock on an object
        /// Generates a new value for the lockedObject based on its current value
        /// Checks the input condition. If the input condition evaluates to true, attains a lock
        /// and checks the input condition again. If the input condition still evaluates to true,
        /// generates a new value for the locked object using the input factory (Which accepts the current
        /// value of the locked object as a parameter for convenience)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lockingObject"></param>
        /// <param name="lockedObject"></param>
        /// <param name="lockCondition"></param>
        /// <param name="lockedObjectFactory"></param>
        /// <returns></returns>
        public static T LockedCreateOrUpdate<T>(ref object lockingObject, ref T lockedObject, Func<T, bool> lockCondition, Func<T, T> lockedObjectFactory)
        {
            if (lockCondition(lockedObject))
            {
                lock (lockingObject)
                {
                    if (lockCondition(lockedObject))
                    {
                        lockedObject = lockedObjectFactory(lockedObject);
                    }
                }
            }

            return lockedObject;
        }

        /// <summary>
        /// Assists with writing code that requires a double-checked lock 
        /// https://en.wikipedia.org/wiki/Double-checked_locking
        /// Gets the item's current value if it is not null. If it is null, locks using
        /// the input locking object and checks if it is still null (For instances where multiple
        /// threads have evaluated the object as being null) then populates it using the input factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lockingObject"></param>
        /// <param name="lockedObject"></param>
        /// <param name="lockedObjectFactory"></param>
        /// <returns></returns>
        public static T LockedGetOrCreate<T>(ref object lockingObject, ref T lockedObject, Func<T> lockedObjectFactory)
        {
            return LockedCreateOrUpdate(
                ref lockingObject,
                ref lockedObject,
                (lo) => lo == null,
                (lo) => lockedObjectFactory()
            );
        }

        public static T LockedRead<T>(ref object lockingObject, ref T lockedObject)
        {
            lock (lockingObject)
            {
                return lockedObject;
            }
        }
    }
}
