using EmployeeManagementSystem.ViewModels.EducationLevel;
using EmployeeManagementSystem.ViewModels.Employee;
using System.Windows;

namespace EmployeeManagementSystem.Windows.EducationLevel
{
    /// <summary>
    /// Interaction logic for EducationLevelEditWindow.xaml
    /// </summary>
    public partial class EducationLevelEditWindow : Window
    {
        private readonly EducationLevelEditViewModel _viewModel;
        public Models.Entities.EducationLevel? EducationLevel { get; set; }
        public bool IsReadOnlyMode { get; set; }

        public EducationLevelEditWindow(EducationLevelEditViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

            _viewModel.OnSubmitChangesSuccess += OnSubmitChangesSuccess;
        }

        private void OnSubmitChangesSuccess(object? sender, EventArgs e)
        {
            if (EducationLevel == null)
                EducationLevel = new Models.Entities.EducationLevel();

            EducationLevel.Id = _viewModel.Id ?? 0;
            EducationLevel.Title = _viewModel.Title;

            DialogResult = true;
        }

        protected override void OnActivated(EventArgs e)
        {
            if (EducationLevel != null)
            {
                this.Title = "Edit Education Level";
                _viewModel.Id = EducationLevel.Id;
                _viewModel.Title = EducationLevel.Title;
            }
            else
            {
                this.Title = "New Education Level";
            }
            base.OnActivated(e);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
