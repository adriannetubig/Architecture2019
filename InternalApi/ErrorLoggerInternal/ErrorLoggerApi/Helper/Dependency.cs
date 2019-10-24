using BaseData.Interfaces;
using BaseData.Services;
using ErrorLoggerBusiness.Interfaces;
using ErrorLoggerBusiness.Services;
using ErrorLoggerData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        }
    }
}
