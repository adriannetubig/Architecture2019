using AuthenticationExternalBusiness.Interfaces;
using AuthenticationExternalBusiness.Models;
using BaseConsumer.Interfaces;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalBusiness.Services
{
    public class BusinessApiAuthentication: IBusinessApiAuthentication
    {
        private readonly IBaseApiConsumer _iBaseApiConsumer;
        private readonly InternalApiCredential _internalApiCredential;
        private Authentication _authentication;

        public BusinessApiAuthentication(IBaseApiConsumer iBaseApiConsumer, InternalApiCredential internalApiCredential)
        {
            _iBaseApiConsumer = iBaseApiConsumer;
            _internalApiCredential = internalApiCredential;
            _iBaseApiConsumer.SetUrl(_internalApiCredential.Url);
        }

        public async Task<string> ReadToken(CancellationToken cancellationToken)//ToDo: function to renew token
        {
            if (_authentication == null)
                await Authenticate(cancellationToken);

            return _authentication.Token;
        }

        private async Task Authenticate(CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = _internalApiCredential.Username,
                Password = _internalApiCredential.Password
            };
            var authenticationResult = await _iBaseApiConsumer.Post<User, RequestResult<Authentication>>(user, "/api/v1/Authentications", cancellationToken);

            if (authenticationResult.Succeeded)
                _authentication = authenticationResult.Model;
        }
    }
}
