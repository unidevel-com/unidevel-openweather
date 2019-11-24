## Synopsis

Open Weather client library. Minimal dependencies, minimal interface, complete set of data and enumerations provided by Open Weather API.

## What's new?

### 0.9.5

- Fixed: WeatherForecastItem was missing Weather property (important!)

### 0.9.4

- IOpenWeatherClient is now IDisposable (implementation already was)
- ApiKey can be provided directly to constructor as a string, without using IConfiguration

## Code Example

Open Weather Api Key can be provided through IConfiguration (via dependency injection) or constructor parameter. Client will
look for it in OpenWeather:ApiKey property ('OpenWeather' section, 'ApiKey' string value). You can also
provide apiKey directly to called methods. If provided, it will be preferred over configured value.

### How to create instance

#### Using parameterless constructor
```csharp
using (IOpenWeatherClient openWeatherClient = new OpenWeatherClient())
{
	// your code here, but each call must include apiKey parameter otherwise you'll get WebException with code 401
}
```
#### Using constructor with API key

```csharp
using (IOpenWeatherClient openWeatherClient = new OpenWeatherClient(apiKey: "-- my api key --"))
{
	// your code here
}
```
#### Using constructor with IConfiguration
```csharp
var config = new ConfigurationBuilder()
	.AddInMemoryCollection(new KeyValuePair<string, string>[] // of course there are better providers, look at ConfigurationBuilder docs
	{ 
		new KeyValuePair<string, string>("OpenWeather:ApiKey", "-- my api key --") 
	})
    .Build();

using (IOpenWeatherClient client = new OpenWeatherClient(config))
{
	// your code here
}
```
### Query samples

#### Query by longitude and latitude
```csharp
using (IOpenWeatherClient openWeatherClient = new OpenWeatherClient(apiKey: "-- my api key --"))
{
	var currentWeather = await openWeatherClient.GetCurrentWeatherAsync(longitude: 22.021255f, latitude: 51.500319f);
	var weatherForecast = await openWeatherClient.GetWeatherForecast5d3hAsync(longitude: 22.021255f, latitude: 51.500319f);
}
```
#### Query by city name
```csharp
using (IOpenWeatherClient openWeatherClient = new OpenWeatherClient(apiKey: "-- my api key --"))
{
	var currentWeather = await openWeatherClient.GetCurrentWeatherAsync(cityNameCountryCode: "London, uk");
	var weatherForecast = await openWeatherClient.GetWeatherForecast5d3hAsync(cityNameCountryCode: "London, uk");
}
```
#### Query by cityId
```csharp

using (IOpenWeatherClient openWeatherClient = new OpenWeatherClient(apiKey: "-- my api key --"))
{
	var currentWeather = await openWeatherClient.GetCurrentWeatherAsync(cityId: 2172797);
	var weatherForecast = await openWeatherClient.GetWeatherForecast5d3hAsync(cityId: 2172797);
}
```
### Complete interface is:
```csharp
public interface IOpenWeatherClient: IDisposable
{
    Task<CurrentWeather> GetCurrentWeatherAsync(
        float longitude = float.NaN, float latitude = float.NaN, 
        string cityNameCountryCode = null, 
        int cityId = Int32.MinValue,
        string apiKey = null);

    Task<WeatherForecast> GetWeatherForecast5d3hAsync(
        float longitude = float.NaN, float latitude = float.NaN, 
        string cityNameCountryCode = null, 
        int cityId = Int32.MinValue,
        string apiKey = null);
}
```

## Motivation

Initially developed for home automation. Simple interface and full JSON object structure, exactly as in Open Weather API documentation.

## Installation

Use NuGet.

## API Reference

Look at code example should be enough. Api in this version has been changed to include Async suffix
in method names. Also IConfiguration is optionally accepted by constructor and allows to provide
apiKey by IConfiguration interface ("OpenWeather:ApiKey").

## Tests

Tests are included in this release. However, you must provide apiKey in Const.cs file (you can
obtain one free from Open Weather website).

## Contributors

Every contributor is welcome here. But keep it simple.

## License

I like MIT licence for my work, so this one will be used.
