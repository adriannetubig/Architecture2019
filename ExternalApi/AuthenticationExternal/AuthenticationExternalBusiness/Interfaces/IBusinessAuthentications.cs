using AuthenticationExternalBusiness.Models;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalBusiness.Interfaces
{
    public interface IBusinessAuthentications
    {
        Task<RequestResult<Authentication>> Authenticate(User user, CancellationToken cancellationToken);
    }
}
