﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace BaseApi
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected virtual string Username => User.Identities.FirstOrDefault().Name;
        protected virtual int UserId => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        protected virtual IActionResult Created<T>(T model)
        {
            return new ObjectResult(model)
            {
                StatusCode = (int)HttpStatusCode.Created
            };
        }
    }
}
