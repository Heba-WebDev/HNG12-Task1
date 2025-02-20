using Microsoft.AspNetCore.Mvc;
namespace task1.Controllers;

[ApiController]
[Route("/integration.json")]
public class SettingsController : ControllerBase
{
    [HttpGet]
public IActionResult GetSettings()
{
    var filePath = "/home/heba/Desktop/task1/Controllers/settings.json";

    if (!System.IO.File.Exists(filePath))
    {
        return NotFound("Settings file not found");
    }
    
    var jsonContent = System.IO.File.ReadAllText(filePath);

    return Content(jsonContent, "application/json");
}
}
