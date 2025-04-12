using Microsoft.AspNetCore.Mvc;

namespace IceCreamRecipes.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogDebug("Weather forecast request received at {RequestTime}", DateTime.UtcNow);

        try
        {
            // Example of structured logging with properties
            var forecastCount = 5;
            _logger.LogInformation("Retrieving {ForecastCount} weather forecasts", forecastCount);

            var forecasts = Enumerable.Range(1, forecastCount).Select(index =>
            {
                var forecast = new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                };

                // Log each forecast as structured data
                _logger.LogInformation("Generated forecast: {@Forecast}", forecast);

                return forecast;
            }).ToArray();

            _logger.LogInformation("Successfully retrieved {Count} weather forecasts", forecasts.Length);

            // Example of warning level log
            if (forecasts.Any(f => f.TemperatureC > 40))
            {
                _logger.LogWarning("High temperature detected in forecast: {MaxTemp}°C",
                    forecasts.Max(f => f.TemperatureC));
            }

            return forecasts;
        }
        catch (Exception ex)
        {
            // Example of error level log
            _logger.LogError(ex, "Error occurred while retrieving weather forecasts");
            throw;
        }
    }

    [HttpGet("error")]
    public IActionResult SimulateError()
    {
        try
        {
            _logger.LogInformation("Simulating an error for testing");
            // Simulate an exception
            throw new InvalidOperationException("This is a test exception to demonstrate error logging");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Caught simulated exception: {ErrorMessage}", ex.Message);
            return StatusCode(500, "Error simulation successful! Check the logs for details.");
        }
    }
}

public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }
}
