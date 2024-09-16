using ADO_Dapper_ServiceManagment.DAL.dtos.category;
using ADO_Dapper_ServiceManagment.DAL.entities;
using ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ADO_Dapper_ServiceManagment.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> logger;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryManager, IMapper mapper)
        {
            this.logger = logger;
            this.categoryService = categoryManager;
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
                var categories = categoryService.GetAllCategories();
                var result = mapper.Map<IEnumerable<CategoryResponse>>(categories);
                logger.LogInformation("Returned all categories from the database");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "getting all categories");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = categoryService.GetCategoryById(id);
                if (category == null)
                {
                    logger.LogWarning($"Category with id {id} not found");
                    return NotFound(new { Message = $"Category with id {id} not found" });
                }

                var result = mapper.Map<CategoryResponse>(category);
                logger.LogInformation($"Returned category with id {id}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"getting category by id {id}");
            }
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryRequest categoryRequest)
        {
            try
            {
                var category = mapper.Map<Category>(categoryRequest);
                var rowsAffected = categoryService.CreateCategory(category);
                logger.LogInformation("Category created");
                return Ok(new { RowsAffected = rowsAffected });
            }
            catch (Exception ex)
            {
                return HandleException(ex, "creating category");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryRequest categoryRequest)
        {
            try
            {
                var category = categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return BadRequest(new { Message = $"Category with id {id} doesn't exist" });
                }

                category = mapper.Map<Category>(categoryRequest);
                category.Id = id;

                categoryService.UpdateCategory(category);
                logger.LogInformation($"Category with id {id} updated");
                return Ok(categoryService.GetCategoryById(category.Id));
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"updating category with id {id}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = categoryService.GetCategoryById(id);

                if (category == null)
                {
                    return BadRequest(new { Message = $"Category with id {id} doesn't exist" });
                }

                categoryService.DeleteCategory(category);
                logger.LogInformation($"Category with id {id} deleted");

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"deleting category with id {id}");
            }
        }
    }
}
