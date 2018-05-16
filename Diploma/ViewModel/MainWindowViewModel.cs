using System;
using System.Windows;
using System.Windows.Input;
using Diploma.Infrastructure;

namespace Diploma.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private RelayCommand _testCommand;
		public ICommand Test => _testCommand ?? (_testCommand =
			                                new RelayCommand(ExecuteCommand,
				                                CanExecuteCommand));

		public void ExecuteCommand(object parameter)
		{
			try
			{
				MessageBox.Show("Test");
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
		public bool CanExecuteCommand(object parameter) => true;

		protected override void OnDispose()
		{
			// Очистка ресурсов
		}
	}
}