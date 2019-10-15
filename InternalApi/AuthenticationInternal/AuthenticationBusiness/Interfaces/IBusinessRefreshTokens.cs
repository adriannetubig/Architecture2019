using AuthenticationBusiness.Models;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationBusiness.Interfaces
{
    public interface IBusinessRefreshTokens
    {
        Task<RequestResult<RefreshToken>> Create(int userId, CancellationToken cancellationToken);
        Task<RequestResult<RefreshToken>> Refresh(int userId, string refreshToken, CancellationToken cancellationToken);
        Task<RequestResult> Delete(int userId, CancellationToken cancellationToken);
    }
}
