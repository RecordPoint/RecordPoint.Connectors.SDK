using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using RecordPoint.Connectors.SDK.SubmitPipeline;

namespace RecordPoint.Connectors.SDK.Test.Filters
{
    public abstract class FilterTestBase
    {
        protected static IList<string> FilterTypes { get; set; } = new List<string>()
        {
            FilterConstants.FilterFieldTypes.BooleanType,
            FilterConstants.FilterFieldTypes.DateType,
            FilterConstants.FilterFieldTypes.NumericalType,
            FilterConstants.FilterFieldTypes.StringType,
        };

        protected static IList<string> FieldTypes { get; set; } = new List<string>()
        {
            nameof(String),
            nameof(Boolean),
            nameof(DateTime),
            nameof(Double)
        };

        protected static SubmitContext GetSubmitContext(FiltersModel filters = null)
        {
            var output = new SubmitContext()
            {
                CoreMetaData = new List<SubmissionMetaDataModel>(),
                SourceMetaData = new List<SubmissionMetaDataModel>(),
                Filters = filters
            };

            foreach (var dataType in FieldTypes)
            {
                for (int i = 0; i < 5; i++)
                {
                    var coreMetaDataName = Guid.NewGuid().ToString();
                    output.CoreMetaData.Add(new SubmissionMetaDataModel(coreMetaDataName, coreMetaDataName, dataType, Guid.NewGuid().ToString()));

                    var sourceMetaDataName = Guid.NewGuid().ToString();
                    output.SourceMetaData.Add(new SubmissionMetaDataModel(sourceMetaDataName, sourceMetaDataName, dataType, Guid.NewGuid().ToString()));
                }
            }

            return output;
        }

        protected static string GetSourceFieldName(Guid connectorTypeId, string fieldName, string fieldType)
        {
            return $"S|{connectorTypeId}|{fieldType}|{fieldName}";
        }

        protected static string GetTestString()
        {
            return Guid.NewGuid().ToString();
        }

        protected static KeyValuePair<string, string> GetTestKeyValuePair()
        {
            return new KeyValuePair<string, string>(GetTestString(), GetTestString());
        }
    }
}
