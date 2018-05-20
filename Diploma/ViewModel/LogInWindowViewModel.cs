using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diploma.ViewModel
{
    class LogInWindowViewModel : ViewModelBase
    {
        //public event EventHandler LoginCompleted;

        //public void RaiseLoginCompleted()
        //{
        //    if (LoginCompleted != null)
        //    {
        //        LoginCompleted(this, EventArgs.Empty);
        //    }
        //}

        //private string login;
        //private string password;

        //public string Login
        //{
        //    get { return login; }
        //    set { login = value; OnPropertyChanged("Login"); }
        //}

        //public string Password
        //{
        //    private get { return password; }
        //    set { password = value; OnPropertyChanged("Password"); }
        //}

        //public void LoginOp(object o)
        //{
        //    RaiseLoginCompleted();
        //}

        //public ICommand LoginCommand { get; set; }

        //public LoginViewModel()
        //{
        //    LoginCommand = new DelegateCommand(LoginOp);
        //    OnPropertyChanged("LoginOp");
        //}

        protected override void OnDispose()
        {
        }
    }
}
