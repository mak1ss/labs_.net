using ADO_Dapper_ServiceManagment.DAL.dtos.service;
using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ADO_Dapper_ServiceManagment.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly ILogger<ServicesController> logger;
        private readonly IServiceManager serviceManager;
        private readonly IMapper mapper;

        public ServicesController(ILogger<ServicesController> logger, IServiceManager serviceManager, IMapper mapper)
        {
            this.logger = logger;
            this.serviceManager = serviceManager;
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
                var services = serviceManager.GetAllServices();
                var result = mapper.Map<IEnumerable<ServiceResponse>>(services);
                logger.LogInformation("Returned all services from the database");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "getting all services");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var service = serviceManager.GetServiceById(id);
                if (service == null)
                {
                    logger.LogWarning($"Service with id {id} not found");
                    return NotFound(new { Message = $"Service with id {id} not found" });
                }

                var result = mapper.Map<ServiceResponse>(service);
                logger.LogInformation($"Returned service with id {id}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"getting service by id {id}");
            }
        }

        [HttpPost]
        public IActionResult CreateService([FromBody] ServiceRequest serviceRequest)
        {
            try
            {
                var service = mapper.Map<Service>(serviceRequest);
                var newServiceId = serviceManager.CreateService(service);
                logger.LogInformation($"Service created with id {newServiceId}");
                return Ok(new { ServiceId = newServiceId });
            }
            catch (Exception ex)
            {
                return HandleException(ex, "creating service");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, [FromBody] ServiceRequest serviceRequest)
        {
            try
            {
                var service = serviceManager.GetServiceById(id);
                if(service == null)
                {
                    return BadRequest(new { Message = $"Service with id {id} doesn't exist" });
                }

                service = mapper.Map<Service>(serviceRequest);

                
                service.Id = id;

                serviceManager.UpdateService(service);
                logger.LogInformation($"Service with id {id} updated");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"updating service with id {id}");
            }
        }
    }
}
