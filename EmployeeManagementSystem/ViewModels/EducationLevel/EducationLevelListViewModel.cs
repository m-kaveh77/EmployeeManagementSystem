using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.ViewModels.EducationLevel
{
    public class EducationLevelListViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Entities.EducationLevel> EducationLevels { get; set; }
        private readonly EmployeeDbContext _context;
        public event Action<Models.Entities.Employee> OnSubmitChangesSuccess;

        public EducationLevelListViewModel(EmployeeDbContext context)
        {
            _context = context;
            EducationLevels = new ObservableCollection<Models.Entities.EducationLevel>(context.EducationLevels.ToList());

            ReloadItems();
        }

        public void ReloadItems()
        {
            EducationLevels = new ObservableCollection<Models.Entities.EducationLevel>(_context.EducationLevels
                .AsNoTracking()
                .ToList());
        }
    }
}
