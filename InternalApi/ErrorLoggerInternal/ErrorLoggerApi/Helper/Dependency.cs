﻿using BaseData.Interfaces;
using BaseData.Services;
using ErrorLoggerBusiness.Interfaces;
using ErrorLoggerBusiness.Services;
using ErrorLoggerData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ErrorLoggerApi.Helper
{
    public static class Dependency
    {
        public static void SetDependency(ref IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ErrorLoggerContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, ErrorLoggerContext>();
            services.AddScoped<IRepoBase, RepoBase>();

            services.AddScoped<IBusinessExceptionLogs, BusinessExceptionLogs>();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .AddEventLog();
            });
            loggerFactory.AddFile("Logs/ErrorLogs-{Date}.txt");

            services.AddSingleton(loggerFactory.CreateLogger<Program>());
        }
    }
}
