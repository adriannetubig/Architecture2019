﻿using BaseModel;
using ErrorLoggerBusiness.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ErrorLoggerApi.Helper
{
    public class ExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(IConfiguration iConfiguration, ILogger<Program> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IBusinessExceptionLogs iBusinessExceptionLogs)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex, iBusinessExceptionLogs);
            }
        }

        private async Task<Task> HandleException(HttpContext context, Exception exception, IBusinessExceptionLogs iBusinessExceptionLogs)
        {
            var requestResult = new RequestResult();
            try
            {
                await iBusinessExceptionLogs.Create(default, exception, Assembly.GetExecutingAssembly().GetName().Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception, $"{DateTime.UtcNow}");
                _logger.LogError(ex, $"{DateTime.UtcNow}");
            }
            finally
            {
                requestResult.Errors.Add("There was an Error"); //For External Api use this
                requestResult.Exceptions.Add(exception); //For Internal Api use this
            }
            return context.Response.WriteAsync(JsonConvert.SerializeObject(requestResult));
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}