using Calendar.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Calendar.Data.Repositories;

namespace CalendarServices.Services;

public class TodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoItem> Create(TodoItem todo)
    {
        await _todoRepository.AddAsync(todo);
        return todo;
    }

    public async Task<TodoItem> Update(TodoItem todo)
    {
        await _todoRepository.UpdateAsync(todo);
        return todo;
    }

    public async Task<bool> Delete(int id)
    {
        return await _todoRepository.DeleteAsync(id);
    }

    public async Task<TodoItem?> Get(int id)
    {
        return await _todoRepository.GetByIdAsync(id);
    }

    public async Task<List<TodoItem>> GetAll()
    {
        return await _todoRepository.GetAllAsync();
    }

    public async Task<List<TodoItem>> GetByToday(int userId)
    {
        return await _todoRepository.GetDueTodayAsync(userId);
    }

    public async Task<List<TodoItem>> GetByQuadrant(int userId, Quadrant quadrant)
    {
        return await _todoRepository.GetQuadrantTodosAsync(userId, quadrant);
    }

    public async Task<List<TodoItem>> GetByQuadrantAndDate(int userId, Quadrant quadrant, DateTime date)
    {
        var todos = await _todoRepository.GetQuadrantTodosAsync(userId, quadrant);
        return todos.Where(t => t.DueAt == date.Date).ToList();
    }

    public async Task<List<TodoItem>> GetDone(int userId)
    {
        var todos = await _todoRepository.GetAllAsync();
        return todos.Where(t => t.IsDone && t.UserId == userId).ToList();
    }

    public async Task<List<TodoItem>> GetNotDone(int userId)
    {
        var todos = await _todoRepository.GetAllAsync();
        return todos.Where(t => !t.IsDone && t.UserId == userId).ToList();
    }

    public async Task<TodoItem?> AddCategory(int todoId, int categoryId)
    {
        return await _todoRepository.AddCategoryById(todoId, categoryId);
    }

    public async Task<TodoItem?> ChangeQuadrant(int todoId, Quadrant quadrant)
    {
        return await _todoRepository.ChangeQuadrantAsync(todoId, quadrant);
    }

    public async Task<TodoItem?> RemoveCategory(int todoId, int categoryId)
    {
        return await _todoRepository.RemoveCategoryAsync(todoId, categoryId);
    }
}
