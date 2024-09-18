using EF_ServcieManagement.DAL.Exceptions;
using EF_ServiceManagement.BLL.DTO.Tag;
using EF_ServiceManagement.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EF_ServiceManagment.WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagService tagService, ILogger<TagController> logger)
        {
            _tagService = tagService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                var tags = await _tagService.GetAsync();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetTags)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the tags.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTagById(int id)
        {
            try
            {
                var tag = await _tagService.GetByIdAsync(id);
                return Ok(tag);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Tag with ID {id} not found: {ex.Message}");
                return NotFound($"Tag with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetTagById)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the tag.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTag([FromBody] TagRequest tagRequest)
        {
            if (tagRequest == null)
            {
                _logger.LogWarning("CreateTag was called with null request.");
                return BadRequest("Invalid tag request.");
            }

            try
            {
                await _tagService.InsertAsync(tagRequest);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(CreateTag)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the tag.");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] TagRequest tagRequest)
        {
            if (tagRequest == null || id <= 0)
            {
                _logger.LogWarning("UpdateTag was called with invalid parameters.");
                return BadRequest("Invalid tag request or ID.");
            }

            try
            {
                var tag = await _tagService.GetByIdAsync(id);
                await _tagService.UpdateAsync(tagRequest);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Tag with ID {id} not found: {ex.Message}");
                return NotFound($"Tag with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateTag)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the tag.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTag(int id)
        {
            try
            {
                var tag = await _tagService.GetByIdAsync(id);
                await _tagService.DeleteAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Tag with ID {id} not found: {ex.Message}");
                return NotFound($"Tag with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteTag)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the tag.");
            }
        }
    }
}
