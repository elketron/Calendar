using Calendar.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CalendarContext _context;

    public CategoryRepository(CalendarContext context)
    {
        _context = context;
    }

    public async Task<Category> AddAsync(Category category)
    {
        this._context.Categories.Add(category);

        await this._context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await this._context.Categories.FindAsync(id);
        if (category == null)
        {
            return false;
        }

        this._context.Categories.Remove(category);
        await this._context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Category>> GetByUserIdAsync(int userId)
    {
        var categories = await this._context.Categories.Where(c => c.UserId == userId).ToListAsync();
        return categories;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        this._context.Categories.Update(category);
        await this._context.SaveChangesAsync();
        
        return category;
    }
}
