using Microsoft.AspNetCore.Mvc;
using WeatherService.Api.Client;

namespace WeatherService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase
{
    private WeatherClient weatherClient;

    public StatusController(WeatherClient weatherClient)
    {
        this.weatherClient = weatherClient;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetStatusAsync()
    {
        var result = await weatherClient.GetCityStatus("Busan, South Korea");
        return Ok(result);
    }
}