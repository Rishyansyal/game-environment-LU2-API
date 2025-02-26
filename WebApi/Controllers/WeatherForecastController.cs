using Microsoft.AspNetCore.Mvc;
using System;
using WebApi;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectMap.WebApi.Controllers;

[ApiController]
[Route("Worlds")]
public class WeatherForecastController : ControllerBase
{
    private static List<Worlds> weatherForecasts = new List<Worlds>() 
    {
        new Worlds()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            TemperatureC = 20,
            Summary = "Perfect day for a walk."
        },
        new Worlds()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            TemperatureC = 4,
            Summary = "Pretty cold."
        },
        new Worlds()
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
    public ActionResult<IEnumerable<Worlds>> Get()
    {
        return weatherForecasts;
    }

    [HttpGet("{date:datetime}", Name = "ReadWeatherForecastByDate")]
    public ActionResult<Worlds> Get(DateOnly date)
    {
        Worlds weatherForeCast = GetWeatherForecast(date);
        if (weatherForeCast == null)
            return NotFound();

        return weatherForeCast;
    }


    [HttpPost(Name = "CreateWeatherForecast")]
    public ActionResult Add(Worlds weatherForecast)
    {
        if(GetWeatherForecast(weatherForecast.Date) != null)
            return BadRequest("Weather forecast for date " + weatherForecast.Date + " already exists.");

        weatherForecasts.Add(weatherForecast);
        return Created();
    }


    [HttpPut("{date:datetime}", Name = "UpdateWeatherForecastByDate")]
    public IActionResult Update(DateOnly date, Worlds newWeatherForeCast)
    {
        if(date != newWeatherForeCast.Date)
            return BadRequest("The id of the object did not match the id of the route");

        Worlds weatherForeCastToUpdate = GetWeatherForecast(newWeatherForeCast.Date);
        if (weatherForeCastToUpdate == null)
            return NotFound();

        weatherForecasts.Remove(weatherForeCastToUpdate);
        weatherForecasts.Add(newWeatherForeCast);

        return Ok();
    }

    [HttpDelete("{date:datetime}", Name = "DeleteWeatherForecastByDate")]
    public IActionResult Update(DateOnly date)
    {
        Worlds weatherForeCastToDelete = GetWeatherForecast(date);
        if (weatherForeCastToDelete == null)
            return NotFound();

        weatherForecasts.Remove(weatherForeCastToDelete);
        return Ok();
    }

    private Worlds GetWeatherForecast(DateOnly date)
    {
        foreach (Worlds weatherForecast in weatherForecasts)
        {
            if (weatherForecast.Date == date)
                return weatherForecast;
        }

        return null;
    }
}
