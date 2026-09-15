using Microsoft.AspNetCore.Mvc;
using RDiazClock.Core.Models;
using RDiazClock.Core.Services;

namespace RDiazClock.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClockController : ControllerBase
{
    private readonly IClockService _clockService;

    public ClockController(IClockService clockService)
    {
        _clockService = clockService;
    }

    [HttpGet("settings")]
    public IActionResult GetSettings()
    {
        return Ok(_clockService.GetSettings());
    }

    [HttpPost("settings")]
    public IActionResult UpdateSettings([FromBody] ClockSettings request)
    {
        _clockService.UpdateSettings(request.TickMessage, request.TockMessage, request.BongMessage);
        return Ok(_clockService.GetSettings());
    }
}