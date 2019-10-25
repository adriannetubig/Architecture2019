using System;
using System.Reflection;
using System.Threading.Tasks;
using BaseConsumer.Interfaces;
using BaseModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuthenticationApi.Helper
{
    public class ExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IBaseApiConsumer _iBaseApiConsumer;
        

        public ExceptionMiddleware(IConfiguration iConfiguration, ILogger<Program> logger, RequestDelegate next, IBaseApiConsumer iBaseApiConsumer)
        {
            _logger = logger;
            _next = next;
            _iBaseApiConsumer = iBaseApiConsumer;
            var errorLoggergUrl = iConfiguration.GetSection("ErrorLoggergUrl").Get<string>();
            _iBaseApiConsumer.SetUrl(errorLoggergUrl);
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task<Task> HandleException(HttpContext context, Exception exception)
        {


            var requestResult = new RequestResult();
            try
            {
                await _iBaseApiConsumer.Put(exception, $"api/v1/ExceptionLogs/{Assembly.GetExecutingAssembly().GetName().Name}", default);
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
