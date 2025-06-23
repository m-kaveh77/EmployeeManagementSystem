using EmployeeManagementSystem.ViewModels.Login;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem.Windows.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _loginViewModel;

        public LoginWindow(LoginViewModel loginViewModel, MainWindow mainWindow)
        {
            _loginViewModel = loginViewModel;
            _loginViewModel.OnSuccessLogin += (sender, args) =>
            {
                mainWindow.Show();
                this.Close();
            };
            this.DataContext = _loginViewModel;
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _loginViewModel.Password = ((PasswordBox)sender).Password;
            LoginButton.IsEnabled = true;
        }
    }
}
