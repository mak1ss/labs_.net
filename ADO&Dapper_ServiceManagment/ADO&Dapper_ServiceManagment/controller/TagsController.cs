using ADO_Dapper_ServiceManagment.DAL.dtos.tag;
using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ADO_Dapper_ServiceManagment.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> logger;
        private readonly ITagService tagService;
        private readonly IMapper mapper;

        public TagController(ILogger<TagController> logger, ITagService tagManager, IMapper mapper)
        {
            this.logger = logger;
            this.tagService = tagManager;
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
                var tags = tagService.GetAllTags();
                var result = mapper.Map<IEnumerable<TagResponse>>(tags);
                logger.LogInformation("Returned all tags from the database");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "getting all tags");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var tag = tagService.GetTagById(id);
                if (tag == null)
                {
                    logger.LogWarning($"Tag with id {id} not found");
                    return NotFound(new { Message = $"Tag with id {id} not found" });
                }

                var result = mapper.Map<TagResponse>(tag);
                logger.LogInformation($"Returned tag with id {id}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"getting tag by id {id}");
            }
        }

        [HttpPost]
        public IActionResult CreateTag([FromBody] TagRequest tagRequest)
        {
            try
            {
                var tag = mapper.Map<Tag>(tagRequest);
                var rowsAffected = tagService.CreateTag(tag);
                logger.LogInformation("Tag created");
                return Ok(new { RowsAffected = rowsAffected });
            }
            catch (Exception ex)
            {
                return HandleException(ex, "creating tag");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTag(int id, [FromBody] TagRequest tagRequest)
        {
            try
            {
                var tag = tagService.GetTagById(id);
                if (tag == null)
                {
                    return BadRequest(new { Message = $"Tag with id {id} doesn't exist" });
                }

                tag = mapper.Map<Tag>(tagRequest);
                tag.Id = id;

                tagService.UpdateTag(tag);
                logger.LogInformation($"Tag with id {id} updated");
                return Ok(tagService.GetTagById(tag.Id));
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"updating tag with id {id}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            try
            {
                var tag = tagService.GetTagById(id);

                if (tag == null)
                {
                    return BadRequest(new { Message = $"Tag with id {id} doesn't exist" });
                }

                tagService.DeleteTag(tag);
                logger.LogInformation($"Tag with id {id} deleted");

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"deleting tag with id {id}");
            }
        }
    }
}
