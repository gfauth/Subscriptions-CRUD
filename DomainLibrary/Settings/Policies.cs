using Microsoft.Data.SqlClient;
using Polly;
using Polly.Wrap;

namespace DomainLibrary.Settings
{
    public sealed class Policies
    {
        private static readonly int _circuitBreakerExceptionsAllowedCount = 5;
        private static readonly int _circuitBreakerDuration = 5;
        private static readonly int _retryCount = 3;

        public static AsyncPolicyWrap GetPolicyAsync()
        {
            var policyCircuitBreaker = Policy.Handle<SqlException>().CircuitBreakerAsync(
                    _circuitBreakerExceptionsAllowedCount,
                    TimeSpan.FromMinutes(_circuitBreakerDuration)
                );

            var policyRetry = Policy.Handle<SqlException>().WaitAndRetryAsync(
                    retryCount: _retryCount,
                    sleepDurationProvider: retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                );

            return Policy.WrapAsync(policyCircuitBreaker, policyRetry);
        }
    }
}