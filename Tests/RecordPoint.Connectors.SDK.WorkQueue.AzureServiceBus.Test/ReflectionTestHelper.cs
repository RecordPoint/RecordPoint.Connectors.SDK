#nullable enable
using System.Reflection;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test
{
    /// <summary>
    /// Reflection helpers shared by the Azure Service Bus tests. The Service Bus SDK stores
    /// event handlers in private backing fields that cannot be mocked, so tests extract them
    /// via reflection.
    /// </summary>
    public static class ReflectionTestHelper
    {
        /// <summary>
        /// Walks the type hierarchy of <paramref name="instance"/> (optionally starting from
        /// <paramref name="startType"/>) and returns the value of the first instance field whose
        /// type is exactly <typeparamref name="TDelegate"/>, or <c>null</c> if none is found.
        /// </summary>
        /// <typeparam name="TDelegate">The delegate type of the backing field to extract.</typeparam>
        /// <param name="instance">The instance to read the field value from.</param>
        /// <param name="startType">
        /// The type to begin the search from. Defaults to the runtime type of <paramref name="instance"/>.
        /// Supply an explicit base type to skip over dynamically generated proxy fields.
        /// </param>
        public static TDelegate? ExtractDelegateField<TDelegate>(object instance, Type? startType = null)
            where TDelegate : class
        {
            var type = startType ?? instance.GetType();
            while (type != null)
            {
                var field = type
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .FirstOrDefault(f => f.FieldType == typeof(TDelegate));
                if (field != null)
                {
                    return field.GetValue(instance) as TDelegate;
                }
                type = type.BaseType;
            }
            return null;
        }
    }
}
