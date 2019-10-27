using BaseModel;
using ErrorLoggerBusiness.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorLoggerBusiness.Interfaces
{
    public interface IBusinessExceptionLogs
    {
        Task<RequestResult<ExceptionLog>> Create(CancellationToken cancellationToken, Exception exception, string applicationName);
        Task<RequestResult<ExceptionLog>> Create(CancellationToken cancellationToken, ExceptionLog exceptionLog, string applicationName);
        Task<RequestResult<PagedList<ExceptionLog>>> Read(CancellationToken cancellationToken, bool ascending, int itemsPerPage, int pageNo, string sortBy);
    }
}
