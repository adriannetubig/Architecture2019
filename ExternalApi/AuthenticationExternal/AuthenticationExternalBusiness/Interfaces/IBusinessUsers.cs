using AuthenticationExternalBusiness.Models;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalBusiness.Interfaces
{
    public interface IBusinessUsers
    {
        Task<RequestResult<User>> Authenticate(User user, CancellationToken cancellationToken);
        Task<RequestResult> ChangePassword(User user, CancellationToken cancellationToken);
    }
}
