using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("environments")]
    public class EnvironmentObjectsController : ControllerBase
    {
        private static List<Environment2d> environmentObjects = new List<Environment2d>()
        {
            new Environment2d()
            {
                Id = 1,
                Name = "Forest",
                MaxLength = 100,
                MaxHeigth = 200
            },
            new Environment2d()
            {
                Id = 2,
                Name = "Desert",
                MaxLength = 150,
                MaxHeigth = 100
            }
        };

        private readonly ILogger<EnvironmentObjectsController> _logger;

        public EnvironmentObjectsController(ILogger<EnvironmentObjectsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "ReadEnvironmentObjects")]
        public ActionResult<IEnumerable<Environment2d>> Get()
        {
            return environmentObjects;
        }

        [HttpGet("{id:int}", Name = "ReadEnvironmentObjectById")]
        public ActionResult<Environment2d> Get(int id)
        {
            Environment2d? environmentObject = GetEnvironmentObject(id);
            if (environmentObject == null)
                return NotFound();

            return environmentObject;
        }

        [HttpPost(Name = "CreateEnvironmentObject")]
        public ActionResult Add(Environment2d environmentObject)
        {
            if (GetEnvironmentObject(environmentObject.Id) != null)
                return BadRequest("Environment object with ID " + environmentObject.Id + " already exists.");

            environmentObjects.Add(environmentObject);
            return CreatedAtAction(nameof(Get), new { id = environmentObject.Id }, environmentObject);
        }

        [HttpPut("{id:int}", Name = "UpdateEnvironmentObjectById")]
        public IActionResult Update(int id, Environment2d newEnvironmentObject)
        {
            if (id != newEnvironmentObject.Id)
                return BadRequest("The ID of the object did not match the ID of the route");

            Environment2d? environmentObjectToUpdate = GetEnvironmentObject(newEnvironmentObject.Id);
            if (environmentObjectToUpdate == null)
                return NotFound();

            environmentObjects.Remove(environmentObjectToUpdate);
            environmentObjects.Add(newEnvironmentObject);

            return Ok();
        }

        [HttpDelete("{id:int}", Name = "DeleteEnvironmentObjectById")]
        public IActionResult Delete(int id)
        {
            Environment2d? environmentObjectToDelete = GetEnvironmentObject(id);
            if (environmentObjectToDelete == null)
                return NotFound();

            environmentObjects.Remove(environmentObjectToDelete);
            return Ok();
        }

        private Environment2d? GetEnvironmentObject(int id)
        {
            foreach (Environment2d environmentObject in environmentObjects)
            {
                if (environmentObject.Id == id)
                    return environmentObject;
            }

            return null;
        }
    }
}







