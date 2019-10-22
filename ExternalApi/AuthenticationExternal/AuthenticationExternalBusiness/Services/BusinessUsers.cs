using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Models;
using BaseConsumer.Interfaces;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalBusiness.Services
{
    public class BusinessUsers : IBusinessUsers
    {
        private readonly IBaseApi _iBaseApi;
        private readonly IBusinessApiAuthentication _iBusinessApiAuthentication;
        public BusinessUsers(IBaseApi iBaseApi, IBusinessApiAuthentication iBusinessApiAuthentication, string url)
        {
            _iBaseApi = iBaseApi;
            _iBusinessApiAuthentication = iBusinessApiAuthentication;
            _iBaseApi.SetUrl(url);
        }

        public async Task<RequestResult<User>> Authenticate(User user, CancellationToken cancellationToken)
        {
            var token = await _iBusinessApiAuthentication.ReadToken(cancellationToken);
            return await _iBaseApi.Post<User, RequestResult<User>>(user, "/api/v1/Users/Authenticate", token, cancellationToken);
        }

        public async Task<RequestResult> ChangePassword(User user, CancellationToken cancellationToken)
        {
            var token = await _iBusinessApiAuthentication.ReadToken(cancellationToken);
            return await _iBaseApi.Post<User, RequestResult>(user, "/api/v1/Users/ChangePassword", token, cancellationToken);
        }
    }
}
