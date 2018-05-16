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
			_concreteFormula = new ConcreteFormula();
			BrandConcrete = _concreteFormula.BrandConcreteList[0];
		}

		private ConcreteFormula _concreteFormula;
		public ConcreteFormula ConcreteFormula
		{
			get => _concreteFormula;
			set
			{
				_concreteFormula = value;
				OnPropertyChanged(nameof(ConcreteFormula));
			}
		}

		private BrandConcrete _brandConcrete;
		public BrandConcrete BrandConcrete
		{
			get => _brandConcrete;
			set
			{
				_brandConcrete = value;
				OnPropertyChanged(nameof(BrandConcrete));
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
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(BrandConcrete.Strength);
				messageBoxWindow.ShowDialog();
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}
		public bool CanExecuteCommand(object parameter)
		{
			return BrandConcrete != null;
		}

		protected override void OnDispose()
		{
			// Очистка ресурсов
		}
	}
}