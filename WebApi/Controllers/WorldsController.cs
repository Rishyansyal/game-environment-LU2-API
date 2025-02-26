using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApi.Models;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("World")]
    public class WorldsController : ControllerBase
    {
        private static List<WorldCreator> weatherForecasts = new List<WorldCreator>()
        {
            new WorldCreator()
            {
                Id = 432,
                UserId = 213132,
                Name = "World 1",
                CreatedAt = DateTime.UtcNow
            }
        };

        private readonly ILogger<WorldsController> _logger;

        public WorldsController(ILogger<WorldsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "ReadWorlds")]
        public ActionResult<IEnumerable<WorldCreator>> Get()
        {
            return weatherForecasts;
        }

        [HttpGet("{date:datetime}", Name = "ReadWorldByDate")]
        public ActionResult<WorldCreator> Get(DateOnly date)
        {
            WorldCreator? weatherForeCast = GetWeatherForecast(date);
            if (weatherForeCast == null)
                return NotFound();

            return weatherForeCast;
        }

        [HttpPost(Name = "CreateWorld")]
        public ActionResult Add(WorldCreator weatherForecast)
        {
            if (GetWeatherForecast(DateOnly.FromDateTime(weatherForecast.CreatedAt)) != null)
                return BadRequest("Weather forecast for date " + weatherForecast.CreatedAt + " already exists.");

            weatherForecasts.Add(weatherForecast);
            return CreatedAtAction(nameof(Get), new { date = weatherForecast.CreatedAt }, weatherForecast);
        }

        [HttpPut("{date:datetime}", Name = "UpdateWorldByDate")]
        public IActionResult Update(DateOnly date, WorldCreator newWeatherForeCast)
        {
            if (date != DateOnly.FromDateTime(newWeatherForeCast.CreatedAt))
                return BadRequest("The id of the object did not match the id of the route");

            WorldCreator? weatherForeCastToUpdate = GetWeatherForecast(DateOnly.FromDateTime(newWeatherForeCast.CreatedAt));
            if (weatherForeCastToUpdate == null)
                return NotFound();

            weatherForecasts.Remove(weatherForeCastToUpdate);
            weatherForecasts.Add(newWeatherForeCast);

            return Ok();
        }

        [HttpDelete("{date:datetime}", Name = "DeleteWorldByDate")]
        public IActionResult Delete(DateOnly date)
        {
            WorldCreator? weatherForeCastToDelete = GetWeatherForecast(date);
            if (weatherForeCastToDelete == null)
                return NotFound();

            weatherForecasts.Remove(weatherForeCastToDelete);
            return Ok();
        }

        private WorldCreator? GetWeatherForecast(DateOnly date)
        {
            foreach (WorldCreator weatherForecast in weatherForecasts)
            {
                if (DateOnly.FromDateTime(weatherForecast.CreatedAt) == date)
                    return weatherForecast;
            }

            return null;
        }
    }

}
