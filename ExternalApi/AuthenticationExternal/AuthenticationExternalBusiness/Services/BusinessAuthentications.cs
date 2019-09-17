using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Models;
using BaseConsumer;
using BaseConsumer.Services;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalBusiness.Services
{
    public class BusinessAuthentications : BaseApi, IBusinessAuthentications
    {
        public BusinessAuthentications(string url): base(url)
        {
        }

        public async Task<RequestResult<Authentication>> Authenticate(User user, CancellationToken cancellationToken)
        {
            return await Post<User, RequestResult<Authentication>>(user, "/api/v1/Authentications", cancellationToken);
        }
    }
}
