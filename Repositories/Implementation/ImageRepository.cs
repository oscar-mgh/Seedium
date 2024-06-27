using Microsoft.EntityFrameworkCore;
using Seedium.Data;
using Seedium.Models.Domain;
using Seedium.Repositories.Interface;

namespace Seedium.Repositories.Implementation;

public class ImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _dbContext;

    public ImageRepository(
        IWebHostEnvironment webHostEnvironment,
        IHttpContextAccessor httpContextAccessor,
        AppDbContext dbContext
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BlogImage>> GetAllAsync()
    {
        return await _dbContext.BlogImages.ToListAsync();
    }

    public async Task<BlogImage> Upload(IFormFile file, BlogImage image)
    {
        var localPath = Path.Combine(
            _webHostEnvironment.ContentRootPath,
            "wwwroot", "Images",
            $"{image.FileName}{image.FileExtension}"
        );
        using var stream = new FileStream(localPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var urlPath =
            $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/wwwroot/Images/{image.FileName}{image.FileExtension}";

        image.Url = urlPath;
        _dbContext.BlogImages.Add(image);
        await _dbContext.SaveChangesAsync();

        return image;
    }
}
