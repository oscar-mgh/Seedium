using Seedium.Models.Domain;

namespace Seedium.Repositories.Interface;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync(
        int pageSize,
        int pageNumber,
        bool isAsc,
        string? sortBy,
        string? filterOn,
        string? filterQuery
    );

    Task<BlogPost?> GetByIdAsync(Guid id);

    Task<BlogPost?> GetByUrlAsync(string url);

    Task CreateAsync(BlogPost blogPost);

    Task<BlogPost?> UpdateAsync(Guid id, BlogPost blogPost);

    Task DeleteAsync(Guid id);
}
