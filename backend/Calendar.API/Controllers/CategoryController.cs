using Microsoft.AspNetCore.Mvc;
using CalendarServices.Services;
using Calendar.Models.Entities;

namespace Calendar.API.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<List<Category>> GetAll()
    {
        return await _categoryService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<Category?> Get(int id)
    {
        return await _categoryService.Get(id);
    }

    [HttpPost]
    public async Task<Category> Create(string name)
    {
        return await _categoryService.Create(name);
    }

    [HttpPut("")]
    public async Task<Category?> Update(Category category)
    {
        return await _categoryService.Update(category);
    }

    [HttpDelete("{id}")]
    public async Task<Category?> Delete(int id)
    {
        return await _categoryService.Delete(id);
    }

}
