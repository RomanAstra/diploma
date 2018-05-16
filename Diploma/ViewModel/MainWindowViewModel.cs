using System;
using System.Windows;
using System.Windows.Input;
using Diploma.Infrastructure;
using Diploma.View;

namespace Diploma.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private string _test;
		public string TestStr
		{
			get => _test;
			set
			{
				_test = value;
				OnPropertyChanged(nameof(TestStr));
			}
		}

		private RelayCommand _testCommand;
		public ICommand Test => _testCommand ?? (_testCommand =
			                                new RelayCommand(ExecuteCommand,
				                                CanExecuteCommand));

		public void ExecuteCommand(object parameter)
		{
			try
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow("Test");
				messageBoxWindow.ShowDialog();
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}
		public bool CanExecuteCommand(object parameter) => !String.IsNullOrEmpty(TestStr);

		protected override void OnDispose()
		{
			// Очистка ресурсов
		}
	}
}