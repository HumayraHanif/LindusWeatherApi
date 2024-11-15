using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherService.Api.Controllers.Response;
using WeatherService.Api.Utils;

namespace WeatherService.Api.Client
{
    public class WeatherClient
    {

        private HttpClient client;
        private readonly WeatherServiceSettings settings;
        private static JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public WeatherClient(IOptions<WeatherServiceSettings> _settings, HttpClient _client)
        {
            settings = _settings.Value;
            client = _client;
        }

        private string BaseURL = "http://api.weatherapi.com/v1";
        private string CurrentRequestUrl(string key, string city) => $"{BaseURL}/current.json?key={key}&q={city}";
        private string AstronomyRequestUrl(string key, string city) => $"{BaseURL}/astronomy.json?key={key}&q={city}";

        public virtual async Task<CityStatusResponse> GetCityStatus(string city)
        {
            try
            {
                var currentDto = await GetCurrentWeather(city);
                var astronomyDto = await GetAstronomyWeather(city);
                var result = new CityStatusResponse()
                {
                    City = currentDto.Location.City,
                    Region = currentDto.Location.Region,
                    Country = currentDto.Location.Country,
                    LocalTime = currentDto.Location.LocalTime,
                    Temperature = currentDto.Current.Temperature,
                    Sunrise = astronomyDto.Astronomy.Astro.Sunrise,
                    Sunset = astronomyDto.Astronomy.Astro.Sunset

                };
                return result;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        private async Task<CurrentWeatherDto?> GetCurrentWeather(string city)
        {
            var url = CurrentRequestUrl(settings.WeatherApiKey, city);
            var responseBody = await SendRequest(url);
            var currentDto = JsonSerializer.Deserialize<CurrentWeatherDto>(responseBody, options);
            return currentDto;
        }

        private async Task<AstronomyWeatherDto?> GetAstronomyWeather(string city)
        {
            var url = AstronomyRequestUrl(settings.WeatherApiKey, city);
            var responseBody = await SendRequest(url);
            var astronomyDto = JsonSerializer.Deserialize<AstronomyWeatherDto>(responseBody, options);
            return astronomyDto;
        }

        private async Task<string> SendRequest(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
