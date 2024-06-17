using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Unidevel.OpenWeather
{
    public class OpenWeatherClient : IDisposable, IOpenWeatherClient
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly IHttpClientFactory _httpClientFactory;

        public OpenWeatherClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiKey = _configuration.GetSection("OpenWeather")?["ApiKey"];
        }

        public async Task<CurrentWeather> GetCurrentWeatherAsync(float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = int.MinValue, string apiKey = null)
        {
            var url = BuildCurrentWeatherUrl(apiKey ?? _apiKey, longitude, latitude, cityNameCountryCode, cityId);

            using var httpClient = _httpClientFactory.CreateClient();

            var currentWeather = await httpClient.GetFromJsonAsync<CurrentWeather>(url);

            return currentWeather;
        }

        public async Task<WeatherForecast> GetWeatherForecast5d3hAsync(float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = int.MinValue, string apiKey = null)
        {
            var url = BuildWeatherForecast5d3hUrl(apiKey ?? _apiKey, longitude, latitude, cityNameCountryCode, cityId);

            using var httpClient = _httpClientFactory.CreateClient();

            var weatherForecast = await httpClient.GetFromJsonAsync<WeatherForecast>(url);

            return weatherForecast;
        }

        private static readonly IFormatProvider AmericanNumberFormatProvider = System.Globalization.CultureInfo.GetCultureInfo("en-US").NumberFormat;

        private static string BuildCurrentWeatherUrl(string apiKey, float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = int.MinValue)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("Must not be null, empty or whitespace.", nameof(apiKey));
            return $"http://api.openweathermap.org/data/2.5/weather?{BuildLocationPart(longitude, latitude, cityNameCountryCode, cityId)}&units=metric&appid={apiKey}";
        }

        private static string BuildWeatherForecast5d3hUrl(string apiKey, float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = int.MinValue)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("Must not be null, empty or whitespace.", nameof(apiKey));
            return $"http://api.openweathermap.org/data/2.5/forecast?{BuildLocationPart(longitude, latitude, cityNameCountryCode, cityId)}&units=metric&appid={apiKey}";
        }

        private static string BuildLocationPart(float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = int.MinValue)
        {
            if ((!float.IsNaN(longitude)) || (!float.IsNaN(latitude)))
            {
                if (float.IsNaN(latitude)) throw new ArgumentException("Must be specified and not NaN when latitude provided.", nameof(longitude));
                if (float.IsNaN(longitude)) throw new ArgumentException("Must be specified and not NaN when longitude provided.", nameof(latitude));
                if (!string.IsNullOrWhiteSpace(cityNameCountryCode)) throw new ArgumentException("Must not be specified when coordinates provided.", nameof(cityNameCountryCode));
                if (cityId != int.MinValue) throw new ArgumentException("Must not be specified when coordinates provided.", nameof(cityId));

                return $"lat={latitude.ToString(AmericanNumberFormatProvider)}&lon={longitude.ToString(AmericanNumberFormatProvider)}";
            }
            else if (cityNameCountryCode != null)
            {
                if (cityId != int.MinValue) throw new ArgumentException("Must not be specified when coordinates provided.", nameof(cityId));

                return $"q={Uri.EscapeDataString(cityNameCountryCode)}";
            }
            else if (cityId != int.MinValue)
            {
                return $"id={cityId}";
            }
            else
                throw new ArgumentException("Location information has not been provided.");
        }

        public void Dispose()
        {
        }
    }
}
