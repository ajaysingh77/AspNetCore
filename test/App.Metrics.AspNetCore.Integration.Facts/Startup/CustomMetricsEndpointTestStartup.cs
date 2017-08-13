﻿// <copyright file="CustomMetricsEndpointTestStartup.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using App.Metrics.AspNetCore.Endpoints;
using App.Metrics.Formatters.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.Metrics.AspNetCore.Integration.Facts.Startup
{
    public class CustomMetricsEndpointTestStartup : TestStartup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMetricsEndpoint();
            app.UseMetricsAllMiddleware();

            SetupAppBuilder(app, env, loggerFactory);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appMetricsOptions = new MetricsOptions
                                    {
                                        DefaultOutputMetricsFormatter = new MetricsJsonOutputFormatter()
                                    };

            var endpointsOptions = new MetricsEndpointsOptions
            {
                MetricsEndpoint = new PathString("/metrics-json")
            };

            var aspNetCoreOptions = new MetricsAspNetCoreOptions();

            SetupServices(services, appMetricsOptions, aspNetCoreOptions, endpointsOptions);
        }
    }
}