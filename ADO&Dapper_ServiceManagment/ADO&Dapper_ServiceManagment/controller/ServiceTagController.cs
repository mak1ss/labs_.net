using ADO_Dapper_ServiceManagment.DAL.dtos.serviceTag;
using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ADO_Dapper_ServiceManagment.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceTagController : ControllerBase
    {
        private readonly ILogger<ServiceTagController> logger;
        private readonly IServiceTagManager serviceTagManager;
        private readonly IMapper mapper;

        public ServiceTagController(ILogger<ServiceTagController> logger, IServiceTagManager serviceTagManager, IMapper mapper)
        {
            this.logger = logger;
            this.serviceTagManager = serviceTagManager;
            this.mapper = mapper;
        }

        private IActionResult HandleException(Exception ex, string action)
        {
            logger.LogError($"Error during {action}: {ex.Message}");
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var serviceTags = serviceTagManager.GetAllServiceTags();
                var result = mapper.Map<IEnumerable<ServiceTagResponse>>(serviceTags);
                logger.LogInformation("Returned all service tags from the database");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "getting all service tags");
            }
        }

        [HttpPost]
        public IActionResult CreateServiceTag([FromBody] ServiceTagRequest serviceTagRequest)
        {
            try
            {
                var serviceTag = mapper.Map<ServiceTag>(serviceTagRequest);
                var rowsAffected = serviceTagManager.CreateServiceTag(serviceTag);
                logger.LogInformation("ServiceTag created");
                return Ok(new { RowsAffected = rowsAffected });
            }
            catch (Exception ex)
            {
                return HandleException(ex, "creating serviceTag");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServiceTag(int id)
        {
            try
            {
                var serviceTag = serviceTagManager.GetServiceTag(id);

                if (serviceTag == null)
                {
                    return BadRequest(new { Message = $"ServiceTag with id {id} doesn't exist" });
                }

                serviceTagManager.DeleteServiceTag(serviceTag);
                logger.LogInformation($"ServiceTag with id {id} deleted");

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"deleting serviceTag with id {id}");
            }
        }
    }
}
