using Stl.Fusion;

namespace FusionQA.Feb2020.Shared;

public interface IWeatherForecastService
{
    [ComputeMethod]
    Task<WeatherForecast[]> GetForecast(DateTime startDate, CancellationToken cancellationToken = default);
}
