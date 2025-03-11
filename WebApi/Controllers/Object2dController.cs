using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Repositories;
using System;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("environments/{environmentId}/objects")]
    public class Object2dController : ControllerBase
    {
        private readonly IObject2DRepository _repository;
        private readonly ILogger<Object2dController> _logger;

        public Object2dController(IObject2DRepository repository, ILogger<Object2dController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet(Name = "ReadObject2ds")]
        public async Task<ActionResult<IEnumerable<Object2d>>> Get(int environmentId)
        {
            try
            {
                var objectsInEnvironment = await _repository.GetAllObject2DsAsync();
                return Ok(objectsInEnvironment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR fetching objects: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ReadObject2dById")]
        public async Task<ActionResult<Object2d>> Get(int environmentId, string id)
        {
            try
            {
                var object2d = await _repository.GetObject2DByIdAsync(id);
                if (object2d == null || object2d.EnvironmentId != environmentId)
                {
                    return NotFound();
                }
                return Ok(object2d);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR fetching object with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "CreateObject2d")]
        public async Task<ActionResult> Add(int environmentId, [FromBody] Object2d object2d)
        {
            try
            {
                var existingObject = await _repository.GetObject2DByIdAsync(object2d.Id);
                if (existingObject != null)
                {
                    return BadRequest($"Object2d with ID {object2d.Id} already exists.");
                }

                object2d.EnvironmentId = environmentId;
                await _repository.AddObject2DAsync(object2d);

                return CreatedAtAction(nameof(Get), new { environmentId = environmentId, id = object2d.Id }, object2d);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR creating object: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}", Name = "UpdateObject2dById")]
        public async Task<IActionResult> Update(int environmentId, string id, [FromBody] Object2d newObject2d)
        {
            if (id != newObject2d.Id)
            {
                return BadRequest("The ID of the object did not match the ID of the route");
            }

            try
            {
                var existingObject = await _repository.GetObject2DByIdAsync(id);
                if (existingObject == null || existingObject.EnvironmentId != environmentId)
                {
                    return NotFound();
                }

                await _repository.UpdateObject2DAsync(newObject2d);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR updating object with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}", Name = "DeleteObject2dById")]
        public async Task<IActionResult> Delete(int environmentId, string id)
        {
            try
            {
                var object2d = await _repository.GetObject2DByIdAsync(id);
                if (object2d == null || object2d.EnvironmentId != environmentId)
                {
                    return NotFound();
                }

                await _repository.DeleteObject2DAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR deleting object with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
