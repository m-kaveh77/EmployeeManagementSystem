using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Windows.Login;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagementSystem.Infrastructure
{
    public class AppHostedService(LoginWindow loginWindow, UserService userService) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!await userService.IsUserExistsAsync("admin"))
            {
                await userService.CreateUserAsync("admin", "123456", "sysadmin");
            }

            loginWindow.Show();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
