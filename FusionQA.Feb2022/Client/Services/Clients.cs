using System.Reactive;
using FusionQA.Feb2022.Shared;
using RestEase;

namespace FusionQA.Feb2022.Client.Services;

[BasePath("counter")]
public interface ICounterClientDef
{
    [Post("increment")]
    Task Increment(CancellationToken cancellationToken = default);

    [Get("get")]
    Task<int> Get(CancellationToken cancellationToken = default);
}

[BasePath("weatherForecast")]
public interface IWeatherForecastClientDef
{
    [Get("getForecast")]
    Task<WeatherForecast[]> GetForecast(DateTime startDate, CancellationToken cancellationToken = default);
}

