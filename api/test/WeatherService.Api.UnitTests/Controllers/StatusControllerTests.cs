using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using WeatherService.Api.Client;
using WeatherService.Api.Controllers;
using WeatherService.Api.Controllers.Response;
using WeatherService.Api.Utils;

namespace WeatherService.Api.UnitTests.Controllers;

public class StatusControllerTests
{
    [Fact]
    public async Task Get_Status_ReturnsOkAsync()
    {
        var _settings = new Mock<IOptions<WeatherServiceSettings>>();
        var _httpClient = new Mock<HttpClient>();

        var _weatherClient = new Mock<WeatherClient>(MockBehavior.Strict, 
            new object[] { _settings.Object, _httpClient.Object });

        var city = "Rotterdam";
        var region = "South Holland";
        var country = "Netherlands";
        var localTime = "2022-04-12 11:33";
        var temperature = 15.0;
        var sunrise = "06:52 AM";
        var sunset = "08:34 PM";

        CityStatusResponse cityStatusResponse = new CityStatusResponse
        {
            City = city,
            Region = region,
            Country = country,
            LocalTime = localTime,
            Temperature = temperature,
            Sunrise = sunrise,
            Sunset = sunset,
        };

        _weatherClient.Setup(client => client.GetCityStatus(It.IsAny<string>())).ReturnsAsync(cityStatusResponse);
        var controller = new StatusController(_weatherClient.Object);
        var result = await controller.GetStatusAsync();

        Assert.IsType<OkObjectResult>(result);
    }
}
