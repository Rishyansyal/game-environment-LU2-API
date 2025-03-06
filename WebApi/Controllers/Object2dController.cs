using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Object2d")]
    public class Object2dController : ControllerBase
    {
        private static List<Object2d> object2ds = new List<Object2d>()
        {
            new Object2d()
            {
                Id = 1,
                EnvironmentId = 100,
                PrefabId = "Tree",
                X_Position = 1.0f,
                Y_Position = 2.0f,
                ScaleX = 1.0f,
                ScaleY = 1.0f,
                RotationZ = 0.0f,
                SortingLayer = 1
            },
            new Object2d()
            {
                Id = 2,
                EnvironmentId = 101,
                PrefabId = "Rock",
                X_Position = 3.0f,
                Y_Position = 4.0f,
                ScaleX = 1.5f,
                ScaleY = 1.5f,
                RotationZ = 45.0f,
                SortingLayer = 2
            }
        };

        private readonly ILogger<Object2dController> _logger;

        public Object2dController(ILogger<Object2dController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "ReadObject2ds")]
        public ActionResult<IEnumerable<Object2d>> Get()
        {
            return object2ds;
        }

        [HttpGet("{id:int}", Name = "ReadObject2dById")]
        public ActionResult<Object2d> Get(int id)
        {
            Object2d object2d = GetObject2dById(id);
            if (object2d == null)
                return NotFound();

            return object2d;
        }

        [HttpPost(Name = "CreateObject2d")]
        public ActionResult Add(Object2d object2d)
        {
            if (GetObject2dById(object2d.Id) != null)
                return BadRequest("Object2d with ID " + object2d.Id + " already exists.");

            object2ds.Add(object2d);
            return CreatedAtAction(nameof(Get), new { id = object2d.Id }, object2d);
        }

        [HttpPut("{id:int}", Name = "UpdateObject2dById")]
        public IActionResult Update(int id, Object2d newObject2d)
        {
            if (id != newObject2d.Id)
                return BadRequest("The ID of the object did not match the ID of the route");

            Object2d object2dToUpdate = GetObject2dById(newObject2d.Id);
            if (object2dToUpdate == null)
                return NotFound();

            object2ds.Remove(object2dToUpdate);
            object2ds.Add(newObject2d);

            return Ok();
        }

        [HttpDelete("{id:int}", Name = "DeleteObject2dById")]
        public IActionResult Delete(int id)
        {
            Object2d object2dToDelete = GetObject2dById(id);
            if (object2dToDelete == null)
                return NotFound();

            object2ds.Remove(object2dToDelete);
            return Ok();
        }

        private Object2d GetObject2dById(int id)
        {
            foreach (Object2d object2d in object2ds)
            {
                if (object2d.Id == id)
                    return object2d;
            }

            return null;
        }
    }
}

