using EmployeeManagementSystem.ViewModels;
using System.Windows;

namespace EmployeeManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(MainViewModel mainViewModel, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            DataContext = mainViewModel;
            InitializeComponent();
        }
    }
}
