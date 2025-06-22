using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.ViewModels.Position
{
    public class PositionListViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Entities.Position> Positions { get; set; }
        private readonly EmployeeDbContext _context;
        public event Action<Models.Entities.Position> OnSubmitChangesSuccess;

        public PositionListViewModel(EmployeeDbContext context)
        {
            _context = context;
            Positions = new ObservableCollection<Models.Entities.Position>(context.Positions.ToList());

            ReloadItems();
        }

        public void ReloadItems()
        {
            Positions = new ObservableCollection<Models.Entities.Position>(_context.Positions.AsNoTracking().ToList());
        }
    }
}
