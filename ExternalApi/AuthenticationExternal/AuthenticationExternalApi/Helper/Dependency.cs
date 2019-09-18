using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Services;
using BaseModel;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationExternalApi.Helper
{
    public class Dependency
    {
        public static void SetDependency(ref IServiceCollection services, InternalApiCredential internalApiCredential, string authenticationInternalUrl)
        {
            services.AddSingleton<IBusinessApiAuthentication>(a => new BusinessApiAuthentication(internalApiCredential));
            services.AddScoped<IBusinessAuthentications>(a => new BusinessAuthentications(authenticationInternalUrl));
            services.AddScoped<IBusinessUsers>(a => new BusinessUsers(a.GetService<IBusinessApiAuthentication>(), authenticationInternalUrl));
        }
    }
}
