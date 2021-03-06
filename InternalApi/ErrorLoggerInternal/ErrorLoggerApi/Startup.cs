using ErrorLoggerApi.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ErrorLoggerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();
            var connectionString = Configuration.GetConnectionString("Default");

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CORS", corsPolicyBuilder => corsPolicyBuilder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(allowedOrigins)
                    .AllowCredentials());
            });
            Dependency.SetDependency(ref services, connectionString);
            services.AddSingleton(Helper.AutoMapper.Config());

            ApiVersioning.SetVersion(ref services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionMiddleware();
            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CORS");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
