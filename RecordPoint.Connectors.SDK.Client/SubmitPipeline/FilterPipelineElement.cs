using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterPipelineElement : SubmitPipelineElementBase
    {

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
            var excludeMatches = MatchesFilter(submitContext, submitContext?.Filters?.Excluded, false);
            if (excludeMatches.Result)
            {
                SkipNext(submitContext, excludeMatches.MatchReason);
                return;
            }

            var includeMatches = MatchesFilter(submitContext, submitContext?.Filters?.Included, true);
            if (!includeMatches.Result)
            {
                SkipNext(submitContext, includeMatches.MatchReason);
                return;
            }

            await InvokeNext(submitContext).ConfigureAwait(false);
        }

        private MatchResult MatchesFilter(SubmitContext submitContext, SearchTreeNodeModel filter, bool nullResult)
        {
            if (filter == null)
            {
                return new MatchResult() { Result = nullResult };
            }

            // If the SearchTerm is not null, evaluate
            if (filter.SearchTerm != null)
            {
                return FilterHelpers.MatchesFilter(submitContext, filter.SearchTerm);
            }

            // If the children are null or empty, filter matches
            if (!(filter.Children?.Any() ?? false))
            {
                return new MatchResult() { Result = nullResult };
            }

            // Otherwise, evaluate using the boolean operator and all the children of the SearchTerm
            var results = filter.Children.Select(x => MatchesFilter(submitContext, x, nullResult));
            switch (filter.BoolOperator)
            {
                case FilterConstants.FilterBooleanOperators.And:
                    {
                        if (results.All(x => x.Result))
                        {
                            return GetResult(results);
                        }

                        break;
                    }
                case FilterConstants.FilterBooleanOperators.Or:
                    {
                        var matches = results.Where(x => x.Result);
                        if (matches.Any())
                        {
                            return GetResult(matches);
                        }

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException($"Filter has invalid BoolOperator [{filter.BoolOperator}]");
                    }
            }

            return new MatchResult() { Result = false };
        }

        private MatchResult GetResult(IEnumerable<MatchResult> results)
        {
            if (results.Count() == 1)
            {
                return results.First();
            }

            return new MatchResult()
            {
                Result = true,
                MatchReason = string.Join(",", results.Select(x => x.MatchReason).ToArray())
            };
        }
    }
}
