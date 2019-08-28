using System;
using System.Threading.Tasks;

namespace Unidevel.OpenWeather
{
    public interface IOpenWeatherClient: IDisposable
    {
        Task<CurrentWeather> GetCurrentWeatherAsync(
            float longitude = float.NaN,
            float latitude = float.NaN, string cityNameCountryCode = null,
            int cityId = Int32.MinValue,
            string apiKey = null);

        Task<WeatherForecast> GetWeatherForecast5d3hAsync(
            float longitude = float.NaN,
            float latitude = float.NaN, string cityNameCountryCode = null,
            int cityId = Int32.MinValue,
            string apiKey = null);
    }
}
