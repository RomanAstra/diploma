using System;
using System.Windows.Input;
using Diploma.Data;
using Diploma.Infrastructure;
using Diploma.View;

namespace Diploma.ViewModel
{
	public class SetNameCalculateWindowViewModel : ViewModelBase
	{
		private string _nameCalculate;

		public string NameCalculate
		{
			get => _nameCalculate;
			set
			{
				_nameCalculate = value;
				OnPropertyChanged(nameof(NameCalculate));
			}
		}

		private RelayCommand _setNameCalculateCommand;

		public ICommand SetNameCalculate => _setNameCalculateCommand ?? (_setNameCalculateCommand =
			                             new RelayCommand(ExecuteSetNameCalculateCommand,
				                             CanExecuteSetNameCalculateCommand));

		public void ExecuteSetNameCalculateCommand(object parameter)
		{
			try
			{
				MainWindowViewModel.Instance.SetNameCalculation(NameCalculate);
				if (parameter is SetNameCalculateWindow temp)
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

		public bool CanExecuteSetNameCalculateCommand(object parameter)
		{
			return !String.IsNullOrWhiteSpace(NameCalculate);
		}

		protected override void OnDispose()
		{

		}
	}
}