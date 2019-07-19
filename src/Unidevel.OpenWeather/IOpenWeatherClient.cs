using System;
using System.Threading.Tasks;

namespace Unidevel.OpenWeather
{
    public interface IOpenWeatherClient
    {
        Task<CurrentWeather> GetCurrentWeather(
            string apiKey, 
            float longitude = float.NaN, float latitude = float.NaN, 
            string cityNameCountryCode = null, 
            int cityId = Int32.MinValue);

        Task<WeatherForecast> GetWeatherForecast5d3h(
            string apiKey, 
            float longitude = float.NaN, float latitude = float.NaN, 
            string cityNameCountryCode = null, 
            int cityId = Int32.MinValue);
    }
}
