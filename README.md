## Synopsis

Open Weather client library 

## Code Example

```csharp
IOpenWeatherClient openWeatherClient = new OpenWeatherClient();

var currentWeather = await openWeatherClient.GetCurrentWeather("apikeyapikeyapikeyapikey", 
	22.021255f, 51.500319f)

var weatherForecast = await openWeatherClient.GetWeatherForecast5d3h("apikeyapikeyapikeyapikey", 
	22.021255f, 51.500319f)
```

Complete interface is:

```csharp
public interface IOpenWeatherClient
{
	Task<CurrentWeather> GetCurrentWeather(string apiKey, float longitude, float latitude);
	Task<WeatherForecast> GetWeatherForecast5d3h(string apiKey, float longitude, float latitude);
}
```

## Motivation

Initially developed for home automation. Simple interface and full JSON object structure, exactly as in Open Weather API documentation.

## Installation

Use NuGet.

## API Reference

Look at code example should be enough.

## Tests

Describe and show how to run the tests with code examples.

## Contributors

Every contributor is welcome here. But keep it simple.

## License

I like MIT licence for my work, so this one will be used.
