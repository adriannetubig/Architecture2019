using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationExternalBusiness.Interfaces
{
    public interface IBusinessApiAuthentication
    {
        Task<string> ReadToken(CancellationToken cancellationToken);
    }
}
