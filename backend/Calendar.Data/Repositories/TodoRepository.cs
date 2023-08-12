using Calendar.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Data.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly CalendarContext _context;

    public TodoRepository(CalendarContext context)
    {
        _context = context;
    }

    public async Task<TodoItem> AddAsync(TodoItem todo)
    {
        await _context.TodoItems.AddAsync(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null)
        {
            return false;
        }

        _context.TodoItems.Remove(todo);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await this._context.TodoItems
            .Include(c => c.Categories)
            .ToListAsync();
    }

    public async Task<List<TodoItem>> GetDueTodayAsync(int userId)
    {
        return await this._context.TodoItems
            .Include(c => c.Categories)
            .Where(t => t.UserId == userId && t.DueAt == DateTime.Today)
            .ToListAsync();
    }

    public async Task<List<TodoItem>> GetQuadrantTodosAsync(int userId, Quadrant quadrant)
    {
        return await this._context.TodoItems
            .Include(c => c.Categories)
            .Where(t => t.UserId == userId && t.Quadrant == quadrant)
            .ToListAsync();
    }

    public async Task<List<TodoItem>> GetUserTodosAsync(int userId)
    {
        return await this._context.TodoItems
            .Include(c => c.Categories)
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<TodoItem?> UpdateAsync(TodoItem todo)
    {
        var todoToUpdate = this.GetByIdAsync(todo.Id);
        if (todoToUpdate == null)
        {
            return null;
        }

        this._context.TodoItems.Update(todo);
        await this._context.SaveChangesAsync();

        return todo;
    }

    public async Task<TodoItem?> AddCategoryById(int id, int categoryId)
    {
        var todo = await this._context.TodoItems.FindAsync(id);
        if (todo == null)
        {
            return null;
        }

        var category = await this._context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            return null;
        }

        todo.Categories.Add(category);
        await this._context.SaveChangesAsync();

        return todo;
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await this._context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TodoItem?> RemoveCategoryAsync(int todoId, int categoryId)
    {
        var todo = await this.GetByIdAsync(todoId);
        if (todo == null)
        {
            return null;
        }

        var category = await this._context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            return null;
        }

        todo.Categories.Remove(category);
        await this._context.SaveChangesAsync();

        return todo;
    }

    public async Task<TodoItem?> ChangeQuadrantAsync(int todoId, Quadrant quadrant)
    {
        var todo = await this.GetByIdAsync(todoId);
        if (todo == null)
        {
            return null;
        }

        todo.Quadrant = quadrant;
        await this._context.SaveChangesAsync();

        return todo;
    }
}
