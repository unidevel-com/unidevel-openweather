using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Unidevel.OpenWeather.Tests
{
    public class OpenWeatherClient_GetWeatherForecast5d3h_Tests
    {
        private readonly IConfigurationRoot _configuration;

        public OpenWeatherClient_GetWeatherForecast5d3h_Tests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[] { new("OpenWeather:ApiKey", Const.OpenWeatherApiKey) })
                .Build();
        }

        [Fact]
        public async Task ByLonLat_UseIConfiguration()
        {
            using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
            var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                longitude: Const.SampleLongitude,
                latitude: Const.SampleLatitude);

            Assert.NotNull(weatherForecast);
        }

        [Fact]
        public async Task ByLonLat_UseConstructorAppId()
        {
            using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
            var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                longitude: Const.SampleLongitude,
                latitude: Const.SampleLatitude);

            Assert.NotNull(weatherForecast);
        }

        [Fact]
        public async Task Err_ConstructorAppIdOverridesIConfiguration()
        {
            await Assert.ThrowsAsync<System.Net.WebException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude,
                    apiKey: "this-key-fails-for-sure");
            });
        }

        [Fact]
        public async Task ByLonLat()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                    longitude: Const.SampleLongitude,
                    latitude: Const.SampleLatitude);
            });
        }

        [Fact]
        public async Task ByCityName()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                    cityNameCountryCode: Const.SampleCityNameCountryCode);
            });
        }

        [Fact]
        public async Task ByCityId()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                    cityId: Const.SampleCityId);
            });
        }

        [Fact]
        public async Task Err_MissingLat()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                    longitude: Const.SampleLongitude);
            });
        }

        [Fact]
        public async Task Err_MissingLon()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                    latitude: Const.SampleLatitude);
            });
        }

        [Fact]
        public async Task Err_LonLatCityName()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
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
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
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
                using var client = new OpenWeatherClient(_configuration, CreateHttpClientFactory());
                var weatherForecast = await client.GetWeatherForecast5d3hAsync(
                    cityNameCountryCode: Const.SampleCityNameCountryCode,
                    cityId: Const.SampleCityId);
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
