using FusionQA.Feb2022.Shared;
using Microsoft.AspNetCore.Mvc;
using Stl.Fusion.Server;

namespace FusionQA.Feb2022.Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController, JsonifyErrors]
public class WeatherForecastController : ControllerBase, IWeatherForecastService
{
    private readonly IWeatherForecastService _forecast;

    public WeatherForecastController(IWeatherForecastService forecast) => _forecast = forecast;

    [HttpGet, Publish]
    public Task<WeatherForecast[]> GetForecast(DateTime startDate,
        CancellationToken cancellationToken = default)
        => _forecast.GetForecast(startDate, cancellationToken);
}
