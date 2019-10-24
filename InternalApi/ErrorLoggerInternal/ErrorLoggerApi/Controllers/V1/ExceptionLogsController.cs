using ErrorLoggerBusiness.Interfaces;
using ErrorLoggerBusiness.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorLoggerApi.Controllers
{
    public class ExceptionLogsController : BaseControllerV1
    {
        private readonly IBusinessExceptionLogs _iBusinessExceptionLogs;
        public ExceptionLogsController(IBusinessExceptionLogs iBusinessExceptionLogs)
        {
            _iBusinessExceptionLogs = iBusinessExceptionLogs;
        }

        [HttpPut("{applicationName}")]
        public async Task<IActionResult> Create(ExceptionLog exceptionLog, string applicationName)
        {
            var requestResult = await _iBusinessExceptionLogs.Create(default, exceptionLog, applicationName); //We will log the issue regardless if the request was cancelled
            return Created(requestResult);
        }

        [HttpGet("{ascending}/{itemsPerPage}/{pageNo}/{sortBy}")]
        public async Task<IActionResult> Read(bool ascending, int itemsPerPage, int pageNo, string sortBy, CancellationToken cancellationToken)
        {
            return Ok(await _iBusinessExceptionLogs.Read(cancellationToken, ascending, itemsPerPage, pageNo, sortBy));
        }
    }
}
