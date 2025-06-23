using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Entities;
using EmployeeManagementSystem.Utils;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services
{
    public class UserService(EmployeeDbContext context)
    {
        public async Task<bool> IsUserExistsAsync(string username)
        {
            return await context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task CreateUserAsync(string username, string password, string role)
        {
            var passwordSalt = Guid.NewGuid().ToString("N");

            User user = new()
            {
                Username = username,
                Password = HashCreator.Sha256Hasher(password, passwordSalt),
                PasswordSalt = passwordSalt,
                CreateDate = DateTime.Now,
                Role = role
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsUserValidAsync(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return false;

            return user.Password == HashCreator.Sha256Hasher(password, user.PasswordSalt);
        }
    }
}
