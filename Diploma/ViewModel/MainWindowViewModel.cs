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
		public MainWindowViewModel()
		{
			//_concreteFormula = new ConcreteFormula();
			CurrentBrandConcrete = _concreteFormula?.BrandConcreteList[0];
		}

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
			get => _currentCurrentBrandConcrete;
			set
			{
				_currentCurrentBrandConcrete = value;
				OnPropertyChanged(nameof(CurrentBrandConcrete));
			}
		}

		#endregion


		#region Commands

		private RelayCommand _testCommand;

		public ICommand Test => _testCommand ?? (_testCommand =
			                        new RelayCommand(ExecuteCommand,
				                        CanExecuteCommand));

		public void ExecuteCommand(object parameter)
		{
			try
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(CurrentBrandConcrete.Strength);
				messageBoxWindow.ShowDialog();
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
			ConcreteFormulaRepositoty.SaveData();
		}
		public bool CanExecuteCommand(object parameter)
		{
			return CurrentBrandConcrete != null;
		}

		#endregion

		protected override void OnDispose()
		{
			// Очистка ресурсов
		}
	}
}