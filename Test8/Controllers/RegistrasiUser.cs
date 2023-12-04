using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Test8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrasiUser : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<RegistrasiUser> _logger;

        public RegistrasiUser(ILogger<RegistrasiUser> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("/api/user/register")]    
        public async Task<IActionResult> RegisterUser(IFormCollection userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var (result, name) = await Upload(payload);
                    return Requests.Response(this, HttpStatusCode.OK, null, result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}