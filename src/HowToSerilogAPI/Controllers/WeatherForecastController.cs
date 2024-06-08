using Microsoft.AspNetCore.Mvc;

namespace HowToSerilogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Get Endpoint.... ");
            try
            {

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred!");
                throw;
            }
        }

        [HttpGet("Error")]
        public void Error()
        {
            _logger.LogInformation("GetError Endpoint.... ");
            try
            {

                throw new Exception("Sample error to demonstrate Serilog logging lib...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred!");
                throw;
            }
        }
    }
}
