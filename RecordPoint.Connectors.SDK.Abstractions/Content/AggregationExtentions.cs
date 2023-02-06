using Newtonsoft.Json;

namespace RecordPoint.Connectors.SDK.Content
{
    public static class AggregationExtentions
    {
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
                Title = aggregationModel.Title,
                MetaDataItems = items
            };
        }

        public static List<Aggregation> ToAggregationList(this IEnumerable<AggregationModel> aggregationModels) => aggregationModels.Select(a => a.ToAggregation()).ToList();


        public static AggregationModel ToAggregationModel(this Aggregation aggregation)
        {
            return new()
            {
                ExternalId = aggregation.ExternalId,
                Title = aggregation.Title,
                MetaData = JsonConvert.SerializeObject(aggregation.MetaDataItems)
            };
        }

        public static List<AggregationModel> ToAggregationModelList(this IEnumerable<Aggregation> aggregations) => aggregations.Select(a => a.ToAggregationModel()).ToList();
    }
}
