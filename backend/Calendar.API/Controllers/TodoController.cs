using Calendar.Models.Entities;
using CalendarServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.API.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
    {
        return await _todoService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoById(int id)
    {
        var todo = await _todoService.Get(id);

        if (todo == null)
        {
            return NotFound();
        }

        return todo;
    }

    [HttpGet("quadrant")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodosByQuadrant(Quadrant quadrant)
    {
        return await _todoService.GetByQuadrant(quadrant);
    }

    [HttpGet("quadant/{id}/{date}")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodosByQuadrantAndDate(Quadrant quadrant, DateTime date)
    {
        return await _todoService.GetByQuadrantAndDate(quadrant, date);
    }

    [HttpGet("today/{date}")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodosByToday(DateTime date)
    {
        return await _todoService.GetByToday(date);
    }

    [HttpGet("done")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetDoneTodos()
    {
        return await _todoService.GetDone();
    }

    [HttpGet("done/not")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetNotDoneTodos()
    {
        return await _todoService.GetNotDone();
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem todo)
    {
        await _todoService.Create(todo);

        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoById(int id, TodoItem todo)
    {
        if (id != todo.Id)
        {
            return BadRequest();
        }

        await _todoService.Update(todo);

        return NoContent();
    }

    [HttpPut("done/{id}")]
    public async Task<IActionResult> UpdateTodoDoneById(int id, bool done)
    {
        var todo = await _todoService.Get(id);

        if (todo == null)
        {
            return NotFound();
        }

        todo.IsDone = done;

        await _todoService.Update(todo);

        return NoContent();
    }

    [HttpPut("category/{id}/{categoryId}")]
    public async Task<IActionResult> AddTodoCategoryById(int id, int categoryId)
    {
        var todo = await _todoService.AddCategory(id, categoryId);

        if (todo == null)
        {
            return NotFound();
        }

        //await _todoService.Update(todo);

        return NoContent();
    }

    [HttpDelete("category/{id}/{categoryId}")]
    public async Task<IActionResult> RemoveTodoCategoryById(int id, int categoryId)
    {
        var todo = await _todoService.RemoveCategory(id, categoryId);

        if (todo == null)
        {
            return NotFound();
        }

        await _todoService.Update(todo);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoById(int id)
    {
        var todo = await _todoService.Get(id);

        if (todo == null)
        {
            return NotFound();
        }

        await _todoService.Delete(id);

        return NoContent();
    }


}
