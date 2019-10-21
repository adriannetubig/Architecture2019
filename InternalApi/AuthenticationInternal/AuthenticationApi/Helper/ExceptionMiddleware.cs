using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BaseModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace AuthenticationApi.Helper
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                //ToDo: Add Exception Logger here

                //Custom Exception Handler
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        //Custom Exception Handler
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var requestResult = new RequestResult();
            requestResult.Errors.Add("There was an Error");
            return context.Response.WriteAsync(JsonConvert.SerializeObject(requestResult));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
