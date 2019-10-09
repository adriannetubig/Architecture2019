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

namespace ExamApi.Helper
{
    public static class Dependency
    {
        public static void SetDependency(ref IServiceCollection services, string connectionString, JwtTokenSettings jwtTokenSettings, JwtTokenValidation jwtTokenValidation)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, Context>();
            services.AddScoped<IRepoBase, RepoBase>();

            services.AddScoped<IDataFibonaccis>(a => new DataFibonaccis(connectionString));

            services.AddScoped<IBusinessFibonaccis, BusinessFibonaccis>();
        }
    }
}
