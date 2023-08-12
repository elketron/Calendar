using Calendar.Models.Entities;

namespace Calendar.Data.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
    Task<User> AddAsync(User user);
}
