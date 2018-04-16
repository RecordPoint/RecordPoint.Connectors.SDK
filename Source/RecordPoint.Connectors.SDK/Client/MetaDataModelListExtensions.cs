using RecordPoint.Connectors.SDK.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordPoint.Connectors.SDK.Client
{
    public static class MetaDataModelListExtensions
    {
        public static IList<MetaDataModel> AddOrUpdate(this IList<MetaDataModel> metaDataList, string name, string type, string value)
        {
            ValidationHelper.ArgumentNotNull(metaDataList, nameof(metaDataList));
            ValidationHelper.ArgumentNotNullOrEmpty(name, nameof(name));
            // null or empty value is permitted

            var existing = metaDataList.SingleOrDefault(x => x.Name == name);
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

        public static IList<MetaDataModel> AddOrUpdate(this IList<MetaDataModel> metaDataList, string name, string value)
        {
            return metaDataList.AddOrUpdate(name, nameof(String), value);
        }

        public static string GetValueOrDefault(this IList<MetaDataModel> metaDataList, string name)
        {
            ValidationHelper.ArgumentNotNull(metaDataList, nameof(metaDataList));
            ValidationHelper.ArgumentNotNullOrEmpty(name, nameof(name));

            var existing = metaDataList.SingleOrDefault(x => x.Name == name);
            if (existing == null)
            {
                return string.Empty;
            }
            return existing.Value;
        }

    }
}
