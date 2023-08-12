using Calendar.Models.Entities;
using Calendar.Data.Repositories;

namespace CalendarServices.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Create(int userId,string name)
    {
        var category = new Category
        {
            Name = name,
            UserId = userId
        };
        await _categoryRepository.AddAsync(category);
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        await _categoryRepository.UpdateAsync(category);
        return category;
    }

    public async Task<bool> Delete(int id)
    {
        return await _categoryRepository.DeleteAsync(id);
    }

    public async Task<List<Category>> GetAll(int id)
    {
        return await _categoryRepository.GetByUserIdAsync(id);
    }
}
