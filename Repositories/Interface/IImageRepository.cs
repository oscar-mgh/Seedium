using Seedium.Models.Domain;

namespace Seedium.Repositories.Interface;

public interface IImageRepository
{
    Task<BlogImage> Upload(IFormFile file, BlogImage image);

    Task<IEnumerable<BlogImage>> GetAllAsync();
}
