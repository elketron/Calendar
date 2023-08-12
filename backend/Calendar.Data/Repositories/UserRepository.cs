using Calendar.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CalendarContext _context;

    public UserRepository(CalendarContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        this._context.Users.Add(user);
        await this._context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userToDelete = await this.GetByIdAsync(id);
        if (userToDelete == null)
        {
            return false;
        }

        this._context.Users.Remove(userToDelete);

        await this._context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await this._context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await this._context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var userToUpdate = await this.GetByIdAsync(user.Id);
        if (userToUpdate == null)
        {
            return null;
        }

        userToUpdate.Email = user.Email;
        userToUpdate.Password = user.Password;
        userToUpdate.Name = user.Name;

        await this._context.SaveChangesAsync();
        return userToUpdate;
    }
}
