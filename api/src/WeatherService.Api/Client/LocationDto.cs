using System.Text.Json.Serialization;

namespace WeatherService.Api.Client
{
    public class LocationDto
    {
        [JsonPropertyName("name")]
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        [JsonPropertyName("localtime")]
        public string LocalTime { get; set; }

    }
}
