using Mapster;
using Microsoft.AspNetCore.Mvc;
using Seedium.Models.Domain;
using Seedium.Models.DTO;
using Seedium.Repositories.Interface;

namespace Seedium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageRepository _imageRepository;
    private readonly ILogger<ImagesController> _logger;

    public ImagesController(IImageRepository imageRepository, ILogger<ImagesController> logger)
    {
        _imageRepository = imageRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(
        [FromForm] IFormFile file,
        [FromForm] string fileName,
        [FromForm] string title
    )
    {
        ValidateFileUpload(file);

        if (ModelState.IsValid)
        {
            var blogImage = new BlogImage
            {
                FileExtension = Path.GetExtension(file.FileName),
                FileName = fileName,
                Title = title,
                DateCreated = DateTime.Now
            };

            blogImage = await _imageRepository.Upload(file, blogImage);

            _logger.LogInformation("Image Uploaded : {@blogImage}", blogImage);
            return Ok(blogImage.Adapt<BlogImageDto>());
        }

        _logger.LogError("Could not upload image");
        return BadRequest(ModelState);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllImages()
    {
        var blogImages = await _imageRepository.GetAllAsync();
        var blogImagesDto = blogImages.Adapt<List<BlogImageDto>>();

        _logger.LogInformation("list of BlogImage Dto : {@blogImagesDto}", blogImagesDto);
        return Ok(blogImagesDto);
    }

    private void ValidateFileUpload(IFormFile file)
    {
        string[] allowedExtensions = [".jpg", ".jpeg", ".png"];

        if (!allowedExtensions.Contains(Path.GetExtension(file.FileName)))
        {
            ModelState.AddModelError("file", "file extension not allowed");
        }

        if (file.Length > 10485760)
        {
            ModelState.AddModelError("file", "file size is too big, the max size is 9MB");
        }
    }
}
