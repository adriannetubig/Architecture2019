using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Services;
using BaseConsumer.Interfaces;
using BaseConsumer.Services;
using BaseModel;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationExternalApi.Helper
{
    public class Dependency
    {
        public static void SetDependency(ref IServiceCollection services, InternalApiCredential internalApiCredential, string authenticationInternalUrl,
            JwtTokenSettings jwtTokenSettings, JwtTokenValidation jwtTokenValidation)
        {
            services.AddSingleton<IBaseApiConsumer, BaseApiConsumer>();

            services.AddSingleton<IBusinessApiAuthentication>(a => new BusinessApiAuthentication(a.GetService<IBaseApiConsumer>(), internalApiCredential));
            services.AddSingleton<IBusinessAuthentications>(a => new BusinessAuthentications(jwtTokenSettings, jwtTokenValidation));
            services.AddSingleton<IBusinessUsers>(a => new BusinessUsers(a.GetService<IBaseApiConsumer>(), a.GetService<IBusinessApiAuthentication>(), authenticationInternalUrl));
        }
    }
}
