using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace NoSolo.Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ExcludeFromCodeCoverage]
public class BaseApiController : ControllerBase
{
}