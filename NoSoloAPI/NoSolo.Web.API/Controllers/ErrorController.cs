using Microsoft.AspNetCore.Mvc;
using NoSolo.Web.API.Errors;

namespace NoSolo.Web.API.Controllers;

[Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseApiController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}