using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService(EmployeeDbContext context)
    {
        public async Task<int> GetAllEmployeeCountAsync()
        {
            return await context.Employees.CountAsync();
        }

        public async Task<int> GetActiveEmployeeCountAsync()
        {
            return await context.Employees.CountAsync(e => e.IsActive);
        }
    }
}
