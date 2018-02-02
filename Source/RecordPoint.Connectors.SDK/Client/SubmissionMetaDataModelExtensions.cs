using System.Collections.Generic;
using System.Linq;

namespace RecordPoint.Connectors.SDK.Client.Models
{
    public static class SubmissionMetaDataModelExtensions
    {
        public static IList<SubmissionMetaDataModel> AddOrUpdate(this IList<SubmissionMetaDataModel> metaDataList, SubmissionMetaDataModel metaData)
        {
            var existing = metaDataList.FirstOrDefault(x => x.Name == metaData.Name);
            if(existing != null)
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

        public static string GetValueOrDefault(this IList<SubmissionMetaDataModel> metaDataList, string name)
        {
            return metaDataList.FirstOrDefault(x => x.Name == name)?.Value;
        }
    }
}
