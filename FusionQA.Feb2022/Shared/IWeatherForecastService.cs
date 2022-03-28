using Stl.Fusion;

namespace FusionQA.Feb2022.Shared;

public interface IWeatherForecastService
{
    [ComputeMethod]
    Task<WeatherForecast[]> GetForecast(DateTime startDate, CancellationToken cancellationToken = default);
}
