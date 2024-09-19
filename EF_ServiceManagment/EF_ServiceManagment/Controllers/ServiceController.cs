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

        public ServiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServices()
        {

            var services = await _serviceManager.GetAsync();
            return Ok(services);

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _serviceManager.GetByIdAsync(id);
            return Ok(service);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest serviceRequest)
        {
            await _serviceManager.InsertAsync(serviceRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceRequest serviceRequest)
        {
            var service = await _serviceManager.GetByIdAsync(id);
            await _serviceManager.UpdateAsync(serviceRequest);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _serviceManager.GetByIdAsync(id);
            await _serviceManager.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("byCategory/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServicesByCategory(int categoryId)
        {
            var services = await _serviceManager.GetServicesByCategoryAsync(categoryId);
            return Ok(services);
        }

        [HttpGet("byTags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServicesByTags([FromQuery] string[] tags)
        {
            var services = await _serviceManager.GetServicesByTagsAsync(tags);
            return Ok(services);
        }
    }
}
