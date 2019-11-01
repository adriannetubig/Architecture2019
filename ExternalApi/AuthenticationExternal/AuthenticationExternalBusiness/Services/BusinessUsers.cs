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
        private readonly IBaseApiConsumer _iBaseApiConsumer;
        private readonly IBusinessApiAuthentication _iBusinessApiAuthentication;
        public BusinessUsers(IBaseApiConsumer iBaseApiConsumer, IBusinessApiAuthentication iBusinessApiAuthentication, string url)
        {
            _iBaseApiConsumer = iBaseApiConsumer;
            _iBusinessApiAuthentication = iBusinessApiAuthentication;
            _iBaseApiConsumer.SetUrl(url);
        }

        public async Task<RequestResult<User>> Authenticate(User user, CancellationToken cancellationToken)
        {
            var token = await _iBusinessApiAuthentication.ReadToken(cancellationToken);
            return await _iBaseApiConsumer.Post<User, RequestResult<User>>(user, "/api/v1/Users/Authenticate", token, cancellationToken);
        }

        public async Task<RequestResult> ChangePassword(User user, CancellationToken cancellationToken)
        {
            var token = await _iBusinessApiAuthentication.ReadToken(cancellationToken);
            return await _iBaseApiConsumer.Post<User, RequestResult>(user, "/api/v1/Users/ChangePassword", token, cancellationToken);
        }
    }
}
