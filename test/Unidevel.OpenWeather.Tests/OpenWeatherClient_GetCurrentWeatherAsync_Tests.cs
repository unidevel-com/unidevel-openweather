using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Unidevel.OpenWeather.Tests
{
    public class OpenWeatherClient_GetCurrentWeatherAsync_Tests
    {
        private readonly IConfigurationRoot _configuration;

        public OpenWeatherClient_GetCurrentWeatherAsync_Tests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[] { new("OpenWeather:ApiKey", Const.OpenWeatherApiKey) })
                .Build();
        }

        [Fact]
        public async Task ByLonLat_UseIConfiguration()
        {
            using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
            var currentWeather = await client.GetCurrentWeatherAsync(
                longitude: Const.SampleLongitude,
                latitude: Const.SampleLatitude);

            Assert.NotNull(currentWeather);
        }

        [Fact]
        public async Task ByLonLat_UseConstructorAppId()
        {
            using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
            var currentWeather = await client.GetCurrentWeatherAsync(
                longitude: Const.SampleLongitude,
                latitude: Const.SampleLatitude,
                apiKey: Const.OpenWeatherApiKey);

            Assert.NotNull(currentWeather);
        }

        [Fact]
        public async Task Err_ConstructorAppIdOverridesIConfiguration()
        {
            await Assert.ThrowsAsync<System.Net.WebException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var currentWeather = await client.GetCurrentWeatherAsync(
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude,
                    apiKey: "this-key-fails-for-sure");
            });
        }

        [Fact]
        public async Task ByLonLat()
        {
            using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
            var currentWeather = await client.GetCurrentWeatherAsync(
                longitude: Const.SampleLongitude,
                latitude: Const.SampleLatitude,
                apiKey: Const.OpenWeatherApiKey);

            Assert.NotNull(currentWeather);
        }

        [Fact]
        public async Task ByCityName()
        {
            using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
            var currentWeather = await client.GetCurrentWeatherAsync(
                cityNameCountryCode: Const.SampleCityNameCountryCode,
                apiKey: Const.OpenWeatherApiKey);

            Assert.NotNull(currentWeather);
        }

        [Fact]
        public async Task ByCityId()
        {
            using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
            var currentWeather = await client.GetCurrentWeatherAsync(
                cityId: Const.SampleCityId,
                apiKey: Const.OpenWeatherApiKey);

            Assert.NotNull(currentWeather);
        }

        [Fact]
        public async Task Err_MissingLat()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var currentWeather = await client.GetCurrentWeatherAsync(
                    longitude: Const.SampleLongitude,
                    apiKey: Const.OpenWeatherApiKey);
            });
        }

        [Fact]
        public async Task Err_MissingLon()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var currentWeather = await client.GetCurrentWeatherAsync(
                    latitude: Const.SampleLatitude,
                    apiKey: Const.OpenWeatherApiKey);
            });
        }

        [Fact]
        public async Task Err_LonLatCityName()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var currentWeather = await client.GetCurrentWeatherAsync(
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude,
                    cityNameCountryCode: Const.SampleCityNameCountryCode,
                    apiKey: Const.OpenWeatherApiKey);
            });
        }

        [Fact]
        public async Task Err_LonLatCityId()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var currentWeather = await client.GetCurrentWeatherAsync(
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude,
                    cityId: Const.SampleCityId,
                    apiKey: Const.OpenWeatherApiKey);
            });
        }

        [Fact]
        public async Task Err_CityNameCityId()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var currentWeather = await client.GetCurrentWeatherAsync(
                    cityNameCountryCode: Const.SampleCityNameCountryCode,
                    cityId: Const.SampleCityId,
                    apiKey: Const.OpenWeatherApiKey);
            });
        }

        private static IHttpClientFactory CreateHttpClientFactory()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
    }
}
