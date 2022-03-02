// <copyright file="HealthCheckConfiguration.cs" company="Air Liquide">
// Copyright (c) Air Liquide. All rights reserved.
// </copyright>

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Delta.Standard.Gateway.Api.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static void ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var endpoints = configuration.GetSection(EndpointConfiguration.SectionName)
                                                     .Get<EndpointConfiguration>();

            services.AddHealthChecks()
                    .AddUrlGroup(new Uri(endpoints.StandardHealth), name: "Standard API", tags: new[] { "standard-status" })
                    .AddCheck("Own Health", () => HealthCheckResult.Healthy(), tags: new[] { "api-status" });
        }

        public static void MapEndpointsHealthCheck(this IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("api-status"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/health/standard", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("standard-status"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/health/all", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
