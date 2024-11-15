using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Api.Controllers.Response;

namespace WeatherService.Api.UnitTests.Controllers.Response
{
    public class CityStatusResponseTest
    {
        [Fact]
        public void CityStatusResponse_TestProperties()
        {
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
            
            Assert.Equal(cityStatusResponse.City, city);
            Assert.Equal(cityStatusResponse.Region, region);
            Assert.Equal(cityStatusResponse.Country, country);
            Assert.Equal(cityStatusResponse.LocalTime, localTime);
            Assert.Equal(cityStatusResponse.Temperature, temperature);
            Assert.Equal(cityStatusResponse.Sunrise, sunrise);
            Assert.Equal(cityStatusResponse.Sunset, sunset);
        }
    }
}
