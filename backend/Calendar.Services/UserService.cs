using Calendar.Models.Entities;
using Calendar.Data.Repositories;
using BCrypt;

namespace CalendarServices.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserAsync(string mail, string password)
    {
        var userFromDb = await _userRepository.GetByEmailAsync(mail);
        if (userFromDb == null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(password, userFromDb.Password))
        {
            return null;
        }

        return userFromDb;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        return await _userRepository.AddAsync(user);
    }

    public async Task<bool?> UpdateUserAsync(int id, string? mail, string? password)
    {
        var userFromDb = await _userRepository.GetByIdAsync(id);
        if (userFromDb == null)
        {
            return null;
        }

        if (mail != null)
        {
            userFromDb.Email = mail;
        }

        if (password != null)
        {
            userFromDb.Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        await _userRepository.UpdateAsync(userFromDb);
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}
