using AuthenticationBusiness.Models;
using BaseModel;

namespace AuthenticationBusiness.Interfaces
{
    public interface IBusinessAuthentications
    {
        RequestResult<Authentication> Create(string refreshToken, User user);
    }
}
