using Seedium.Models.Domain;

namespace Seedium.Repositories.Interface;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(Guid id);

    Task CreateAsync(Category category);

    Task<Category?> UpdateAsync(Guid id, Category category);

    Task DeleteAsync(Guid id);
}
