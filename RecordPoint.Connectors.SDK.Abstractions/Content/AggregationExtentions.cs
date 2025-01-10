using Newtonsoft.Json;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// The aggregation extentions.
    /// </summary>
    public static class AggregationExtentions
    {
        /// <summary>
        /// Converts to the aggregation.
        /// </summary>
        /// <param name="aggregationModel">The aggregation model.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RequiredValueNullException"></exception>
        /// <returns>An Aggregation</returns>
        public static Aggregation ToAggregation(this AggregationModel aggregationModel)
        {
            if (aggregationModel == null)
            {
                throw new ArgumentNullException(nameof(aggregationModel));
            }
            if (string.IsNullOrEmpty(aggregationModel.Title))
            {
                throw new RequiredValueNullException(nameof(aggregationModel.Title));
            }

            List<MetaDataItem> items;
            if (string.IsNullOrEmpty(aggregationModel.MetaData))
            {
                items = new();
            }
            else
            {
                var deserialisedItems = JsonConvert.DeserializeObject<List<MetaDataItem>>(aggregationModel.MetaData);
                if (deserialisedItems == null)
                {
                    items = new();
                }
                else
                {
                    items = deserialisedItems;
                }
            }

            return new()
            {
                ExternalId = aggregationModel.ExternalId,
                ParentExternalId = aggregationModel.ParentExternalId,
                Location = aggregationModel.Location,
                Title = aggregationModel.Title,
                MetaDataItems = items
            };
        }

        /// <summary>
        /// Converts to aggregation list.
        /// </summary>
        /// <param name="aggregationModels">The aggregation models.</param>
        /// <returns><![CDATA[List<Aggregation>]]></returns>
        public static List<Aggregation> ToAggregationList(this IEnumerable<AggregationModel> aggregationModels) => aggregationModels.Select(a => a.ToAggregation()).ToList();


        /// <summary>
        /// Converts to aggregation model.
        /// </summary>
        /// <param name="aggregation">The aggregation.</param>
        /// <returns>An AggregationModel</returns>
        public static AggregationModel ToAggregationModel(this Aggregation aggregation)
        {
            return new()
            {
                ExternalId = aggregation.ExternalId,
                ParentExternalId = aggregation.ParentExternalId,
                Location = aggregation.Location,
                Title = aggregation.Title,
                MetaData = JsonConvert.SerializeObject(aggregation.MetaDataItems)
            };
        }

        /// <summary>
        /// Converts to aggregation model list.
        /// </summary>
        /// <param name="aggregations">The aggregations.</param>
        /// <returns><![CDATA[List<AggregationModel>]]></returns>
        public static List<AggregationModel> ToAggregationModelList(this IEnumerable<Aggregation> aggregations) => aggregations.Select(a => a.ToAggregationModel()).ToList();
    }
}
