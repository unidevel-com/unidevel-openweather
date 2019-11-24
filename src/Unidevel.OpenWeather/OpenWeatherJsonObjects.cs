using Newtonsoft.Json;
using System;

namespace Unidevel.OpenWeather
{
    public enum WeatherCode : short
    {
        ThunderstormWithLighRain = 200,
        ThunderstormWithRain = 201,
        ThunderstormWithHeavyRain = 202,
        LightThunderstorm = 210,
        Thunderstorm = 211,
        HeavyThunderstorm = 212,
        RaggedThunderstorm = 221,
        ThunderstormWithLightDrizzle = 230,
        ThunderstormWithDrizzle = 231,
        ThunderstormWithHeavyDrizzle = 232,

        LightIntensityDrizzle = 300,
        Drizzle = 301,
        HeavyIntensityDrizzle = 302,
        LightIntensityDrizzleRain = 310,
        DrizzleRain = 311,
        HeavyIntensityDrizzleRain = 312,
        ShowerRainAndDrizzle = 313,
        HeavyShowerRainAndDrizzle = 314,
        ShowerDrizzle = 321,

        LightRain = 500,
        ModerateRain = 501,
        HeavyIntensityRain = 502,
        VeryHeavyRain = 503,
        ExtremeRain = 504,
        FreezingRain = 511,
        LightIntensityShowerRain = 520,
        ShowerRain = 521,
        HeavyIntensityShowerRain = 522,
        RaggedShowerRain = 531,

        LightSnow = 600,
        Snow = 601,
        HeavySnow = 602,
        Sleet = 611,
        LightShowerSleet = 612,
        ShowerSleet = 613,
        LightRainAndSnow = 615,
        RainAndSnow = 616,
        LightShowerSnow = 620,
        ShowerSnow = 621,
        HeavyShowerSnow = 622,

        Mist = 701,
        Smoke = 711,
        Haze = 721,
        SandDustWhirls = 731,
        Fog = 741,
        Sand = 751,
        Dust = 761,
        VolcanicAsh = 762,
        Squalls = 771,
        Tornado = 781,

        Clear = 800,
        FewClouds = 801,
        ScatteredClouds = 802,
        BrokenClouds = 803,
        OvercastClouds = 804
    }

    public class CurrentWeather
    {
        [JsonProperty("coord")] public Coordinates Coordinates { get; set; }
        [JsonProperty("weather")] public Weather[] Weather { get; set; }
        [JsonProperty("base")] public string Base { get; set; }
        [JsonProperty("main")] public Main Main { get; set; }
        [JsonProperty("wind")] public Wind Wind { get; set; }
        [JsonProperty("clouds")] public Clouds Clouds { get; set; }
        [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)] public Rain Rain { get; set; }
        [JsonProperty("snow", NullValueHandling = NullValueHandling.Ignore)] public Snow Snow { get; set; }
        [JsonProperty("dt"), JsonConverter(typeof(Newtonsoft.Json.Converters.UnixDateTimeConverter))] public DateTime DateTimeUtc { get; set; }
        [JsonProperty("sys")] public Sys Sys { get; set; }
        [JsonProperty("timezone")] public int TimezoneShiftSeconds { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("cod")] public int Cod { get; set; }
    }

    public class WeatherForecast
    {
        [JsonProperty("city")] public City City { get; set; }
        [JsonProperty("cod")] public int Cod { get; set; }
        [JsonProperty("message")] public float Message { get; set; }
        [JsonProperty("cnt")] public int Count { get; set; }
        [JsonProperty("list")] public WeatherForecastItem[] List { get; set; }
    }

    public class WeatherForecastItem
    {
        [JsonProperty("dt"), JsonConverter(typeof(Newtonsoft.Json.Converters.UnixDateTimeConverter))] public DateTime DateTimeUtc { get; set; }
        [JsonProperty("main")] public Main Main { get; set; }
        [JsonProperty("wind")] public Wind Wind { get; set; }
        [JsonProperty("clouds")] public Clouds Clouds { get; set; }
        [JsonProperty("weather")] public Weather[] Weather { get; set; }
        [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)] public Rain Rain { get; set; }
        [JsonProperty("snow", NullValueHandling = NullValueHandling.Ignore)] public Snow Snow { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("lon")] public float LongitudeDeg { get; set; }
        [JsonProperty("lat")] public float LatitudeDeg { get; set; }
    }

    public class City
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("coord")] public Coordinates Coordinates { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
        [JsonProperty("timezone")] public int TimezoneShiftSeconds { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")] public WeatherCode Id { get; set; }
        [JsonProperty("main")] public string Main { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("icon")] public string Icon { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")] public float TempC { get; set; }
        [JsonProperty("pressure")] public float PressurehPa { get; set; }
        [JsonProperty("humidity")] public float HumidityPerc { get; set; }
        [JsonProperty("temp_min")] public float TempMinC { get; set; }
        [JsonProperty("temp_max")] public float TempMaxC { get; set; }
        [JsonProperty("sea_level", NullValueHandling = NullValueHandling.Ignore)] public float? SeaLevelPressurehPa { get; set; }
        [JsonProperty("grnd_level", NullValueHandling = NullValueHandling.Ignore)] public float? GroundLevelPressurehPa { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")] public float SpeedKmph { get; set; }
        [JsonProperty("deg")] public float DirectionDeg { get; set; }
    }

    public class Rain
    {
        [JsonProperty("1h")] public float OneHourMm { get; set; }
        [JsonProperty("3h")] public float ThreeHourMm { get; set; }
    }

    public class Snow
    {
        [JsonProperty("1h")] public float OneHourMm { get; set; }
        [JsonProperty("3h")] public float ThreeHourMm { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")] public float Percentage { get; set; }
    }

    public class Sys
    {
        [JsonProperty("type")] public int Type { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("message")] public float Message { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
        [JsonProperty("sunrise"), JsonConverter(typeof(Newtonsoft.Json.Converters.UnixDateTimeConverter))] public DateTime SunriseUtc { get; set; }
        [JsonProperty("sunset"), JsonConverter(typeof(Newtonsoft.Json.Converters.UnixDateTimeConverter))] public DateTime SunsetUtc { get; set; }
    }
}
