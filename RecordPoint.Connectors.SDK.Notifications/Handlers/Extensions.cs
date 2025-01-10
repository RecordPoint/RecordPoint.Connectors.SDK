using System.Text.Json;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// Extension methods for deserializing the context of a notification.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns a deserialized object from the context of a notification.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T? ContextToObject<T>(this object context)
        {
            var jsonElement = (JsonElement)context;
            if (jsonElement.ValueKind != JsonValueKind.Object)
            {
                throw new ArgumentException("The provided JSON element is not an object.");
            }

            return jsonElement.Deserialize<T>();
        }

        /// <summary>
        /// Returns a list of deserialized objects from the context of a notification.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> ContextToList<T>(this object context)
            where T : class
        {
            var jsonElement = (JsonElement)context;
            if (jsonElement.ValueKind != JsonValueKind.Array)
            {
                throw new ArgumentException("The provided JSON element is not an array.");
            }

            var list = new List<T>();
            foreach (var item in jsonElement.EnumerateArray())
            {
                var deserialized = item.Deserialize<T>();
                if (deserialized == null) continue;
                list.Add(deserialized);
            }

            return list;
        }

    }
}
