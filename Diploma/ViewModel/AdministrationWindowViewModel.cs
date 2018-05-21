using System;
using System.Windows.Input;
using Diploma.Infrastructure;
using Diploma.View;

namespace Diploma.ViewModel
{
	public class AdministrationWindowViewModel : ViewModelBase
	{
		#region Команда закрытия формы

		private RelayCommand _closeCommand;

		public ICommand Close => _closeCommand ?? (_closeCommand =
			                         new RelayCommand(ExecuteCloseCommand));

		public void ExecuteCloseCommand(object parameter)
		{
			try
			{
				if (parameter is AdministrationWindow temp)
				{
					temp.Close();
				}
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion
		
		#region Команда закрытия формы

		private RelayCommand _saveAndCloseCommand;

		public ICommand SaveAndClose => _saveAndCloseCommand ?? (_saveAndCloseCommand =
			                         new RelayCommand(ExecuteSaveAndCloseCommand));

		public void ExecuteSaveAndCloseCommand(object parameter)
		{
			try
			{
				if (parameter is AdministrationWindow temp)
				{
					temp.Close();
				}
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion
		protected override void OnDispose()
		{
			
		}
	}
}