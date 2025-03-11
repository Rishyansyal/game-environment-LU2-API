using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Repositories;  // Zorg ervoor dat deze namespace is toegevoegd

namespace WebApi.Controllers
{
    [ApiController]
    [Route("environments")]
    public class EnvironmentObjectsController : ControllerBase
    {
        private readonly IEnvironment2DRepository _repository;
        private readonly ILogger<EnvironmentObjectsController> _logger;

        // Injecteer de repository in de constructor
        public EnvironmentObjectsController(IEnvironment2DRepository repository, ILogger<EnvironmentObjectsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet(Name = "ReadEnvironmentObjects")]
        public async Task<ActionResult<IEnumerable<Environment2d>>> Get()
        {
            try
            {
                // Haal alle environment objects op via de repository
                var environmentObjects = await _repository.GetAllEnvironment2DsAsync();
                return Ok(environmentObjects);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR fetching environment objects: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}", Name = "ReadEnvironmentObjectById")]
        public async Task<ActionResult<Environment2d>> Get(int id)
        {
            try
            {
                // Haal een environment object op via de repository
                var environmentObject = await _repository.GetWorldByIdAsync(id);
                if (environmentObject == null)
                {
                    return NotFound();
                }
                return Ok(environmentObject);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR fetching environment object with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "CreateEnvironmentObject")]
        public async Task<ActionResult> Add([FromBody] Environment2d environmentObject)
        {
            try
            {
                // Controleer of het object al bestaat
                var existingObject = await _repository.GetWorldByIdAsync(environmentObject.Id);
                if (existingObject != null)
                {
                    return BadRequest($"Environment object with ID {environmentObject.Id} already exists.");
                }

                // Voeg het nieuwe object toe via de repository
                await _repository.AddWorldAsync(environmentObject);

                return CreatedAtAction(nameof(Get), new { id = environmentObject.Id }, environmentObject);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR creating environment object: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:int}", Name = "UpdateEnvironmentObjectById")]
        public async Task<IActionResult> Update(int id, [FromBody] Environment2d newEnvironmentObject)
        {
            if (id != newEnvironmentObject.Id)
            {
                return BadRequest("The ID of the object did not match the ID of the route");
            }

            try
            {
                // Haal het bestaande object op
                var existingObject = await _repository.GetWorldByIdAsync(newEnvironmentObject.Id);
                if (existingObject == null)
                {
                    return NotFound();
                }

                // Update het object via de repository
                await _repository.UpdateWorldAsync(newEnvironmentObject);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR updating environment object with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteEnvironmentObjectById")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Haal het object op
                var environmentObject = await _repository.GetWorldByIdAsync(id);
                if (environmentObject == null)
                {
                    return NotFound();
                }

                // Verwijder het object via de repository
                await _repository.DeleteWorldAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ ERROR deleting environment object with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
