using System;
using System.Threading.Tasks;
using Xunit;

namespace Unidevel.OpenWeather.Tests
{
    public class OpenWeatherClient_GetCurrentWeather_Tests
    {
        [Fact]
        public async Task ByLonLat()
        {
            IOpenWeatherClient client = new OpenWeatherClient();

            CurrentWeather currentWeather = await client.GetCurrentWeather(
                apiKey: Const.OpenWeatherApiKey, 
                longitude: Const.SampleLongitude, 
                latitude: Const.SampleLatitude);

            Assert.NotNull(currentWeather);
        }

        [Fact]
        public async Task ByCityName()
        {
            IOpenWeatherClient client = new OpenWeatherClient();

            CurrentWeather currentWeather = await client.GetCurrentWeather(
                apiKey: Const.OpenWeatherApiKey,
                cityNameCountryCode: Const.SampleCityNameCountryCode);

            Assert.NotNull(currentWeather);
        }

        [Fact]
        public async Task ByCityId()
        {
            IOpenWeatherClient client = new OpenWeatherClient();

            var currentWeather = await client.GetCurrentWeather(
                apiKey: Const.OpenWeatherApiKey,
                cityId: Const.SampleCityId);

            Assert.NotNull(currentWeather);
        }

        [Fact]
        public async Task Err_MissingLat()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                IOpenWeatherClient client = new OpenWeatherClient();

                var currentWeather = await client.GetCurrentWeather(
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

                var currentWeather = await client.GetCurrentWeather(
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

                var currentWeather = await client.GetCurrentWeather(
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

                var currentWeather = await client.GetCurrentWeather(
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

                var currentWeather = await client.GetCurrentWeather(
                    apiKey: Const.OpenWeatherApiKey,
                    cityNameCountryCode: Const.SampleCityNameCountryCode,
                    cityId: Const.SampleCityId);
            });
        }
    }
}
