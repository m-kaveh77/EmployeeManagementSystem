using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Windows.Commponents;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModels.Login
{
    public class LoginViewModel(UserService userService) : BaseViewModel
    {
        private string? username;
        private string? password;

        public event EventHandler? OnSuccessLogin;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ICommand? LoginCommand => ResolveCommand("LoginCommand", LoginCommandAction, LoginCommandValidator);

        private async void LoginCommandAction()
        {
            if (!await userService.IsUserValidAsync(Username, Password))
            {
                CustomDialog.Show("Username or Password is incorrect.", CustomDialog.DialogIconType.Error, CustomDialog.DialogButtonType.OK);
                return;
            }

            if (OnSuccessLogin != null)
            {
                OnSuccessLogin(this, EventArgs.Empty);
            }
        }

        private bool LoginCommandValidator()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }
    }
}
