using EmployeeManagementSystem.Models;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.ViewModels.Employee
{
    public class EmployeeListViewModel : BaseViewModel
    {
        private readonly EmployeeDbContext _context;
        public ObservableCollection<Models.Entities.Employee> Employees { get; set; } = new();

        public EmployeeListViewModel(EmployeeDbContext context)
        {
            _context = context;
            Employees = new ObservableCollection<Models.Entities.Employee>(context.Employees.ToList());

            ReloadItems();
        }

        public void ReloadItems()
        {
            Employees = new ObservableCollection<Models.Entities.Employee>(_context.Employees
                .Select(b => new Models.Entities.Employee
                {
                    Id = b.Id,
                    Name = b.Name,
                    Family = b.Family,
                    Mobile = b.Mobile,
                    NationalCode = b.NationalCode,
                    Avatar = b.Avatar,
                    Gender= b.Gender,
                    //Author = b.Authors.FirstOrDefault().Name
                }).ToList());
        }
    }
}
