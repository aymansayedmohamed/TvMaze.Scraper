using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace TvMaze.Scraper.Synchronizer.Extentions
{
    public static class IServiceCollectionExtensions
    {
        public static void SetupHttpClient<TClient, TClientImplementation>(this IServiceCollection services, string name, Action<HttpClient> configuration)
            where TClient : class
            where TClientImplementation : class, TClient
        {
            services.AddHttpClient<TClient, TClientImplementation>(name, configuration)
                .AddPolicyHandler((svc, request) => HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(8)
                },
                onRetry: (outcome, timespan, retryAttempt, context) => {
                    var logger = svc.GetService<ILogger<TClientImplementation>>();
                    if (logger != null)
                        logger.LogWarning($"Delaying for {timespan.TotalMilliseconds}ms, then making a retry #{retryAttempt}.");
                }));
        }
    }
}
