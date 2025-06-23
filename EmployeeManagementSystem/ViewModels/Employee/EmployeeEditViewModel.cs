using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Entities;
using EmployeeManagementSystem.Windows.Components;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EmployeeManagementSystem.ViewModels.Employee
{
    public class EmployeeEditViewModel : BaseViewModel
    {
        private readonly EmployeeDbContext _context;
        public event EventHandler? OnSubmitChangesSuccess;
        
        public EmployeeEditViewModel(EmployeeDbContext context)
        {
            _context = context;

            EducationLevels = _context.EducationLevels.AsNoTracking().OrderBy(c => c.Title).ToList();
            Positions = _context.Positions.AsNoTracking().OrderBy(c => c.Title).ToList();
            Genders = Enum.GetValues(typeof(GenderType));

            EmploymentDate = null;
            BirthDate = null;
        }

        private int? id;
        private string? name;
        private string? family;
        private string? mobile;
        private string? nationalCode;
        private string? studyField;
        private string? avatar;
        private bool isActive;
        private DateTime? birthDate;
        private DateTime? employementDate;
        private GenderType gender;
        private Models.Entities.EducationLevel? educationLevel;
        private Models.Entities.Position? position;
        private ImageSource? avatarPreview;

        public List<Models.Entities.EducationLevel> EducationLevels { get; set; }
        public List<Models.Entities.Position> Positions { get; set; }
        public Array Genders { get; set; }

        public int? Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Family
        {
            get => family;
            set => SetProperty(ref family, value);
        }

        public string Mobile
        {
            get => mobile;
            set => SetProperty(ref mobile, value);
        }

        public string NationalCode
        {
            get => nationalCode;
            set => SetProperty(ref nationalCode, value);
        }

        public string StudyField
        {
            get => studyField;
            set => SetProperty(ref studyField, value);
        }

        public string? Avatar
        {
            get => avatar;
            set => SetProperty(ref avatar, value);
        }

        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }

        public DateTime? BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value);
        }

        public DateTime? EmploymentDate
        {
            get => employementDate;
            set => SetProperty(ref employementDate, value);
        }

        public GenderType Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        public Models.Entities.EducationLevel EducationLevel
        {
            get => educationLevel;
            set => SetProperty(ref educationLevel, value);
        }

        public Models.Entities.Position Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public ImageSource? AvatarPreview
        {
            get => avatarPreview;
            set => SetProperty(ref avatarPreview, value);
        }

        public ICommand AcceptChangesCommand => ResolveCommand("AcceptChange", ExecuteSubmitChanges);
        public ICommand UploadPhotoCommand => ResolveCommand("UploadPhoto", UploadPhoto);

        private void UploadPhoto()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var imagePath = openFileDialog.FileName;

                AvatarPreview = new BitmapImage(new Uri(imagePath));

                byte[] imageBytes = File.ReadAllBytes(imagePath);
                Avatar = Convert.ToBase64String(imageBytes);
            }
        }

        private void ExecuteSubmitChanges()
        {
            if (!ValidateInputs())
                return;

            Models.Entities.Employee employee;

            if (Id.HasValue)
            {
                employee = _context.Employees
                    .Include(b => b.EducationLevel)
                    .Include(e => e.Position)
                    .First(b => b.Id == Id);
            }
            else
            {
                employee = new Models.Entities.Employee();

                _context.Employees.Add(employee);
            }

            employee.Name = Name;
            employee.Family = Family;
            employee.Mobile = Mobile;
            employee.NationalCode = NationalCode;
            employee.StudyField = StudyField;
            employee.Avatar = Avatar;
            employee.IsActive = true;
            employee.BirthDate = BirthDate;
            employee.EmploymentDate = EmploymentDate;
            employee.Gender = Gender;
            employee.EducationLevelId = EducationLevel?.Id;
            employee.PositionId = Position?.Id;

            _context.SaveChanges();

            Id = employee.Id;

            OnSubmitChangesSuccess?.Invoke(this, EventArgs.Empty);
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                CustomDialog.Show("Name is Required.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Family))
            {
                CustomDialog.Show("Family is Required.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Mobile))
            {
                CustomDialog.Show("Mobile is Required.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NationalCode) || NationalCode.Length < 10)
            {
                CustomDialog.Show("National code is incorrect.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            if (string.IsNullOrWhiteSpace(StudyField))
            {
                CustomDialog.Show("Study Field is Required.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            if (EducationLevel == null || string.IsNullOrWhiteSpace(EducationLevel.Title))
            {
                CustomDialog.Show("Education Level is required.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            if (Position == null || string.IsNullOrWhiteSpace(Position.Title))
            {
                CustomDialog.Show("Position is required.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            if (!Id.HasValue && _context.Employees.Any(m => m.NationalCode == NationalCode))
            {
                CustomDialog.Show("National code is already exists.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return false;
            }

            return true;
        }
    }
}
