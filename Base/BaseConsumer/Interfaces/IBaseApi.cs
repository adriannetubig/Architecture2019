using System.Threading;
using System.Threading.Tasks;

namespace BaseConsumer.Interfaces
{
    public interface IBaseApi
    {
        void SetUrl(string url);
        Task Delete(string requestUri, CancellationToken cancellationToken);
        Task Delete(string requestUri, string jwtToken, CancellationToken cancellationToken);
        Task<Result> Delete<Result>(string requestUri, CancellationToken cancellationToken);
        Task<Result> Delete<Result>(string requestUri, string jwtToken, CancellationToken cancellationToken);
        Task Get(string requestUri, CancellationToken cancellationToken);
        Task Get(string requestUri, string jwtToken, CancellationToken cancellationToken);
        Task<Result> Get<Result>(string requestUri, CancellationToken cancellationToken);
        Task<Result> Get<Result>(string requestUri, string jwtToken, CancellationToken cancellationToken);
        Task Post<T>(T t, string requestUri, CancellationToken cancellationToken);
        Task Post<T>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken);
        Task<Result> Post<T, Result>(T t, string requestUri, CancellationToken cancellationToken);
        Task<Result> Post<T, Result>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken);
        Task Put<T>(T t, string requestUri, CancellationToken cancellationToken);
        Task Put<T>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken);
        Task<Result> Put<T, Result>(T t, string requestUri, CancellationToken cancellationToken);
        Task<Result> Put<T, Result>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken);
    }
}
