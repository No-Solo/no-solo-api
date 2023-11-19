using Microsoft.AspNetCore.Mvc;
using NoSolo.Presentation.Errors;

namespace NoSolo.Presentation.Controllers;

[Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseApiController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}