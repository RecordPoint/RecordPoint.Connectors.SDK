using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterPipelineElement : SubmitPipelineElementBase
    {
        private SubmitContext _submitContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public FilterPipelineElement(ISubmission next) : base(next)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        public override async Task Submit(SubmitContext submitContext)
        {
            _submitContext = submitContext;

            try
            {
                var excludeResult = ShouldExclude();
                if (excludeResult.Result)
                {
                    SkipNext(submitContext, excludeResult.FailureReason);
                    return;
                }

                var includeResult = ShouldInclude();
                if (!includeResult.Result)
                {
                    SkipNext(submitContext, includeResult.FailureReason);
                    return;
                }
            }
            catch(Exception ex) when (ex.IsInvalidFilterException())
            {
                LogWarning(_submitContext, nameof(Submit), $"Invalid filter, allowing submission regardless of filters. Exception: [{ex}]");
            }

            await InvokeNext(submitContext).ConfigureAwait(false);
        }

        private FilterResult ShouldExclude()
        {
            // If the Excluded filter is not present, ingest all content
            if (_submitContext?.Filters?.Excluded == null)
            {
                return new FilterResult() { Result = false };
            }

            return MatchesFilter(_submitContext.Filters.Excluded);
        }

        private FilterResult ShouldInclude()
        {
            // If the Included filter is not present, ingest all content
            if (_submitContext?.Filters?.Included == null)
            {
                return new FilterResult() { Result = true };
            }

            return MatchesFilter(_submitContext.Filters.Included);
        }

        private FilterResult MatchesFilter(SearchTreeNodeModel filter)
        {
            if(filter == null)
            {
                throw new ValidationException("Filter is null");
            }

            // If the SearchTerm is not null, evaluate using it
            if (filter.SearchTerm != null)
            {
                return FilterHelpers.MatchesFilter(_submitContext, filter.SearchTerm);
            }

            // If the children are null or empty, the filter is invalid
            if (!(filter.Children?.Any() ?? false))
            {
                throw new ValidationException("Filter has no SearchTerm and no Children");
            }

            // Otherwise, evaluate using the boolean operator and all the children of the SearchTerm
            var results = filter.Children.Select(x => MatchesFilter(x));
            switch (filter.BoolOperator)
            {
                case FilterConstants.FilterBooleanOperators.And:
                    {
                        var failures = results.Where(x => !x.Result);
                        if(failures.Any())
                        {
                            return GetResult(failures);
                        }

                        break;
                    }
                case FilterConstants.FilterBooleanOperators.Or:
                    {
                        var failures = results.Where(x => !x.Result);
                        if (failures.Count() == results.Count())
                        {
                            return GetResult(failures);
                        }

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException($"Filter has invalid BoolOperator [{filter.BoolOperator}]");
                    }
            }

            return new FilterResult() { Result = true };
        }

        private FilterResult GetResult(IEnumerable<FilterResult> results)
        {
            if (results.Count() == 1)
            {
                return results.First();
            }

            return new FilterResult()
            {
                Result = false,
                FailureReason = string.Join(",", results.Select(x => x.FailureReason).ToArray())
            };
        }
    }
}
