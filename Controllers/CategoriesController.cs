using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seedium.Models.Domain;
using Seedium.Models.DTO;
using Seedium.Repositories.Interface;

namespace Seedium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(
        ICategoryRepository repository,
        ILogger<CategoriesController> logger
    )
    {
        _categoryRepository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoryDtos = categories.Adapt<List<CategoryDto>>();
        _logger.LogInformation("list of Categories Response Dto : {@categoryDto}", categoryDtos);
        return Ok(categoryDtos);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        var categoryDto = category.Adapt<CategoryDto>();
        _logger.LogInformation("Category Response Dto : {@categoryDto}", categoryDto);
        return Ok(categoryDto);
    }

    [HttpPost]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
    {
        var category = request.Adapt<Category>();

        await _categoryRepository.CreateAsync(category);
        var categoryDto = category.Adapt<CategoryDto>();
        _logger.LogInformation("Category Response Dto : {@categoryDto}", categoryDto);
        return Ok(categoryDto);
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryRequestDto request
    )
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        var categoryToUpdate = request.Adapt<Category>();

        await _categoryRepository.UpdateAsync(id, categoryToUpdate);
        var categoryDto = category.Adapt<CategoryDto>();
        _logger.LogInformation("Category Response Dto : {@categoryDto}", categoryDto);
        return Ok(categoryDto);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        await _categoryRepository.DeleteAsync(id);

        _logger.LogInformation("Category Deleted : {@category}", category);
        return NoContent();
    }
}
