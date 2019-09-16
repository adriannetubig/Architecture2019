using AuthenticationBusiness.Models;
using BaseModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationBusiness.Interfaces
{
    public interface IBusinessRoles
    {
        Task<RequestResult<List<Role>>> Read(CancellationToken cancellationToken);

    }
}
