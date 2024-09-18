using EF_ServcieManagement.DAL.Exceptions;
using EF_ServiceManagement.BLL.DTO.Category;
using EF_ServiceManagement.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EF_ServiceManagment.WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCategories)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the categories.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Category with ID {id} not found: {ex.Message}");
                return NotFound($"Category with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCategoryById)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the category.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
            {
                _logger.LogWarning("CreateCategory was called with null request.");
                return BadRequest("Invalid category request.");
            }

            try
            {
                await _categoryService.InsertAsync(categoryRequest);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(CreateCategory)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the category.");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest categoryRequest)
        {
            if (categoryRequest == null || id <= 0)
            {
                _logger.LogWarning("UpdateCategory was called with invalid parameters.");
                return BadRequest("Invalid category request or ID.");
            }

            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                await _categoryService.UpdateAsync(categoryRequest);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Category with ID {id} not found: {ex.Message}");
                return NotFound($"Category with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateCategory)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the category.");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                await _categoryService.DeleteAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning($"Category with ID {id} not found: {ex.Message}");
                return NotFound($"Category with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteCategory)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the category.");
            }
        }
    }
}
