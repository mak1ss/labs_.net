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

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.GetAsync();
            return Ok(tags);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            return Ok(tag);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTag([FromBody] TagRequest tagRequest)
        {
            await _tagService.InsertAsync(tagRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] TagRequest tagRequest)
        {
            var tag = await _tagService.GetByIdAsync(id);
            await _tagService.UpdateAsync(tagRequest);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            await _tagService.DeleteAsync(id);
            return Ok();
        }
    }
}
