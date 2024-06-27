using Seedium.Models.Domain;

namespace Seedium.Repositories.Interface;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync(
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        bool sortDesc,
        int pageNumber,
        int pageSize
    );

    Task<BlogPost?> GetByIdAsync(Guid id);

    Task<BlogPost?> GetByUrlAsync(string url);

    Task CreateAsync(BlogPost blogPost);

    Task<BlogPost?> UpdateAsync(Guid id, BlogPost blogPost);

    Task DeleteAsync(Guid id);
}
