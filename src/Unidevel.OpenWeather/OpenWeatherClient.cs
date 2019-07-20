using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Unidevel.OpenWeather
{
    public class OpenWeatherClient : IDisposable, IOpenWeatherClient
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public OpenWeatherClient(IConfiguration configuration = null)
        {
            _configuration = configuration;
            _apiKey = configuration?.GetSection("OpenWeather")?["ApiKey"];
        }

        public async Task<CurrentWeather> GetCurrentWeatherAsync(float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = Int32.MinValue, string apiKey = null)
        {
            var url = buildCurrentWeatherUrl(apiKey ?? _apiKey, longitude, latitude, cityNameCountryCode, cityId);

            string currentWeatherJson;
            using (var webClient = new WebClient()) currentWeatherJson = await webClient.DownloadStringTaskAsync(url);

            var currentWeather = Newtonsoft.Json.JsonConvert.DeserializeObject<CurrentWeather>(currentWeatherJson);

            return currentWeather;
        }

        public async Task<WeatherForecast> GetWeatherForecast5d3hAsync(float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = Int32.MinValue, string apiKey = null)
        {
            var url = buildWeatherForecast5d3hUrl(apiKey ?? _apiKey, longitude, latitude, cityNameCountryCode, cityId);

            string weatherForecastJson;
            using (var webClient = new WebClient()) weatherForecastJson = await webClient.DownloadStringTaskAsync(url);
            var weatherForecast = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherForecast>(weatherForecastJson);

            return weatherForecast;
        }

        private static readonly IFormatProvider americanNumberFormatProvider = System.Globalization.CultureInfo.GetCultureInfo("en-US").NumberFormat;

        private string buildCurrentWeatherUrl(string apiKey, float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = Int32.MinValue)
        {
            if (String.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("Must not be null, empty or whitespace.", nameof(apiKey));
            return String.Format("http://api.openweathermap.org/data/2.5/weather?{0}&units=metric&appid={1}", buildLocationPart(longitude, latitude, cityNameCountryCode, cityId), apiKey);
        }

        private string buildWeatherForecast5d3hUrl(string apiKey, float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = Int32.MinValue)
        {
            if (String.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("Must not be null, empty or whitespace.", nameof(apiKey));
            return String.Format("http://api.openweathermap.org/data/2.5/forecast?{0}&units=metric&appid={1}", buildLocationPart(longitude, latitude, cityNameCountryCode, cityId), apiKey);
        }

        private string buildLocationPart(float longitude = float.NaN, float latitude = float.NaN, string cityNameCountryCode = null, int cityId = Int32.MinValue)
        {
            if ((!float.IsNaN(longitude)) || (!float.IsNaN(latitude)))
            {
                if (float.IsNaN(latitude)) throw new ArgumentException("Must be specified and not NaN when latitude provided.", nameof(longitude));
                if (float.IsNaN(longitude)) throw new ArgumentException("Must be specified and not NaN when longitude provided.", nameof(latitude));
                if (!String.IsNullOrWhiteSpace(cityNameCountryCode)) throw new ArgumentException("Must not be specified when coordinates provided.", nameof(cityNameCountryCode));
                if (cityId != Int32.MinValue) throw new ArgumentException("Must not be specified when coordinates provided.", nameof(cityId));

                return String.Format(americanNumberFormatProvider, "lat={0}&lon={1}", latitude, longitude);
            }
            else if (cityNameCountryCode != null)
            {
                if (cityId != Int32.MinValue) throw new ArgumentException("Must not be specified when coordinates provided.", nameof(cityId));

                return String.Format("q={0}", Uri.EscapeUriString(cityNameCountryCode));
            }
            else if (cityId != Int32.MinValue)
            {
                return String.Format("id={0}", cityId);
            }
            else
                throw new ArgumentException("Location information has not been provided.");
        }

        public void Dispose()
        {
        }
    }
}
