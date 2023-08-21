﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProtectedResourceController : ControllerBase
{
    [Route("protectedInfo")]
    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public IActionResult Get()
    {
        return Ok("You can see this message means you are a valid user.");
    }
}