using AuthenticationBusiness.Models;
using BaseModel;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationBusiness.Interfaces
{
    public interface IBusinessUsers
    {
        Task<RequestResult<User>> Create(User user, CancellationToken cancellationToken);
        Task<RequestResult<User>> Read(int userId, CancellationToken cancellationToken);
        Task<RequestResult<PagedList<User>>> Read(PageFilter pageFilter, CancellationToken cancellationToken);
        Task<RequestResult<User>> Authenticate(User user, CancellationToken cancellationToken);
        Task<RequestResult<User>> Validate(User user, CancellationToken cancellationToken);
        Task<RequestResult> ChangePassword(User user, CancellationToken cancellationToken);
        Task<RequestResult> Update(User user, CancellationToken cancellationToken);
        Task<RequestResult> UpdatePassword(User user, int updatedBy, CancellationToken cancellationToken);
        Task<RequestResult> Delete(int userId, int deletedBy, CancellationToken cancellationToken);
    }
}
