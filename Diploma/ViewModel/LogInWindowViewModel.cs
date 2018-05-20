using Diploma.Infrastructure;
using Diploma.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Diploma.ViewModel
{
    class LogInWindowViewModel : ViewModelBase
    {
        private string login;
        private string password;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            private get { return password; }
            set { password = value; }
        }

        private RelayCommand _passwordChanged;

        public ICommand PasswordChanged => _logInCommand ?? (_passwordChanged =
                                           new RelayCommand(PasswordChangedCommand));

        public void PasswordChangedCommand(object parameter)
        {
            try
            {
               
            }
            catch (Exception e)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
                messageBoxWindow.ShowDialog();
            }
        }

        public event EventHandler LoginCompleted;

        private RelayCommand _logInCommand;

        public ICommand LogIn => _logInCommand ?? (_logInCommand =
                                           new RelayCommand(LogInCommand));

        public void LogInCommand(object parameter)
        {
            try
            {
                bool logInResult = false;
                var passwordBox = parameter as PasswordBox;
                Password = passwordBox.Password;
                if ((Login != null && Login.Equals(@"123")) && (Password != null && Password.Equals("123")))
                    logInResult = true;
                MainWindowViewModel.Instance.SetLogInResult(logInResult);
            }
            catch (Exception e)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
                messageBoxWindow.ShowDialog();
            }
        }

        protected override void OnDispose()
        {
        }
    }
}
