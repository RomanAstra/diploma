using Diploma.Infrastructure;
using Diploma.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma.Helper;

namespace Diploma.ViewModel
{
    class LogInWindowViewModel : ViewModelBase
    {
        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
	        set
	        {
		        _login = value;
				OnPropertyChanged(Login);
	        }
        }

        public string Password
        {
            private get { return _password; }
            set
            {
	            _password = value;
	            OnPropertyChanged(Password);
            }
        }

	    #region Зачем это?

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

	    #endregion

        private RelayCommand _logInCommand;

        public ICommand LogIn => _logInCommand ?? (_logInCommand =
                                           new RelayCommand(ExecuteLogInCommand,
	                                           CanExecuteLogInCommand));

        public void ExecuteLogInCommand(object parameter)
        {
            try
            {
	            if (parameter is PasswordBox passwordBox)
				{
					Password = passwordBox.Password;
				}

				if (VariablesClass.Senders.ContainsKey(Login)
					&& VariablesClass.Senders[Login] == Password)
				{
					MainWindowViewModel.Instance.SetLogInResult(true);
				}
            }
            catch (Exception e)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
                messageBoxWindow.ShowDialog();
            }
		}

	    public bool CanExecuteLogInCommand(object parameter)
	    {
		    return !String.IsNullOrEmpty(Login);

	    }

	    #region Потом добавлю валидацию пароля

	    private void Password_Error(object sender, ValidationErrorEventArgs e)
	    {
		    if (e.Action == ValidationErrorEventAction.Added)
		    {
			    ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
		    }
		    else
		    {
			    ((Control)sender).ToolTip = "";
		    }
	    }

	    #endregion

		protected override void OnDispose()
        {
        }
    }
}
