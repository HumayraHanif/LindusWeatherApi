using System.Text.Json.Serialization;

namespace WeatherService.Api.Client
{
    public class CurrentWeatherDto

    {
        public LocationDto Location { get; set; }
        public CurrentDto Current { get; set; }
    }
    public class CurrentDto
    {
        [JsonPropertyName("temp_c")]
        public double Temperature { get; set; }
    }

}
