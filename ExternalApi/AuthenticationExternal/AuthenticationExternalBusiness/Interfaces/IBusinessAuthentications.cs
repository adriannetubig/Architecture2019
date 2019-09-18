using AuthenticationExternalBusiness.Models;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalBusiness.Interfaces
{
    public interface IBusinessAuthentications
    {
        RequestResult<Authentication> Create(string refreshToken, User user);
    }
}
