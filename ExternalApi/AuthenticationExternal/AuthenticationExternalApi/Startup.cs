using System.Text;
using BaseModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationExternalApi
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
            var authenticationInternalUrl = Configuration.GetSection("AuthenticationInternalUrl").Get<string>();
            var jwtTokenSettings = Configuration.GetSection("JwtTokenSettings").Get<JwtTokenSettings>();
            var jwtTokenValidation = Configuration.GetSection("JwtTokenValidation").Get<JwtTokenValidation>();
            var internalApiCredential = Configuration.GetSection("InternalApiCredential").Get<InternalApiCredential>();
            var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();

            services.AddControllers();

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

            Helper.Dependency.SetDependency(ref services, internalApiCredential, authenticationInternalUrl, jwtTokenSettings, jwtTokenValidation);

            services.AddApiVersioning(a => {
                a.ReportApiVersions = true;
                a.AssumeDefaultVersionWhenUnspecified = true;
                a.DefaultApiVersion = new ApiVersion(1, 0);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CORS");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
