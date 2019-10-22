using System.Threading;
using System.Threading.Tasks;

namespace BaseConsumer.Interfaces
{
    public interface IBaseApi
    {
        void SetUrl(string url);
        Task Post<T>(T t, string requestUri, CancellationToken cancellationToken);
        Task Post<T>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken);
        Task<Result> Post<T, Result>(T t, string requestUri, CancellationToken cancellationToken);
        Task<Result> Post<T, Result>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken);
    }
}
