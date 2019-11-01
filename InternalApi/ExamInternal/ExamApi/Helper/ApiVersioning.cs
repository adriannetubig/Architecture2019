using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ExamApi.Helper
{
    public static class ApiVersioning
    {
        public static void SetVersion(ref IServiceCollection services)
        {
            services.AddApiVersioning(a => {
                a.ReportApiVersions = true;
                a.AssumeDefaultVersionWhenUnspecified = true;
                a.DefaultApiVersion = new ApiVersion(1, 0);

                a.Conventions.Controller<Controllers.V1.FibonaccisController>().HasApiVersion(new ApiVersion(1, 0));
            });
        }
    }
}
