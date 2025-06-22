using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Windows.Components;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModels.EducationLevel
{
    public class EducationLevelEditViewModel : BaseViewModel
    {
        private readonly EmployeeDbContext _context;
        public event EventHandler? OnSubmitChangesSuccess;

        public EducationLevelEditViewModel(EmployeeDbContext context)
        {
            _context = context;
        }

        private int? id;
        private string? title;

        public int? Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public ICommand AcceptChangesCommand => ResolveCommand("AcceptChange", ExecuteSubmitChanges);

        private void ExecuteSubmitChanges()
        {
            if (!ValidateInputs())
                return;

            Models.Entities.EducationLevel educationLevel;

            if (Id.HasValue)
            {
                educationLevel = _context.EducationLevels.First(b => b.Id == Id);
            }
            else
            {
                educationLevel = new Models.Entities.EducationLevel();

                _context.EducationLevels.Add(educationLevel);
            }

            educationLevel.Title = Title;

            _context.SaveChanges();

            Id = educationLevel.Id;

            OnSubmitChangesSuccess?.Invoke(this, EventArgs.Empty);
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                CustomDialog.Show("Title is Required.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            return true;
        }
    }
}
