using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //public ObservableCollection<Employee> Items { get; } = new();
        private readonly EmployeeService _employeeService;

        private int _allEmployeeCount;
        private int _activeEmployeeCount;

        public int AllEmployeeCount
        {
            get => _allEmployeeCount;
            set => SetProperty(ref _allEmployeeCount, value);
        }

        public int ActiveEmployeeCount
        {
            get => _activeEmployeeCount;
            set => SetProperty(ref _activeEmployeeCount, value);
        }

        public MainViewModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadAllEmployeeCount();
            await LoadActiveEmployeeCount();
        }

        private async Task LoadAllEmployeeCount()
        {
            AllEmployeeCount = await _employeeService.GetAllEmployeeCountAsync();
        }

        private async Task LoadActiveEmployeeCount()
        {
            ActiveEmployeeCount = await _employeeService.GetActiveEmployeeCountAsync();
        }
    }
}
