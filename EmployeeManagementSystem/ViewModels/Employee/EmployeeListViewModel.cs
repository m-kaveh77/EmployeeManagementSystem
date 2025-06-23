using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.ViewModels.Employee
{
    public class EmployeeListViewModel : BaseViewModel
    {
        private readonly EmployeeDbContext _context;
        public event Action<Models.Entities.Employee> OnSubmitChangesSuccess;

        public ObservableCollection<Models.Entities.Employee> Employees { get; set; } = new();

        public EmployeeListViewModel(EmployeeDbContext context)
        {
            _context = context;
            Employees = new ObservableCollection<Models.Entities.Employee>(context.Employees.Include(e => e.EducationLevel).Include(e => e.Position).ToList());

            ReloadItems();
        }

        public void ReloadItems()
        {
            Employees = new ObservableCollection<Models.Entities.Employee>(_context.Employees
                .Include(e => e.EducationLevel)
                .Include(e => e.Position)
                .AsNoTracking()
                .ToList());
        }
    }
}
