using EmployeeManagementSystem.Infrastructure;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.ViewModels;
using EmployeeManagementSystem.ViewModels.EducationLevel;
using EmployeeManagementSystem.ViewModels.Employee;
using EmployeeManagementSystem.ViewModels.Login;
using EmployeeManagementSystem.Windows;
using EmployeeManagementSystem.Windows.EducationLevel;
using EmployeeManagementSystem.Windows.Employee;
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
            services.AddTransient<EmployeeService>();

            // ViewModels
            services.AddTransient<LoginViewModel>();

            services.AddTransient<MainViewModel>();

            services.AddTransient<EmployeeListViewModel>();
            services.AddTransient<EmployeeEditViewModel>();

            services.AddTransient<EducationLevelListViewModel>();
            services.AddTransient<EducationLevelEditViewModel>();

            // Windows
            services.AddTransient<LoginWindow>();
            
            services.AddTransient<MainWindow>();
            
            services.AddTransient<EmployeeListWindow>();
            services.AddTransient<EmployeeEditWindow>();
            
            services.AddTransient<EducationLevelListWindow>();
            services.AddTransient<EducationLevelEditWindow>();
        }
    }
}
