using BaseModel;
using ExamBusiness.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ExamBusiness.Interfaces
{
    public interface IBusinessFibonaccis
    {
        Task<RequestResult<Fibonacci>> Create(Fibonacci fibonacci, CancellationToken cancellationToken);
        Task<RequestResult<Fibonacci>> Read(int fibonacciId, CancellationToken cancellationToken);
        Task<RequestResult<PagedList<Fibonacci>>> Read(PageFilter pageFilter, CancellationToken cancellationToken);
        Task<RequestResult> Update(Fibonacci fibonacci, CancellationToken cancellationToken);
        Task<RequestResult> Delete(int fibonacciId, CancellationToken cancellationToken);
    }
}
