namespace WeatherService.Api.Client
{
    public class AstronomyWeatherDto
    {
        public LocationDto Location { get; set; }
        public Astronomy Astronomy { get; set; }
    }

    public class Astronomy
    {
        public Astro Astro { get; set; }
    }
    public class Astro
    {
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }
}
