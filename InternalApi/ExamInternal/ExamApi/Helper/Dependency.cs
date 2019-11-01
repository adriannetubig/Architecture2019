using BaseConsumer.Interfaces;
using BaseConsumer.Services;
using BaseData.Interfaces;
using BaseData.Services;
using BaseModel;
using ExamBusiness.Interfaces;
using ExamBusiness.Services;
using ExamData;
using ExamData.Interfaces;
using ExamData.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExamApi.Helper
{
    public static class Dependency
    {
        public static void SetDependency(ref IServiceCollection services, string connectionString, JwtTokenSettings jwtTokenSettings, JwtTokenValidation jwtTokenValidation)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, Context>();
            services.AddScoped<IRepoBase, RepoBase>();
            services.AddSingleton<IBaseApiConsumer, BaseApiConsumer>();

            services.AddScoped<IDataFibonaccis>(a => new DataFibonaccis(connectionString));

            services.AddScoped<IBusinessFibonaccis, BusinessFibonaccis>();

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
