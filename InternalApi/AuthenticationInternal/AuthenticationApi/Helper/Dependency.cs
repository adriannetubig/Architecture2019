﻿using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Services;
using AuthenticationData;
using AuthenticationData.Interfaces;
using AuthenticationData.Services;
using BaseData.Interfaces;
using BaseData.Services;
using BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BaseConsumer.Interfaces;
using BaseConsumer.Services;
using Microsoft.Extensions.Logging;

namespace AuthenticationApi.Helper
{
    public static class Dependency
    {
        public static void SetDependency(ref IServiceCollection services, string connectionString, JwtTokenSettings jwtTokenSettings, JwtTokenValidation jwtTokenValidation)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, Context>();
            services.AddScoped<IRepoBase, RepoBase>();
            services.AddSingleton<IBaseApiConsumer, BaseApiConsumer>();

            services.AddScoped<IDataUsers>(a => new DataUsers(connectionString));

            services.AddScoped<IBusinessAuthentications>(a => new BusinessAuthentications(jwtTokenSettings, jwtTokenValidation));
            services.AddScoped<IBusinessRefreshTokens, BusinessRefreshTokens>();
            services.AddScoped<IBusinessRoles, BusinessRoles>();
            services.AddScoped<IBusinessUsers, BusinessUsers>();

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
