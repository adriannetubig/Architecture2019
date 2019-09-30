using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using BaseModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationApi.Controllers.V1
{
    public class FibonaccisController : BaseControllerV1
    {
        private readonly IBusinessRoles _iBusinessRoles;
        private readonly IBusinessFibonaccis _iBusinessFibonaccis;

        public FibonaccisController(IBusinessRoles iBusinessRoles, IBusinessFibonaccis iBusinessFibonaccis)
        {
            _iBusinessRoles = iBusinessRoles;
            _iBusinessFibonaccis = iBusinessFibonaccis;
        }

        [HttpPut]
        public async Task<IActionResult> Create(Fibonacci fibonacci, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessFibonaccis.Create(fibonacci, cancellationToken);
            return Ok(requestResult);
        }

        [HttpGet("{fibonacciId}")]
        public async Task<IActionResult> Read(int fibonacciId, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessFibonaccis.Read(fibonacciId, cancellationToken);
            return Ok(requestResult);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Fibonacci fibonacci, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessFibonaccis.Update(fibonacci, cancellationToken);
            return Ok(requestResult);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Read(PageFilter pageFilter, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessFibonaccis.Read(pageFilter, cancellationToken);

            return Ok(requestResult);
        }

        [HttpDelete("{fibonacciId}")]
        public async Task<IActionResult> Delete(int fibonacciId, CancellationToken cancellationToken)
        {
            var requestResult = await _iBusinessFibonaccis.Delete(fibonacciId, cancellationToken);
            return Ok(requestResult);
        }
    }
}