using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Unidevel.OpenWeather.Tests
{
    public class OpenWeatherClient_GetCurrentWeatherAsync_Tests
    {
        [Fact]
        public async Task ByLonLat_UseIConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("OpenWeather:ApiKey", Const.OpenWeatherApiKey) })
                .Build();

            using (IOpenWeatherClient client = new OpenWeatherClient(config))
            {
                CurrentWeather currentWeather = await client.GetCurrentWeatherAsync(
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude);

                Assert.NotNull(currentWeather);
            }
        }

        [Fact]
        public async Task ByLonLat_UseConstructorAppId()
        {
            using (IOpenWeatherClient client = new OpenWeatherClient(apiKey: Const.OpenWeatherApiKey))
            {
                CurrentWeather currentWeather = await client.GetCurrentWeatherAsync(
                longitude: Const.SampleLongitude,
                latitude: Const.SampleLatitude);

                Assert.NotNull(currentWeather);
            }
        }

        [Fact]
        public async Task Err_ConstructorAppIdOverridesIConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("OpenWeather:ApiKey", Const.OpenWeatherApiKey) })
                .Build();

            await Assert.ThrowsAsync<System.Net.WebException>(async () =>
            {
                using (IOpenWeatherClient client = new OpenWeatherClient(config, apiKey: "this-key-fails-for-sure"))
                {
                    var currentWeather = await client.GetCurrentWeatherAsync(
                        longitude: Const.SampleLongitude,
                        latitude: Const.SampleLatitude);
                }
            });
        }

        [Fact]
        public async Task ByLonLat()
        {
            using (IOpenWeatherClient client = new OpenWeatherClient())
            {
                CurrentWeather currentWeather = await client.GetCurrentWeatherAsync(
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude,
                    apiKey: Const.OpenWeatherApiKey);

                Assert.NotNull(currentWeather);
            }
        }

        [Fact]
        public async Task ByCityName()
        {
            using (IOpenWeatherClient client = new OpenWeatherClient())
            {
                CurrentWeather currentWeather = await client.GetCurrentWeatherAsync(
                    cityNameCountryCode: Const.SampleCityNameCountryCode,
                    apiKey: Const.OpenWeatherApiKey);

                Assert.NotNull(currentWeather);
            }
        }

        [Fact]
        public async Task ByCityId()
        {
            using (IOpenWeatherClient client = new OpenWeatherClient())
            {
                var currentWeather = await client.GetCurrentWeatherAsync(
                    cityId: Const.SampleCityId,
                    apiKey: Const.OpenWeatherApiKey);

                Assert.NotNull(currentWeather);
            }
        }

        [Fact]
        public async Task Err_MissingLat()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using (IOpenWeatherClient client = new OpenWeatherClient())
                {
                    var currentWeather = await client.GetCurrentWeatherAsync(
                        longitude: Const.SampleLongitude,
                        apiKey: Const.OpenWeatherApiKey);
                }
            });
        }

        [Fact]
        public async Task Err_MissingLon()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using (IOpenWeatherClient client = new OpenWeatherClient())
                {
                    var currentWeather = await client.GetCurrentWeatherAsync(
                        latitude: Const.SampleLatitude,
                        apiKey: Const.OpenWeatherApiKey);
                }
            });
        }

        [Fact]
        public async Task Err_LonLatCityName()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using (IOpenWeatherClient client = new OpenWeatherClient())
                {
                    var currentWeather = await client.GetCurrentWeatherAsync(
                        longitude: Const.SampleLongitude,
                        latitude: Const.SampleLatitude,
                        cityNameCountryCode: Const.SampleCityNameCountryCode,
                        apiKey: Const.OpenWeatherApiKey);
                }
            });
        }

        [Fact]
        public async Task Err_LonLatCityId()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using (IOpenWeatherClient client = new OpenWeatherClient())
                {
                    var currentWeather = await client.GetCurrentWeatherAsync(
                        longitude: Const.SampleLongitude,
                        latitude: Const.SampleLatitude,
                        cityId: Const.SampleCityId,
                        apiKey: Const.OpenWeatherApiKey);
                }
            });
        }

        [Fact]
        public async Task Err_CityNameCityId()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using (IOpenWeatherClient client = new OpenWeatherClient())
                {
                    var currentWeather = await client.GetCurrentWeatherAsync(
                        cityNameCountryCode: Const.SampleCityNameCountryCode,
                        cityId: Const.SampleCityId,
                        apiKey: Const.OpenWeatherApiKey);
                }
            });
        }
    }
}
