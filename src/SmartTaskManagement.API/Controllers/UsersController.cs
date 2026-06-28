using Microsoft.AspNetCore.Mvc;

namespace SmartTaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = new[]
        {
            new
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com"
            },
            new
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane@example.com"
            }
        };

        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        return Ok(new
        {
            Id = id,
            Name = "John Doe",
            Email = "john@example.com"
        });
    }

    [HttpPost]
    public IActionResult CreateUser()
    {
        return Ok("User Created");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id)
    {
        return Ok($"User {id} Updated");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        return Ok($"User {id} Deleted");
    }
}