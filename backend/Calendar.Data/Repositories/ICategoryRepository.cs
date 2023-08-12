using Calendar.Models.Entities;

namespace Calendar.Data.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetByUserIdAsync(int userId);
    Task<Category> UpdateAsync(Category category);
    Task<bool> DeleteAsync(int id);
    Task<Category> AddAsync(Category category);
}
