using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Helpers;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// The meta data model list extensions.
    /// </summary>
    public static class MetaDataModelListExtensions
    {
        /// <summary>
        /// Add or update.
        /// </summary>
        /// <param name="metaDataList">The meta data list.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns><![CDATA[IList<MetaDataModel>]]></returns>
        public static IList<MetaDataModel> AddOrUpdate(this IList<MetaDataModel> metaDataList, string name, string type, string value)
        {
            ValidationHelper.ArgumentNotNull(metaDataList, nameof(metaDataList));
            ValidationHelper.ArgumentNotNullOrEmpty(name, nameof(name));
            // null or empty value is permitted

            var existing = metaDataList.FirstOrDefault(x => x.Name == name);
            if (existing != null)
            {
                existing.Type = type;
                existing.Value = value;
            }
            else
            {
                metaDataList.Add(new MetaDataModel
                {
                    Name = name,
                    Type = type,
                    Value = value
                });
            }
            return metaDataList;
        }

        /// <summary>
        /// Add or update.
        /// </summary>
        /// <param name="metaDataList">The meta data list.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns><![CDATA[IList<MetaDataModel>]]></returns>
        public static IList<MetaDataModel> AddOrUpdate(this IList<MetaDataModel> metaDataList, string name, string value)
        {
            return metaDataList.AddOrUpdate(name, nameof(String), value);
        }

        /// <summary>
        /// Get value or default.
        /// </summary>
        /// <param name="metaDataList">The meta data list.</param>
        /// <param name="name">The name.</param>
        /// <returns>A string</returns>
        public static string GetValueOrDefault(this IList<MetaDataModel> metaDataList, string name)
        {
            ValidationHelper.ArgumentNotNull(metaDataList, nameof(metaDataList));
            ValidationHelper.ArgumentNotNullOrEmpty(name, nameof(name));

            var existing = metaDataList.FirstOrDefault(x => x.Name == name);
            if (existing == null)
            {
                return string.Empty;
            }
            return existing.Value;
        }

    }
}
