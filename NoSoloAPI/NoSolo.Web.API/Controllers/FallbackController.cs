using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace NoSolo.Web.API.Controllers;

[ExcludeFromCodeCoverage]
public class FallbackController : Controller
{
    public IActionResult Index()
    {
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
    }
}