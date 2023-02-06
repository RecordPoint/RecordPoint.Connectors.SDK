using Polly;
using Polly.Fallback;

namespace RecordPoint.Connectors.SDK.Extensions
{
    /// <summary>
    /// The default FallbackPolicy enforces return type for generics, if not specified, InvalidOperationException will be thrown: https://github.com/App-vNext/Polly/issues/292 
    /// For non-generics, the same exception will occur if a return type is specified.
    /// However, as we are using implicit generic for Polly (Not specifying a return type but rely on compiler figuring out), there is no way for FallbackPolicy to know whether it should return a type at compile time
    /// At this stage, all we need for fallback is to throw TooManyRequestException without returning anything. Hence we made this class to bypass the runtime InvalidOperationException
    /// This class and the extension underneath are simplified versions of: https://github.com/App-vNext/Polly/tree/57a82e8e43e23935778aef8a944b88d17ccf0cc7/src/Polly.Shared/Fallback
    /// Needs to be updated if Polly is upgraded above 6.1.2
    /// </summary>
    public class CustomFallbackPolicy : Policy, IFallbackPolicy
    {
        /// <summary>
        /// Construct sync CustomFallbackPolicy
        /// </summary>
        /// <param name="exceptionPolicy"></param>
        /// <param name="exceptionPredicates"></param>
        public CustomFallbackPolicy(Action<Action<Polly.Context, CancellationToken>, Polly.Context, CancellationToken> exceptionPolicy, IEnumerable<ExceptionPredicate> exceptionPredicates)
               : base(exceptionPolicy, exceptionPredicates)
        {
        }

        /// <summary>
        /// Construct async CustomFallbackPolicy
        /// </summary>
        /// <param name="asyncExceptionPolicy"></param>
        /// <param name="exceptionPredicates"></param>
        public CustomFallbackPolicy(Func<Func<Polly.Context, CancellationToken, Task>, Polly.Context, CancellationToken, bool, Task> asyncExceptionPolicy, IEnumerable<ExceptionPredicate> exceptionPredicates) : base(asyncExceptionPolicy, exceptionPredicates)
        {
        }

        /// <summary>
        /// Sync extension method to construct CustomFallbackPolicy
        /// Simplified version of: https://github.com/App-vNext/Polly/blob/57a82e8e43e23935778aef8a944b88d17ccf0cc7/src/Polly.Shared/Fallback/FallbackSyntax.cs
        /// </summary>
        /// <param name="fallBackAction"></param>
        /// <returns></returns>
        public static CustomFallbackPolicy CustomFallback<TException>(Action<CancellationToken> fallBackAction) where TException : Exception
        {
            return new CustomFallbackPolicy(
                (action, context, cancellationToken) =>
                CustomFallbackEngine.Implementation(
                    action,
                    context,
                    cancellationToken,
                    GetPredicatedException<TException>(),
                    fallBackAction),
                GetPredicatedException<TException>());
        }

        /// <summary>
        /// Async extension method to construct CustomFallbackPolicy
        /// Simplified version of: https://github.com/App-vNext/Polly/blob/57a82e8e43e23935778aef8a944b88d17ccf0cc7/src/Polly.Shared/Fallback/FallbackSyntaxAsync.cs
        /// </summary>
        /// <param name="fallBackAction"></param>
        /// <returns></returns>
        public static CustomFallbackPolicy CustomFallbackAsync<TException>(Action<CancellationToken> fallBackAction) where TException : Exception
        {
            return new CustomFallbackPolicy(
                (action, context, cancellationToken, continueOnCapturedContext) =>
                CustomFallbackEngine.ImplementationAsync(
                    action,
                    context,
                    cancellationToken,
                    GetPredicatedException<TException>(),
                    continueOnCapturedContext,
                    fallBackAction),
                GetPredicatedException<TException>());
        }

        /// <summary>
        /// Making sure the Exception of Policy.OnException is handled
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <returns></returns>
        private static IEnumerable<ExceptionPredicate> GetPredicatedException<TException>() where TException : Exception
        {
            return new List<ExceptionPredicate>()
            {
                exception => exception is TException ? exception : null
            };
        }
    }

    static class CustomFallbackEngine
    {
        /// <summary>
        /// Handling action from async CustomFallbackPolicy
        /// Simplifed version of: https://github.com/App-vNext/Polly/blob/57a82e8e43e23935778aef8a944b88d17ccf0cc7/src/Polly.Shared/Fallback/FallbackEngine.cs
        /// </summary>
        /// <param name="action"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="shouldHandleExceptionPredicates"></param>
        /// <param name="fallBackAction"></param>
        internal static void Implementation(
               Action<Polly.Context, CancellationToken> action,
               Polly.Context context,
               CancellationToken cancellationToken,
               IEnumerable<ExceptionPredicate> shouldHandleExceptionPredicates,
               Action<CancellationToken> fallBackAction)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                action(context, cancellationToken);
            }
            catch (Exception ex)
            {
                var handledException = shouldHandleExceptionPredicates
                    .Select(predicate => predicate(ex))
                    .FirstOrDefault(e => e != null);
                // If we should handle the exception, go for fallbackAction e.g. swallow the exception and throw a TooManyRequestsException
                if (handledException != null)
                {
                    fallBackAction(cancellationToken);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Handling action from async CustomFallbackPolicy
        /// Simplified version of: https://github.com/App-vNext/Polly/blob/57a82e8e43e23935778aef8a944b88d17ccf0cc7/src/Polly.Shared/Fallback/FallbackEngineAsync.cs
        /// </summary>
        /// <param name="action"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="shouldHandleExceptionPredicates"></param>
        /// <param name="continueOnCapturedContext"></param>
        /// <param name="fallBackAction"></param>
        /// <returns></returns>
        internal static async Task ImplementationAsync(
           Func<Polly.Context, CancellationToken, Task> action,
           Polly.Context context,
           CancellationToken cancellationToken,
           IEnumerable<ExceptionPredicate> shouldHandleExceptionPredicates,
           bool continueOnCapturedContext,
           Action<CancellationToken> fallBackAction)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await action(context, cancellationToken).ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                var handledException = shouldHandleExceptionPredicates
                    .Select(predicate => predicate(ex))
                    .FirstOrDefault(e => e != null);
                // If we should handle the exception, go for fallbackAction e.g. swallow the exception and throw a TooManyRequestsException
                if (handledException != null)
                {
                    fallBackAction(cancellationToken);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
