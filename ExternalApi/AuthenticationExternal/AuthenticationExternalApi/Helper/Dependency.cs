using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationExternalApi.Helper
{
    public class Dependency
    {
        public static void SetDependency(ref IServiceCollection services, string authenticationInternalUrl)
        {
            services.AddScoped<IBusinessAuthentications>(a => new BusinessAuthentications(authenticationInternalUrl));
            services.AddScoped<IBusinessUsers>(a => new BusinessUsers(IBusinessApiAuthentication, authenticationInternalUrl));
        }
    }
}
