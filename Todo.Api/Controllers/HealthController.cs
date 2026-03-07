using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Controllers;

[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet("/health")]
    public IActionResult Health() => Ok(new { status = "ok" });

    [HttpGet("/ready")]
    public IActionResult Ready() => Ok(new { status = "ready" });
}
