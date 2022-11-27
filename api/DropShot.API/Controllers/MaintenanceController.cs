using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MaintenanceController : ControllerBase
{
    [HttpGet("ping")]
    [Authorize(Roles = "User")]
    public string Ping() => "pong";
    
    [HttpGet("sring")]
    [Authorize(Roles = "Admin")]
    public string Sring() => "srong";
}