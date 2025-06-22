using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels.EducationLevel;
using EmployeeManagementSystem.Windows.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EmployeeManagementSystem.Windows.EducationLevel
{
    /// <summary>
    /// Interaction logic for EducationLevelListWindow.xaml
    /// </summary>
    public partial class EducationLevelListWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EducationLevelListViewModel _listViewModel;

        public EducationLevelListWindow(IServiceProvider serviceProvider, EducationLevelListViewModel listViewModel)
        {
            _serviceProvider = serviceProvider;
            _listViewModel = listViewModel;
            DataContext = listViewModel;
            InitializeComponent();
        }

        private void AddNewEducationLevel_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = _serviceProvider.GetService<EducationLevelEditWindow>();
            var showDialog = editWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                EducationLevelDataGrid.ItemsSource = null;
                EducationLevelDataGrid.ItemsSource = _listViewModel.EducationLevels;
            }
        }

        private void DetailEducationLevel_Click(object sender, RoutedEventArgs e)
        {
            var context = _serviceProvider.GetService<EmployeeDbContext>();
            var selectedEduactionLevel = EducationLevelDataGrid.SelectedItem as Models.Entities.EducationLevel;

            var editorWindow = _serviceProvider.GetService<EducationLevelEditWindow>();

            editorWindow.EducationLevel = context.EducationLevels.First(b => b.Id == selectedEduactionLevel.Id);

            editorWindow.IsReadOnlyMode = true;

            var showDialog = editorWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                EducationLevelDataGrid.ItemsSource = null;
                EducationLevelDataGrid.ItemsSource = _listViewModel.EducationLevels;
            }
        }

        private void EditEducationLevel_Click(object sender, RoutedEventArgs e)
        {
            var context = _serviceProvider.GetService<EmployeeDbContext>();
            var selectedEduactionLevel = EducationLevelDataGrid.SelectedItem as Models.Entities.EducationLevel;

            var editorWindow = _serviceProvider.GetService<EducationLevelEditWindow>();

            editorWindow.EducationLevel = context.EducationLevels.First(b => b.Id == selectedEduactionLevel.Id);

            var showDialog = editorWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                EducationLevelDataGrid.ItemsSource = null;
                EducationLevelDataGrid.ItemsSource = _listViewModel.EducationLevels;
            }
        }

        private void DeleteEducationLevel_Click(object sender, RoutedEventArgs e)
        {
            if (EducationLevelDataGrid.SelectedItem == null)
            {
                CustomDialog.Show("Please Select an Item", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return;
            }

            if (CustomDialog.Show("Are you sure to delete?", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.YesNo) == MessageBoxResult.Yes)
            {
                var educationLevel = (Models.Entities.EducationLevel)EducationLevelDataGrid.SelectedItem;
                var context = _serviceProvider.GetService<EmployeeDbContext>();
                var educationLevelToRemove = context.EducationLevels.Find(educationLevel.Id);

                context.EducationLevels.Remove(educationLevelToRemove);
                context.SaveChanges();

                _listViewModel.EducationLevels.Remove(educationLevel);
            }
        }
    }
}
