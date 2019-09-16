﻿using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using BaseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationApi.Controllers.V1
{
    public class AuthenticationsController : BaseControllerV1
    {
        private readonly IBusinessAuthentications _iBAuthentications;
        private readonly IBusinessUsers _iBUsers;

        public AuthenticationsController(IBusinessAuthentications iBAuthentications, IBusinessUsers iBUsers)
        {
            _iBAuthentications = iBAuthentications;
            _iBUsers = iBUsers;
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Read(User user, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<Authentication>();

            var requestResultUser = await _iBUsers.Authenticate(user, cancellationToken);

            requestResult.Add(requestResultUser);

            if (requestResultUser.Succeeded)
            {
                var requestResultAuthentication = _iBAuthentications.Create(string.Empty, requestResultUser.Model); //ToDo: Implement Refresh Token
                requestResult.Add(requestResultAuthentication);

                if (requestResultAuthentication.Succeeded)
                    requestResult.Model = requestResultAuthentication.Model;
            }

            return Ok(requestResult);
        }
    }
}