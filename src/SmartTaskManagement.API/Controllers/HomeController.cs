using Microsoft.AspNetCore.Mvc;

namespace SmartTaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Smart Task Management API is running!");
    }

    [HttpGet("health")]
    public IActionResult Health()
    {

        return Ok(new
        {
            Status = "Healthy"
        });
    }
}