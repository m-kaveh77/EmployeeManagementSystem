using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Windows.Components;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModels.Position
{
    public class PositionEditViewModel : BaseViewModel
    {
        private readonly EmployeeDbContext _context;
        public event EventHandler? OnSubmitChangesSuccess;

        public PositionEditViewModel(EmployeeDbContext context)
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

            Models.Entities.Position position;

            if (Id.HasValue)
            {
                position = _context.Positions.First(b => b.Id == Id);
            }
            else
            {
                position = new Models.Entities.Position();

                _context.Positions.Add(position);
            }

            position.Title = Title;

            _context.SaveChanges();

            Id = position.Id;

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
