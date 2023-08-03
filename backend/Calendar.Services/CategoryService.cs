using Calendar.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalendarServices.Services;

public class CategoryService
{
    private readonly CalendarContext _context;

    public CategoryService(CalendarContext context)
    {
        _context = context;
    }

    public async Task<Category> Create(string name)
    {
        var category = new Category { Name = name };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        return category;
    }

    public async Task<Category?> Get(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<List<Category>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }
}
