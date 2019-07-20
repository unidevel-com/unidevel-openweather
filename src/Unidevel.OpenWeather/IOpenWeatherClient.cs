using System;
using System.Threading.Tasks;

namespace Unidevel.OpenWeather
{
    public interface IOpenWeatherClient
    {
        Task<CurrentWeather> GetCurrentWeatherAsync(
            string apiKey = null, 
            float longitude = float.NaN, float latitude = float.NaN, 
            string cityNameCountryCode = null, 
            int cityId = Int32.MinValue);

        Task<WeatherForecast> GetWeatherForecast5d3hAsync(
            string apiKey = null, 
            float longitude = float.NaN, float latitude = float.NaN, 
            string cityNameCountryCode = null, 
            int cityId = Int32.MinValue);
    }
}
