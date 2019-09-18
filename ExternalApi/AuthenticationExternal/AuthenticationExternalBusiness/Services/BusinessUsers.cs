using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Models;
using BaseConsumer.Services;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalBusiness.Services
{
    public class BusinessUsers : BaseApi, IBusinessUsers
    {
        private readonly IBusinessApiAuthentication _iBusinessApiAuthentication;
        public BusinessUsers(IBusinessApiAuthentication iBusinessApiAuthentication, string url): base(url)
        {
            _iBusinessApiAuthentication = iBusinessApiAuthentication;
        }

        public async Task<RequestResult<User>> Authenticate(User user, CancellationToken cancellationToken)
        {
            var token = await _iBusinessApiAuthentication.ReadToken(cancellationToken);
            return await Post<User, RequestResult<User>>(user, "/api/v1/Users/Authenticate", token, cancellationToken);
        }

        public async Task<RequestResult> ChangePassword(User user, CancellationToken cancellationToken)
        {
            var token = await _iBusinessApiAuthentication.ReadToken(cancellationToken);
            return await Post<User, RequestResult>(user, "/api/v1/Users/ChangePassword", token, cancellationToken);
        }
    }
}
