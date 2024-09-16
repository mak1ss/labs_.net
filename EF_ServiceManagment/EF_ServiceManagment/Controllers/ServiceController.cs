using EF_ServcieManagement.DAL.Exceptions;
using EF_ServiceManagement.BLL.DTO.Service;
using EF_ServiceManagement.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EF_ServiceManagment.WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IServiceManager serviceManager, ILogger<ServiceController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServices()
        {
            try
            {
                var services = await _serviceManager.GetAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetServices)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the services.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServiceById(int id)
        {
            try
            {
                var service = await _serviceManager.GetByIdAsync(id);
                return Ok(service);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Service with ID {id} not found: {ex.Message}");
                return NotFound($"Service with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetServiceById)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the service.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest serviceRequest)
        {
            if (serviceRequest == null)
            {
                _logger.LogWarning("CreateService was called with null request.");
                return BadRequest("Invalid service request.");
            }

            try
            {
                await _serviceManager.InsertAsync(serviceRequest);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(CreateService)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the service.");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceRequest serviceRequest)
        {
            if (serviceRequest == null || id <= 0)
            {
                _logger.LogWarning("UpdateService was called with invalid parameters.");
                return BadRequest("Invalid service request or ID.");
            }

            try
            {
                var service = await _serviceManager.GetByIdAsync(id);
                await _serviceManager.UpdateAsync(serviceRequest);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Service with ID {id} not found: {ex.Message}");
                return NotFound($"Service with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateService)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the service.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var service = await _serviceManager.GetByIdAsync(id);
                await _serviceManager.DeleteAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Service with ID {id} not found: {ex.Message}");
                return NotFound($"Service with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteService)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the service.");
            }
        }

        [HttpGet("byCategory/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServicesByCategory(int categoryId)
        {
            try
            {
                var services = await _serviceManager.GetServicesByCategoryAsync(categoryId);
                if (!services.Any())
                {
                    return NotFound("No services found for the given category.");
                }
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetServicesByCategory)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the services.");
            }
        }

        [HttpGet("byTags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServicesByTags([FromQuery] string[] tags)
        {
            try
            {
                var services = await _serviceManager.GetServicesByTagsAsync(tags);
                if (!services.Any())
                {
                    return NotFound("No services found for the given tags.");
                }
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetServicesByTags)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the services.");
            }
        }
    }
}
