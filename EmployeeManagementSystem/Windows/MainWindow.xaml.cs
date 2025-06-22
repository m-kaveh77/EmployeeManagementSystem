using EmployeeManagementSystem.ViewModels;
using EmployeeManagementSystem.Windows.EducationLevel;
using EmployeeManagementSystem.Windows.Employee;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EmployeeManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(MainViewModel mainViewModel, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            DataContext = mainViewModel;
            InitializeComponent();
        }

        private void EmployeeList_Click(object sender, RoutedEventArgs e)
        {
            var employeeListWindow = _serviceProvider.GetService<EmployeeListWindow>();
            employeeListWindow?.ShowDialog();
        }

        private void EducationLevels_Click(object sender, RoutedEventArgs e)
        {
            var educationLevelListWindow = _serviceProvider.GetService<EducationLevelListWindow>();
            educationLevelListWindow?.ShowDialog();
        }
    }
}
