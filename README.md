## Synopsis

Open Weather client library. Minimal dependencies, minimal interface, complete set of data and enumerations provided by Open Weather API.

## Code Example

### Query samples

#### Query by longitude and latitude
```csharp
IOpenWeatherClient openWeatherClient = new OpenWeatherClient();

var currentWeather = await openWeatherClient.GetCurrentWeather("apikeyapikeyapikeyapikey", 
	longitude: 22.021255f, latitude: 51.500319f)

var weatherForecast = await openWeatherClient.GetWeatherForecast5d3h("apikeyapikeyapikeyapikey", 
	longitude: 22.021255f, latitude: 51.500319f)
```
#### Query by city name
```csharp
IOpenWeatherClient openWeatherClient = new OpenWeatherClient();

var currentWeather = await openWeatherClient.GetCurrentWeather("apikeyapikeyapikeyapikey", 
	cityNameCountryCode: "London, uk")

var weatherForecast = await openWeatherClient.GetWeatherForecast5d3h("apikeyapikeyapikeyapikey", 
	cityNameCountryCode: "London, uk")
```
#### Query by cityId
```csharp
IOpenWeatherClient openWeatherClient = new OpenWeatherClient();

var currentWeather = await openWeatherClient.GetCurrentWeather("apikeyapikeyapikeyapikey", 
	cityId: 2172797)

var weatherForecast = await openWeatherClient.GetWeatherForecast5d3h("apikeyapikeyapikeyapikey", 
	cityId: 2172797)
```
### Complete interface is:

```csharp
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
```

## Motivation

Initially developed for home automation. Simple interface and full JSON object structure, exactly as in Open Weather API documentation.

## Installation

Use NuGet.

## API Reference

Look at code example should be enough.

## Tests

Tests are included in this release. However, you must provide apiKey in Const.cs file (you can
obtain one free from Open Weather website).

## Contributors

Every contributor is welcome here. But keep it simple.

## License

I like MIT licence for my work, so this one will be used.
