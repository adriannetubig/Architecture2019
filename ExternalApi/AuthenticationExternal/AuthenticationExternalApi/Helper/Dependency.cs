using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Services;
using BaseModel;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationExternalApi.Helper
{
    public class Dependency
    {
        public static void SetDependency(ref IServiceCollection services, InternalApiCredential internalApiCredential, string authenticationInternalUrl,
            JwtTokenSettings jwtTokenSettings, JwtTokenValidation jwtTokenValidation)
        {
            services.AddSingleton<IBusinessApiAuthentication>(a => new BusinessApiAuthentication(internalApiCredential));
            services.AddScoped<IBusinessAuthentications>(a => new BusinessAuthentications(jwtTokenSettings, jwtTokenValidation));
            services.AddScoped<IBusinessUsers>(a => new BusinessUsers(a.GetService<IBusinessApiAuthentication>(), authenticationInternalUrl));
        }
    }
}
