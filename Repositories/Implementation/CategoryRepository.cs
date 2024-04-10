using Microsoft.EntityFrameworkCore;
using Seedium.Data;
using Seedium.Models.Domain;
using Seedium.Repositories.Interface;

namespace Seedium.Repositories.Implementation;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        return category;
    }

    public async Task CreateAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category?> UpdateAsync(Guid id, Category category)
    {
        var existingCategory = await GetByIdAsync(id);
        if (existingCategory != null)
        {
            existingCategory.Name = category.Name;
            existingCategory.Slug = category.Slug;
            await _context.SaveChangesAsync();
        }
        return existingCategory;
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingCategory = await GetByIdAsync(id);
        if (existingCategory != null)
        {
            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
        }
    }
}
