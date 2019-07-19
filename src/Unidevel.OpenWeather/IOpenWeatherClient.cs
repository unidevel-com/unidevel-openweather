using System.Threading.Tasks;

namespace Unidevel.OpenWeather
{
    public interface IOpenWeatherClient
    {
        Task<CurrentWeather> GetCurrentWeather(string apiKey, float longitude, float latitude);
        Task<WeatherForecast> GetWeatherForecast5d3h(string apiKey, float longitude, float latitude);
    }
}
