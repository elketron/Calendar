using Calendar.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalendarServices.Services;

public class TodoService
{
    private readonly CalendarContext _context;

    public TodoService(CalendarContext context)
    {
        _context = context;
    }

    public async Task<TodoItem> Create(TodoItem todo)
    {
        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<TodoItem> Update(TodoItem todo)
    {
        _context.TodoItems.Update(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<TodoItem?> Delete(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo != null)
        {
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
        }
        return todo;
    }

    public async Task<TodoItem?> Get(int id)
    {
        return await _context.TodoItems.Join(_context.Categories,
            todo => todo.Id,
            category => category.Id,
            (todo, category) => new TodoItem
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                DueAt = todo.DueAt,
                IsDone = todo.IsDone,
                Quadrant = todo.Quadrant,
                Categories = todo.Categories
            }).FirstOrDefaultAsync(todo => todo.Id == id);
    }

    public async Task<List<TodoItem>> GetAll()
    {
        return await _context.TodoItems.Join(_context.Categories,
            todo => todo.Id,
            category => category.Id,
            (todo, category) => new TodoItem
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                DueAt = todo.DueAt,
                IsDone = todo.IsDone,
                Quadrant = todo.Quadrant,
                Categories = todo.Categories
            }).ToListAsync();
    }

    public async Task<List<TodoItem>> GetByToday(DateTime date)
    {
        return await _context.TodoItems.Join(_context.Categories,
            todo => todo.Id,
            category => category.Id,
            (todo, category) => new TodoItem
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                DueAt = todo.DueAt,
                IsDone = todo.IsDone,
                Quadrant = todo.Quadrant,
                Categories = todo.Categories
            }).Where(todo => todo.DueAt == date).ToListAsync();
    }

    public async Task<List<TodoItem>> GetByQuadrant(Quadrant quadrant)
    {
        return await _context.TodoItems.Join(_context.Categories,
            todo => todo.Id,
            category => category.Id,
            (todo, category) => new TodoItem
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                DueAt = todo.DueAt,
                IsDone = todo.IsDone,
                Quadrant = todo.Quadrant,
                Categories = todo.Categories
            }).Where(todo => todo.Quadrant == quadrant).ToListAsync();
    }

    public async Task<List<TodoItem>> GetByQuadrantAndDate(Quadrant quadrant, DateTime date)
    {
        return await _context.TodoItems.Join(_context.Categories,
            todo => todo.Id,
            category => category.Id,
            (todo, category) => new TodoItem
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                DueAt = todo.DueAt,
                IsDone = todo.IsDone,
                Quadrant = todo.Quadrant,
                Categories = todo.Categories
            }).Where(todo => todo.Quadrant == quadrant && todo.DueAt == date).ToListAsync();
    }

    public async Task<List<TodoItem>> GetDone()
    {
        return await _context.TodoItems.Join(_context.Categories,
            todo => todo.Id,
            category => category.Id,
            (todo, category) => new TodoItem
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                DueAt = todo.DueAt,
                IsDone = todo.IsDone,
                Quadrant = todo.Quadrant,
                Categories = todo.Categories
            }).Where(t => t.IsDone).ToListAsync();
    }

    public async Task<List<TodoItem>> GetNotDone()
    {
        return await _context.TodoItems.Join(_context.Categories,
            todo => todo.Id,
            category => category.Id,
            (todo, category) => new TodoItem
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                DueAt = todo.DueAt,
                IsDone = todo.IsDone,
                Quadrant = todo.Quadrant,
                Categories = todo.Categories
            }).Where(t => !t.IsDone).ToListAsync();
    }

    public async Task<TodoItem?> AddCategory(int todoId, int categoryId)
    {
        var todo = await _context.TodoItems.FindAsync(todoId);
        var category = await _context.Categories.FindAsync(categoryId);
        if (todo != null && category != null)
        {
            todo.Categories!.Add(category);
            await _context.SaveChangesAsync();
        }
        return todo;
    }

    public async Task<TodoItem?> ChangeQuadrant(int todoId, Quadrant quadrant)
    {
        var todo = await _context.TodoItems.FindAsync(todoId);
        if (todo != null)
        {
            todo.Quadrant = quadrant;
            await _context.SaveChangesAsync();
        }
        return todo;

    }

    public async Task<TodoItem?> RemoveCategory(int todoId, int categoryId)
    {
        var todo = await _context.TodoItems.FindAsync(todoId);
        var category = await _context.Categories.FindAsync(categoryId);
        if (todo != null && category != null)
        {
            todo.Categories!.Remove(category);
            await _context.SaveChangesAsync();
        }
        return todo;
    }
}
