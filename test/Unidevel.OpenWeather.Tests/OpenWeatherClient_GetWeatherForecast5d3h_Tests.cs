using System;
using System.Threading.Tasks;
using Xunit;

namespace Unidevel.OpenWeather.Tests
{
    public class OpenWeatherClient_GetWeatherForecast5d3h_Tests
    {
        [Fact]
        public async Task ByLonLat()
        {
            IOpenWeatherClient client = new OpenWeatherClient();

            WeatherForecast weatherForecast = await client.GetWeatherForecast5d3h(
                apiKey: Const.OpenWeatherApiKey, 
                longitude: Const.SampleLongitude, 
                latitude: Const.SampleLatitude);

            Assert.NotNull(weatherForecast);
        }

        [Fact]
        public async Task ByCityName()
        {
            IOpenWeatherClient client = new OpenWeatherClient();

            WeatherForecast weatherForecast = await client.GetWeatherForecast5d3h(
                apiKey: Const.OpenWeatherApiKey,
                cityNameCountryCode: Const.SampleCityNameCountryCode);

            Assert.NotNull(weatherForecast);
        }

        [Fact]
        public async Task ByCityId()
        {
            IOpenWeatherClient client = new OpenWeatherClient();

            var weatherForecast = await client.GetWeatherForecast5d3h(
                apiKey: Const.OpenWeatherApiKey,
                cityId: Const.SampleCityId);

            Assert.NotNull(weatherForecast);
        }

        [Fact]
        public async Task Err_MissingLat()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                IOpenWeatherClient client = new OpenWeatherClient();

                var weatherForecast = await client.GetWeatherForecast5d3h(
                    apiKey: Const.OpenWeatherApiKey,
                    longitude: Const.SampleLongitude);
            });
        }

        [Fact]
        public async Task Err_MissingLon()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                IOpenWeatherClient client = new OpenWeatherClient();

                var weatherForecast = await client.GetWeatherForecast5d3h(
                    apiKey: Const.OpenWeatherApiKey,
                    latitude: Const.SampleLatitude);
            });
        }

        [Fact]
        public async Task Err_LonLatCityName()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                IOpenWeatherClient client = new OpenWeatherClient();

                var weatherForecast = await client.GetWeatherForecast5d3h(
                    apiKey: Const.OpenWeatherApiKey,
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude,
                    cityNameCountryCode: Const.SampleCityNameCountryCode);
            });
        }

        [Fact]
        public async Task Err_LonLatCityId()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                IOpenWeatherClient client = new OpenWeatherClient();

                var weatherForecast = await client.GetWeatherForecast5d3h(
                    apiKey: Const.OpenWeatherApiKey,
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude,
                    cityId: Const.SampleCityId);
            });
        }

        [Fact]
        public async Task Err_CityNameCityId()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                IOpenWeatherClient client = new OpenWeatherClient();

                var weatherForecast = await client.GetWeatherForecast5d3h(
                    apiKey: Const.OpenWeatherApiKey,
                    cityNameCountryCode: Const.SampleCityNameCountryCode,
                    cityId: Const.SampleCityId);
            });
        }
    }
}
