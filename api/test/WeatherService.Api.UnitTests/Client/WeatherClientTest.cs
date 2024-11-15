using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using WeatherService.Api.Client;
using WeatherService.Api.Utils;

namespace WeatherService.Api.UnitTests.Client
{
    public class WeatherClientTest
    {
        private string _currentUrl(string key, string city) => $"http://api.weatherapi.com/v1/current.json?key={key}&q={city}";
        private string _astronomyUrl(string key, string city) => $"http://api.weatherapi.com/v1/astronomy.json?key={key}&q={city}";
        private string _currentLondonSuccess =  @"{""location"":{""name"":""London"",""region"":""City of London, Greater London"",""country"":""United Kingdom"",""lat"":51.5171,""lon"":-0.1062,""tz_id"":""Europe/London"",""localtime_epoch"":1730402972,""localtime"":""2024-10-31 19:29""},""current"":{""last_updated_epoch"":1730402100,""last_updated"":""2024-10-31 19:15"",""temp_c"":12.1,""temp_f"":53.8,""is_day"":0,""condition"":{""text"":""Partly cloudy"",""icon"":""//cdn.weatherapi.com/weather/64x64/night/116.png"",""code"":1003},""wind_mph"":2.2,""wind_kph"":3.6,""wind_degree"":277,""wind_dir"":""W"",""pressure_mb"":1025,""pressure_in"":30.27,""precip_mm"":0,""precip_in"":0,""humidity"":82,""cloud"":75,""feelslike_c"":12.6,""feelslike_f"":54.6,""windchill_c"":13.4,""windchill_f"":56.2,""heatindex_c"":13.4,""heatindex_f"":56.2,""dewpoint_c"":9.1,""dewpoint_f"":48.3,""vis_km"":10,""vis_miles"":6,""uv"":1,""gust_mph"":2.5,""gust_kph"":4}}";
        private string _astroLondonError = @"{""location"":{""name"":""London"",""region"":""City of London, Greater London"",""country"":""United Kingdom"",""lat"":51.5171,""lon"":-0.1062,""tz_id"":""Europe/London"",""localtime_epoch"":1730404739,""localtime"":""2024-10-31 19:58""},""astronomy"":{""astro"":{""sunrise"":""06:53 AM"",""sunset"":""04:34 PM"",""moonrise"":""05:44 AM"",""moonset"":""03:56 PM"",""moon_phase"":""Waning Crescent"",""moon_illumination"":2,""is_moon_up"":0,""is_sun_up"":0}}}";
        [Fact]
        public async void WeatherClient_GetCityStatus_Success()
        {
            var _settings = new Mock<IOptions<WeatherServiceSettings>>();
            var _weatherSettings = new WeatherServiceSettings { WeatherApiKey = "6789998212" };
            _settings.Setup(s => s.Value).Returns(new WeatherServiceSettings { WeatherApiKey = "6789998212" });
            var city = "london";
            var handlerMock = new Mock<HttpMessageHandler>();
                    handlerMock
             .Protected()
             .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith(_currentUrl("6789998212", "london"))),
                 ItExpr.IsAny<CancellationToken>())
             .ReturnsAsync(new HttpResponseMessage()
             {
                 Content = new StringContent(_currentLondonSuccess),
                 StatusCode = System.Net.HttpStatusCode.OK,
             });

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith(_astronomyUrl("6789998212", "london"))),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                Content = new StringContent(_astroLondonError),
                StatusCode = System.Net.HttpStatusCode.OK,
                });



            var _client = new HttpClient(handlerMock.Object);
            var url = _currentUrl(_weatherSettings.WeatherApiKey, city);
            var weatherClient = new WeatherClient(_settings.Object, _client);
            var _httpContent = new Mock<HttpContent>();

            var response = await weatherClient.GetCityStatus("london");
            Assert.NotNull(response);
        }
    }
}
