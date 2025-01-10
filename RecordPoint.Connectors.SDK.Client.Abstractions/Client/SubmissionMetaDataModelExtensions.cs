namespace RecordPoint.Connectors.SDK.Client.Models
{
    /// <summary>
    /// The submission meta data model extensions.
    /// </summary>
    public static class SubmissionMetaDataModelExtensions
    {
        /// <summary>
        /// Add or update.
        /// </summary>
        /// <param name="metaDataList">The meta data list.</param>
        /// <param name="metaData">The meta data.</param>
        /// <returns><![CDATA[IList<SubmissionMetaDataModel>]]></returns>
        public static IList<SubmissionMetaDataModel> AddOrUpdate(this IList<SubmissionMetaDataModel> metaDataList, SubmissionMetaDataModel metaData)
        {
            var existing = metaDataList.FirstOrDefault(x => x.Name == metaData.Name);
            if (existing != null)
            {
                existing.DisplayName = metaData.DisplayName;
                existing.Type = metaData.Type;
                existing.Value = metaData.Value;
            }
            else
            {
                metaDataList.Add(metaData);
            }

            return metaDataList;
        }

        /// <summary>
        /// Get value or default.
        /// </summary>
        /// <param name="metaDataList">The meta data list.</param>
        /// <param name="name">The name.</param>
        /// <returns>A string</returns>
        public static string GetValueOrDefault(this IList<SubmissionMetaDataModel> metaDataList, string name)
        {
            var item = metaDataList.FirstOrDefault(x => x.Name == name);
            if (item == null) return string.Empty;
            return item.Value;
        }
    }
}
