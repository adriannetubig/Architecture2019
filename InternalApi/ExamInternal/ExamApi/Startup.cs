using System.Text;
using BaseModel;
using ExamApi.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ExamApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtTokenSettings = Configuration.GetSection("JwtTokenSettings").Get<JwtTokenSettings>();
            var jwtTokenValidation = Configuration.GetSection("JwtTokenValidation").Get<JwtTokenValidation>();
            var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtTokenValidation.ValidIssuer,
                    ValidAudience = jwtTokenValidation.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenValidation.IssuerSigningKey)),
                    ClockSkew = jwtTokenValidation.ClockSkew
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CORS", corsPolicyBuilder => corsPolicyBuilder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(allowedOrigins)
                    .AllowCredentials());
            });

            var connectionString = Configuration.GetConnectionString("Default");

            Dependency.SetDependency(ref services, connectionString, jwtTokenSettings, jwtTokenValidation);

            services.AddSingleton(Helper.AutoMapper.Config());

            ApiVersioning.SetVersion(ref services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionMiddleware();
            app.UseRouting();
            app.UseAuthentication();

            app.UseCors("CORS");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
