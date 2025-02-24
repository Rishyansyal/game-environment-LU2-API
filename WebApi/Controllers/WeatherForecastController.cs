using Microsoft.AspNetCore.Mvc;
using System;
using WebApi;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectMap.WebApi.Controllers;

[ApiController]
[Route("EnvironmentCreator")]
public class WeatherForecastController : ControllerBase
{
    private static List<EnvironmentCreator> weatherForecasts = new List<EnvironmentCreator>() 
    {
        new EnvironmentCreator()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            TemperatureC = 20,
            Summary = "Perfect day for a walk."
        },
        new EnvironmentCreator()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            TemperatureC = 4,
            Summary = "Pretty cold."
        },
        new EnvironmentCreator()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            TemperatureC = 32,
            Summary = "Don't stay outside for too long."
        }
    };


    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "ReadWeatherForecasts")]
    public ActionResult<IEnumerable<EnvironmentCreator>> Get()
    {
        return weatherForecasts;
    }

    [HttpGet("{date:datetime}", Name = "ReadWeatherForecastByDate")]
    public ActionResult<EnvironmentCreator> Get(DateOnly date)
    {
        EnvironmentCreator weatherForeCast = GetWeatherForecast(date);
        if (weatherForeCast == null)
            return NotFound();

        return weatherForeCast;
    }


    [HttpPost(Name = "CreateWeatherForecast")]
    public ActionResult Add(EnvironmentCreator weatherForecast)
    {
        if(GetWeatherForecast(weatherForecast.Date) != null)
            return BadRequest("Weather forecast for date " + weatherForecast.Date + " already exists.");

        weatherForecasts.Add(weatherForecast);
        return Created();
    }


    [HttpPut("{date:datetime}", Name = "UpdateWeatherForecastByDate")]
    public IActionResult Update(DateOnly date, EnvironmentCreator newWeatherForeCast)
    {
        if(date != newWeatherForeCast.Date)
            return BadRequest("The id of the object did not match the id of the route");

        EnvironmentCreator weatherForeCastToUpdate = GetWeatherForecast(newWeatherForeCast.Date);
        if (weatherForeCastToUpdate == null)
            return NotFound();

        weatherForecasts.Remove(weatherForeCastToUpdate);
        weatherForecasts.Add(newWeatherForeCast);

        return Ok();
    }

    [HttpDelete("{date:datetime}", Name = "DeleteWeatherForecastByDate")]
    public IActionResult Update(DateOnly date)
    {
        EnvironmentCreator weatherForeCastToDelete = GetWeatherForecast(date);
        if (weatherForeCastToDelete == null)
            return NotFound();

        weatherForecasts.Remove(weatherForeCastToDelete);
        return Ok();
    }

    private EnvironmentCreator GetWeatherForecast(DateOnly date)
    {
        foreach (EnvironmentCreator weatherForecast in weatherForecasts)
        {
            if (weatherForecast.Date == date)
                return weatherForecast;
        }

        return null;
    }
}
