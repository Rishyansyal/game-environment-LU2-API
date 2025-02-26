using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("EnvironmentObject")]
    public class EnvironmentObjectsController : ControllerBase
    {
        private static List<EnvironmentObject> environmentObjects = new List<EnvironmentObject>()
        {
            new EnvironmentObject()
            {
                Id = 213132,
                WorldId = 432,
                ObjectType = "Tree",
                X_Position = 1.0f,
                Y_Position = 2.0f,
                Rotation = 3.0f
            }
        };

        private readonly ILogger<EnvironmentObjectsController> _logger;

        public EnvironmentObjectsController(ILogger<EnvironmentObjectsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "ReadEnvironmentObjects")]
        public ActionResult<IEnumerable<EnvironmentObject>> Get()
        {
            return environmentObjects;
        }

        [HttpGet("{id:int}", Name = "ReadEnvironmentObjectById")]
        public ActionResult<EnvironmentObject> Get(int id)
        {
            EnvironmentObject? environmentObject = GetEnvironmentObject(id);
            if (environmentObject == null)
                return NotFound();

            return environmentObject;
        }

        [HttpPost(Name = "CreateEnvironmentObject")]
        public ActionResult Add(EnvironmentObject environmentObject)
        {
            if (GetEnvironmentObject(environmentObject.Id) != null)
                return BadRequest("Environment object with ID " + environmentObject.Id + " already exists.");

            environmentObjects.Add(environmentObject);
            return CreatedAtAction(nameof(Get), new { id = environmentObject.Id }, environmentObject);
        }

        [HttpPut("{id:int}", Name = "UpdateEnvironmentObjectById")]
        public IActionResult Update(int id, EnvironmentObject newEnvironmentObject)
        {
            if (id != newEnvironmentObject.Id)
                return BadRequest("The ID of the object did not match the ID of the route");

            EnvironmentObject? environmentObjectToUpdate = GetEnvironmentObject(newEnvironmentObject.Id);
            if (environmentObjectToUpdate == null)
                return NotFound();

            environmentObjects.Remove(environmentObjectToUpdate);
            environmentObjects.Add(newEnvironmentObject);

            return Ok();
        }

        [HttpDelete("{id:int}", Name = "DeleteEnvironmentObjectById")]
        public IActionResult Delete(int id)
        {
            EnvironmentObject? environmentObjectToDelete = GetEnvironmentObject(id);
            if (environmentObjectToDelete == null)
                return NotFound();

            environmentObjects.Remove(environmentObjectToDelete);
            return Ok();
        }

        private EnvironmentObject? GetEnvironmentObject(int id)
        {
            foreach (EnvironmentObject environmentObject in environmentObjects)
            {
                if (environmentObject.Id == id)
                    return environmentObject;
            }

            return null;
        }
    }
}


