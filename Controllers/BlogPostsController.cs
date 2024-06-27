using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seedium.ActionFilters;
using Seedium.Models.Domain;
using Seedium.Models.DTO;
using Seedium.Repositories.Interface;

namespace Seedium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogPostsController : ControllerBase
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<BlogPostsController> _logger;

    public BlogPostsController(
        IBlogPostRepository blogPostrepository,
        ICategoryRepository categoryRepository,
        ILogger<BlogPostsController> logger
    )
    {
        _blogPostRepository = blogPostrepository;
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlogPosts(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 6,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool sortDesc = false,
        [FromQuery] string? filterOn = null,
        [FromQuery] string? filterQuery = null
    )
    {
        var blogposts = await _blogPostRepository.GetAllAsync(
            filterOn,
            filterQuery,
            sortBy,
            sortDesc,
            pageNumber,
            pageSize
        );
        var blogpostDtos = blogposts.Adapt<List<BlogPostDto>>();

        _logger.LogInformation("list of BlogPost Dto : {blogpostDtos}", blogpostDtos);
        return Ok(blogpostDtos);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        var blogPostDto = blogPost.Adapt<BlogPostDto>();

        _logger.LogInformation("BlogPost Dto : {@blogPostDto}", blogPostDto);
        return Ok(blogPostDto);
    }

    [HttpGet("{url}")]
    public async Task<IActionResult> GetBlogPostBySlug([FromRoute] string url)
    {
        var blogPost = await _blogPostRepository.GetByUrlAsync(url);
        if (blogPost == null)
        {
            return NotFound();
        }

        var blogPostDto = blogPost.Adapt<BlogPostDto>();

        _logger.LogInformation("BlogPost Dto : {@blogPostDto}", blogPostDto);
        return Ok(blogPostDto);
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
    {
        var blogPost = request.Adapt<BlogPost>();
        blogPost.Categories = [];

        foreach (var categoryGuid in request.Categories)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(categoryGuid);
            if (existingCategory != null)
            {
                blogPost.Categories.Add(existingCategory);
            }
        }

        await _blogPostRepository.CreateAsync(blogPost);

        var blogPostDto = blogPost.Adapt<BlogPostDto>();
        blogPostDto.Categories = blogPost
            .Categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            })
            .ToList();

        _logger.LogInformation("BlogPost Dto : {@blogPostDto}", blogPostDto);
        return Ok(blogPostDto);
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> UpdateBlogPost(
        [FromRoute] Guid id,
        [FromBody] UpdateBlogPostRequestDto request
    )
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        var blogPostToUpdate = request.Adapt<BlogPost>();
        blogPostToUpdate.Categories = [];
        foreach (var categoryGuid in request.Categories)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(categoryGuid);
            if (existingCategory != null)
            {
                blogPostToUpdate.Categories.Add(existingCategory);
            }
        }

        await _blogPostRepository.UpdateAsync(id, blogPostToUpdate);
        var blogPostDto = blogPost.Adapt<BlogPostDto>();

        _logger.LogInformation("BlogPost Dto : {@blogPostDto}", blogPostDto);
        return Ok(blogPostDto);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        await _blogPostRepository.DeleteAsync(id);

        _logger.LogInformation("BlogPost Deleted : {@blogPost}", blogPost);
        return NoContent();
    }
}
