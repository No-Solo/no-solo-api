using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoSolo.Presentation.Controllers;

public class AdminResourceController : ControllerBase
{
    private const string AllowedRoles = "Admin";

    [Route("adminResource")]
    [HttpGet]
    [Authorize(Roles = AllowedRoles)]
    public IActionResult Get()
    {
        return Ok($"This resource is granted to the role of {AllowedRoles}");
    }
}