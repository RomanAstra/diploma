using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Diploma.Data;
using Diploma.Helper;
using Diploma.Infrastructure;
using Diploma.View;

namespace Diploma.ViewModel
{
	public class OpenCalculationsViewModel : ViewModelBase
	{
		private ObservableCollection<Calculation> _calculation;
		public ObservableCollection<Calculation> Calculation
		{
			get => _calculation ?? (_calculation = CalculationListRepositoty.ConcreteFormula);
			set
			{
				_calculation = value;
				OnPropertyChanged(nameof(Calculation));
			}
		}

		private Calculation _currentCalculation;
		public Calculation CurrentCalculation
		{
			get => _currentCalculation;
			set
			{
				_currentCalculation = value;
				OnPropertyChanged(nameof(CurrentCalculation));
			}
		}


		#region Команда выбора элемента списка

		private RelayCommand _selectioCommand;

		public ICommand SelectioCommand => _selectioCommand
		                                   ?? (_selectioCommand = new RelayCommand(ExecuteSelectioCommand));

		private void ExecuteSelectioCommand(object o)
		{
			var temp = o as Calculation;
			try
			{
				CurrentCalculation = temp;
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion

		#region Команда передачи сохраненного расчета

		private RelayCommand _openCommand;

		public ICommand Open => _openCommand ?? (_openCommand =
			                        new RelayCommand(ExecuteOpenCommand,
				                        CanExecuteOpenCommand));

		public void ExecuteOpenCommand(object parameter)
		{
			try
			{
				MainWindowViewModel.Instance.SetCalculation(CurrentCalculation);
				if (parameter is OpenCalculationsWindow temp)
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

		public bool CanExecuteOpenCommand(object parameter)
		{
			return CurrentCalculation != null;
		}

		#endregion

		#region Команда закрытия формы

		private RelayCommand _closeCommand;

		public ICommand Close => _closeCommand ?? (_closeCommand =
			                         new RelayCommand(ExecuteCloseCommand));

		public void ExecuteCloseCommand(object parameter)
		{
			try
			{
				if (parameter is OpenCalculationsWindow temp)
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

		#region Команда Удаление элемента

		private RelayCommand _deleteCommand;

		public ICommand Delete => _deleteCommand ?? (_deleteCommand =
			                         new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand));

		public void ExecuteDeleteCommand(object parameter)
		{
			try
			{
				var isContains = Calculation.Contains(CurrentCalculation);
				if (!isContains) return;
				Calculation.Remove(CurrentCalculation);
				CalculationListRepositoty.RemoveItem(CurrentCalculation);
				CalculationListRepositoty.SaveData();
				CurrentCalculation = null;
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		public bool CanExecuteDeleteCommand(object parameter)
		{
			return CurrentCalculation != null;
		}

		#endregion
		protected override void OnDispose()
		{

		}
	}
}