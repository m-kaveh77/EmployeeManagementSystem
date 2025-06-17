using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels.Employee;
using EmployeeManagementSystem.Windows.Components;
using Microsoft.EntityFrameworkCore;
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

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            var context = _serviceProvider.GetService<EmployeeDbContext>();
            var selectedEmployee = EmployeeDataGrid.SelectedItem as Models.Entities.Employee;

            var editorWindow = _serviceProvider.GetService<EmployeeEditWindow>();

            editorWindow.Employee = context.Employees
                .Include(b => b.EducationLevel)
                .Include(b => b.Position)
                .First(b => b.Id == selectedEmployee.Id);

            var showDialog = editorWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                EmployeeDataGrid.ItemsSource = null;
                EmployeeDataGrid.ItemsSource = _listViewModel.Employees;
            }
        }

        private void DetailEmployee_Click(object sender, RoutedEventArgs e)
        {
            var context = _serviceProvider.GetService<EmployeeDbContext>();
            var selectedEmployee = EmployeeDataGrid.SelectedItem as Models.Entities.Employee;

            var editorWindow = _serviceProvider.GetService<EmployeeEditWindow>();

            editorWindow.Employee = context.Employees
                .Include(b => b.EducationLevel)
                .Include(b => b.Position)
                .First(b => b.Id == selectedEmployee.Id);

            editorWindow.IsReadOnlyMode = true;

            var showDialog = editorWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                EmployeeDataGrid.ItemsSource = null;
                EmployeeDataGrid.ItemsSource = _listViewModel.Employees;
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem == null)
            {
                CustomDialog.Show("Please Select an Item", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return;
            }

            if (CustomDialog.Show("Are you sure to delete?", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.YesNo) == MessageBoxResult.Yes)
            {
                var employee = (Models.Entities.Employee)EmployeeDataGrid.SelectedItem;
                var context = _serviceProvider.GetService<EmployeeDbContext>();
                var employeeToRemove = context.Employees.Find(employee.Id);

                context.Employees.Remove(employeeToRemove);
                context.SaveChanges();

                _listViewModel.Employees.Remove(employee);
            }
        }
    }
}
