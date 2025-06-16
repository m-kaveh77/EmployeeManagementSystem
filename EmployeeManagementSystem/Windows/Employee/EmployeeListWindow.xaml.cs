using EmployeeManagementSystem.ViewModels.Employee;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EmployeeManagementSystem.Windows.Employee
{
    /// <summary>
    /// Interaction logic for EmployeeListWindow.xaml
    /// </summary>
    public partial class EmployeeListWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EmployeeListViewModel _listViewModel;

        public EmployeeListWindow(EmployeeListViewModel listViewModel, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _listViewModel = listViewModel;
            DataContext = listViewModel;
            InitializeComponent();
        }

        private void AddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = _serviceProvider.GetService<EmployeeEditWindow>();
            var showDialog = editWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                EmployeeDataGrid.ItemsSource = null;
                EmployeeDataGrid.ItemsSource = _listViewModel.Employees;
            }
        }
    }
}
