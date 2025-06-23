using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels.Position;
using EmployeeManagementSystem.Windows.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EmployeeManagementSystem.Windows.Position
{
    /// <summary>
    /// Interaction logic for PositionListWindow.xaml
    /// </summary>
    public partial class PositionListWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PositionListViewModel _listViewModel;

        public PositionListWindow(IServiceProvider serviceProvider, PositionListViewModel listViewModel)
        {
            _serviceProvider = serviceProvider;
            _listViewModel = listViewModel;
            DataContext = listViewModel;
            InitializeComponent();
        }

        private void AddNewPosition_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = _serviceProvider.GetService<PositionEditWindow>();
            var showDialog = editWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                PositionDataGrid.ItemsSource = null;
                PositionDataGrid.ItemsSource = _listViewModel.Positions;
            }
        }

        private void DetailPosition_Click(object sender, RoutedEventArgs e)
        {
            var context = _serviceProvider.GetService<EmployeeDbContext>();
            var selectedPosition = PositionDataGrid.SelectedItem as Models.Entities.Position;

            var editorWindow = _serviceProvider.GetService<PositionEditWindow>();

            editorWindow.Position = context.Positions.First(b => b.Id == selectedPosition.Id);

            editorWindow.IsReadOnlyMode = true;

            var showDialog = editorWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                PositionDataGrid.ItemsSource = null;
                PositionDataGrid.ItemsSource = _listViewModel.Positions;
            }
        }

        private void EditPosition_Click(object sender, RoutedEventArgs e)
        {
            var context = _serviceProvider.GetService<EmployeeDbContext>();
            var selectedPosition = PositionDataGrid.SelectedItem as Models.Entities.Position;

            var editorWindow = _serviceProvider.GetService<PositionEditWindow>();

            editorWindow.Position = context.Positions.First(b => b.Id == selectedPosition.Id);

            var showDialog = editorWindow?.ShowDialog();

            if (showDialog ?? false)
            {
                _listViewModel.ReloadItems();
                PositionDataGrid.ItemsSource = null;
                PositionDataGrid.ItemsSource = _listViewModel.Positions;
            }
        }

        private void DeletePosition_Click(object sender, RoutedEventArgs e)
        {
            if (PositionDataGrid.SelectedItem == null)
            {
                CustomDialog.Show("Please Select an Item", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return;
            }

            if (CustomDialog.Show("Are you sure to delete?", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.YesNo) == MessageBoxResult.Yes)
            {
                var position = (Models.Entities.Position)PositionDataGrid.SelectedItem;
                var context = _serviceProvider.GetService<EmployeeDbContext>();
                var positionToRemove = context.Positions.Find(position.Id);

                context.Positions.Remove(positionToRemove);
                context.SaveChanges();

                _listViewModel.Positions.Remove(position);
            }
        }
    }
}
