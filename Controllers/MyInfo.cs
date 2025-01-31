using Microsoft.AspNetCore.Mvc;

namespace task1.Controllers;

[ApiController]
[Route("")]
public class MyInfoController : ControllerBase
{
    [HttpGet]
    public IActionResult GetMyInfo()
    {
        var response = new
        {
            email = "your-email@example.com",
            current_datetime = DateTime.UtcNow.ToString("o"),
            github_url = "https://github.com/yourusername/your-repo"
        };

        return Ok(response);
    }
}
