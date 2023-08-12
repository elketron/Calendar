using Calendar.Models.Entities;

namespace Calendar.Data.Repositories;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetQuadrantTodosAsync(int userId, Quadrant quadrant);
    Task<List<TodoItem>> GetUserTodosAsync(int userId);
    Task<List<TodoItem>> GetDueTodayAsync(int userId);
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem?> UpdateAsync(TodoItem todo);
    Task<TodoItem?> RemoveCategoryAsync(int todoId, int categoryId);
    Task<TodoItem?> ChangeQuadrantAsync(int todoId, Quadrant quadrant);
    Task<bool> DeleteAsync(int id);
    Task<TodoItem> AddAsync(TodoItem todo);
    Task<TodoItem?> AddCategoryById(int todoId, int categoryId);
}
