using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Collections.Generic;

namespace RecordPoint.Connectors.SDK.Test.Filters
{
    public class FilterTestBase
    {
        protected static IList<string> FieldTypes = new List<String>()
        {
            nameof(String),
            nameof(Boolean),
            nameof(DateTime),
            nameof(Double)
        };

        protected static IList<string> FilterTypes = new List<String>()
        {
            FilterConstants.FilterFieldTypes.BooleanType,
            FilterConstants.FilterFieldTypes.DateType,
            FilterConstants.FilterFieldTypes.NumericalType,
            FilterConstants.FilterFieldTypes.StringType,
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
                    output.CoreMetaData.Add(new SubmissionMetaDataModel()
                    {
                        Type = dataType,
                        Value = Guid.NewGuid().ToString()
                    });

                    output.SourceMetaData.Add(new SubmissionMetaDataModel(Guid.NewGuid().ToString())
                    {
                        Type = dataType,
                        Value = Guid.NewGuid().ToString()
                    });
                }
            }

            return output;
        }

        protected string GetSourceFieldName(Guid connectorTypeId, string fieldName, string fieldType)
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
