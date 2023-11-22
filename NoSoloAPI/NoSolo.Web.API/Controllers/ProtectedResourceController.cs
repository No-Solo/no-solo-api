using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoSolo.Web.API.Controllers;

public class ProtectedResourceController : BaseApiController
{
    [Route("protectedInfo")]
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("You can see this message means you are a valid user.");
    }
}