using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels.Employee;
using System.Windows;

namespace EmployeeManagementSystem.Windows.Employee
{
    /// <summary>
    /// Interaction logic for EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        private readonly EmployeeEditViewModel _viewModel;
        public Models.Entities.Employee? Employee { get; set; }
        public bool IsReadOnlyMode { get; set; }

        public EmployeeEditWindow(EmployeeEditViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

            _viewModel.OnSubmitChangesSuccess += OnSubmitChangesSuccess;
        }

        private void OnSubmitChangesSuccess(object? sender, EventArgs e)
        {
            if (Employee == null)
                Employee = new Models.Entities.Employee();

            Employee.Id = _viewModel.Id ?? 0;
            Employee.Name = _viewModel.Name;
            Employee.Family = _viewModel.Family;
            Employee.Mobile = _viewModel.Mobile;
            Employee.Name = _viewModel.NationalCode;
            Employee.StudyField = _viewModel.StudyField;
            Employee.Avatar = _viewModel.Avatar;
            Employee.IsActive = _viewModel.IsActive;
            Employee.BirthDate = _viewModel.BirthDate;
            Employee.EmploymentDate = _viewModel.EmploymentDate;
            Employee.Gender = _viewModel.Gender;
            Employee.EducationLevel = _viewModel.EducationLevel;
            Employee.Position = _viewModel.Position;

            DialogResult = true;
        }

        protected override void OnActivated(EventArgs e)
        {
            if (Employee != null)
            {
                //var entry = dbContext.Entry(Book);
                //entry.Collection(b => b.Authors).Load();
                //entry.Reference(b => b.Category).Load();

                this.Title = "Edit Employee";
                _viewModel.Id = Employee.Id;
                _viewModel.Name = Employee.Name;
                _viewModel.Family = Employee.Family;
                _viewModel.Mobile = Employee.Mobile;
                _viewModel.NationalCode = Employee.NationalCode;
                _viewModel.StudyField= Employee.StudyField;
                _viewModel.Avatar= Employee.Avatar;
                _viewModel.IsActive= Employee.IsActive;
                _viewModel.BirthDate= Employee.BirthDate;
                _viewModel.EmploymentDate= Employee.EmploymentDate;
                _viewModel.Gender= Employee.Gender;
                _viewModel.EducationLevel = _viewModel.EducationLevels.First(c => c.Id == Employee.EducationLevelId);
                _viewModel.Position = _viewModel.Positions.First(a => a.Id == Employee.PositionId);
            }
            else
            {
                this.Title = "New Employee";
            }
            base.OnActivated(e);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
