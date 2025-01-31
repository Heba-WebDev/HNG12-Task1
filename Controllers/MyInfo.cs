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
            email = "omarzaynab48@gmail.com",
            current_datetime = DateTime.UtcNow.ToString("o"),
            github_url = "https://github.com/Heba-WebDev/HNG12-Task1"
        };

        return Ok(response);
    }
}
