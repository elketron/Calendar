using Microsoft.AspNetCore.Mvc;
using Calendar.Models.Entities;
using CalendarServices.Services;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Calendar.API.AuthorizeJwt;
using Calendar.API.Models;

[Route("/api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserLogin>> Register(User user)
    {
        if (!isMailValid(user.Email))
        {
            return BadRequest("Invalid mail");
        }

        var createdUser = await _userService.CreateUserAsync(user);
        var userLogin = new UserLogin
        {
            Id = createdUser.Id,
            Mail = createdUser.Email,
            Name = createdUser.Name
        };
        return Ok(userLogin);
    }

    [HttpGet("login/{mail}/{password}")]
    public async Task<ActionResult> Login(string mail, string password)
    {
        if (!isMailValid(mail))
        {
            return BadRequest("Invalid mail");
        }

        var user = await _userService.GetUserAsync(mail, password);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var tokenString = AuthorizeJwt.GenerateToken(_configuration, user.Id, user.Name, user.Email);
        return Ok(new { Token = tokenString });
    }

    [Authorize]
    [HttpPut()]
    public async Task<ActionResult<bool>> Update(string? mail, string? password)
    {
        if (mail != null && !isMailValid(mail))
        {
            return BadRequest("Invalid mail");
        }

        var id = AuthorizeJwt.GetUserId(HttpContext);
        if (id == -1)
        {
            return BadRequest("Invalid user id");
        }

        var updatedUser = await _userService.UpdateUserAsync(id, mail, password);
        if (updatedUser == false)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }

    [Authorize]
    [HttpDelete()]
    public async Task<ActionResult<string>> Delete()
    {
        var userId = AuthorizeJwt.GetUserId(HttpContext);
        if (userId == -1)
        {
            return BadRequest("Invalid user id");
        }
        var deleted = await _userService.DeleteUserAsync(userId);
        if (!deleted)
        {
            return NotFound();
        }
        return Ok(userId);
    }

    private bool isMailValid(string mail)
    {
        var mailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return mailRegex.IsMatch(mail);
    }

}
