using EmployeeManagementSystem.Infrastructure;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.ViewModels.Login;
using EmployeeManagementSystem.Windows;
using EmployeeManagementSystem.Windows.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            var appBuilder = Host.CreateApplicationBuilder(e.Args);
            appBuilder.Configuration.AddJsonFile("appsettings.json");
            appBuilder.Services.AddHostedService<AppHostedService>();

            AddServices(appBuilder.Services);

            var host = appBuilder.Build();
            await host.RunAsync();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddDbContext<EmployeeDbContext>(ServiceLifetime.Transient);
            
            services.AddTransient<UserService>();

            // ViewModels
            services.AddTransient<LoginViewModel>();

            // Windows
            services.AddTransient<LoginWindow>();
            services.AddTransient<MainWindow>();
        }
    }
}
