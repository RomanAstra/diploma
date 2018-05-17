using System;
using System.Windows;
using System.Windows.Input;
using Diploma.Data;
using Diploma.Infrastructure;
using Diploma.View;

namespace Diploma.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private ConcreteFormula _concreteFormula;
		public ConcreteFormula ConcreteFormula
		{
			get => _concreteFormula ?? (_concreteFormula = ConcreteFormulaRepositoty.ConcreteFormula);
			set
			{
				_concreteFormula = value;
				OnPropertyChanged(nameof(ConcreteFormula));
			}
		}


		#region Выбранные параметры

		private BrandConcrete _currentCurrentBrandConcrete;
		public BrandConcrete CurrentBrandConcrete
		{
			get => _currentCurrentBrandConcrete ?? ConcreteFormula?.BrandConcreteList[0];
			set
			{
				_currentCurrentBrandConcrete = value;
				OnPropertyChanged(nameof(CurrentBrandConcrete));
			}
		}

		#endregion


		#region Commands

		private RelayCommand _calculateCommand;

		public ICommand Calculate => _calculateCommand ?? (_calculateCommand =
			                        new RelayCommand(ExecuteCalculateCommand,
										CanExecuteCalculateCommand));

		public void ExecuteCalculateCommand(object parameter)
		{
			try
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(CurrentBrandConcrete.Strength);
				messageBoxWindow.ShowDialog();
				//ConcreteFormulaRepositoty.SaveData();
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		public bool CanExecuteCalculateCommand(object parameter)
		{
			return true;
		}


		#region Команда окрытия списка со сохранеными расчетами

		private RelayCommand _openAccountCommand;

		public ICommand OpenAccount => _openAccountCommand ?? (_openAccountCommand =
			                        new RelayCommand(ExecuteOpenAccountCommand));

		public void ExecuteOpenAccountCommand(object parameter)
		{
			try
			{
				OpenAccountsWindow openAccountsWindow = new OpenAccountsWindow();
				openAccountsWindow.ShowDialog();
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion

		#endregion

		protected override void OnDispose()
		{
			// Очистка ресурсов
		}
	}
}