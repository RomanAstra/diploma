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
	        Calculation = new Calculation
	        {
		        Admixtures = ConcreteFormula?.AdmixturesList[0],
		        BrandConcrete = ConcreteFormula?.BrandConcreteList[0],
		        CementBrand = ConcreteFormula?.CementBrandList[0],
		        CoarseAggregate = ConcreteFormula?.CoarseAggregateList[0],
		        FineAggregate = ConcreteFormula?.FineAggregateList[0],
		        MixtureMobility = ConcreteFormula?.MixtureMobilityList[0]
	        };
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

		private Calculation _calculation;
		public Calculation Calculation
		{
			get => _calculation;
			set
			{
				_calculation = value;
				OnPropertyChanged(nameof(Calculation));
			}
		}


        #region Commands

        private RelayCommand _calculateCommand;

		public ICommand Calculate => _calculateCommand ?? (_calculateCommand =
			                        new RelayCommand(ExecuteCalculateCommand,
										CanExecuteCalculateCommand));

		public void ExecuteCalculateCommand(object parameter)
		{
			try
			{
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow(Calculation.BrandConcrete.Strength);
                messageBoxWindow.ShowDialog();
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

		private RelayCommand _openCalculateCommand;

		public ICommand OpenCalculate => _openCalculateCommand ?? (_openCalculateCommand =
			                        new RelayCommand(ExecuteOpenCalculateCommand));

		public void ExecuteOpenCalculateCommand(object parameter)
		{
			try
			{
				OpenCalculationsWindow openCalculationsWindow = new OpenCalculationsWindow();
				openCalculationsWindow.ShowDialog();
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion	

		#region Команда сохранения расчета

		private RelayCommand _saveCalculationCommand;

		public ICommand SaveCalculation => _saveCalculationCommand ?? (_saveCalculationCommand =
			                               new RelayCommand(ExecuteSaveCalculationCommand));

		public void ExecuteSaveCalculationCommand(object parameter)
		{
			try
			{
				Calculation.Name = "Test";
				Calculation.DateTime = DateTime.Now;
				CalculationListRepositoty.AddItem(Calculation);
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

        #endregion

        #region Команда показа теории
        private RelayCommand _showTheoryCommand;

        public ICommand ShowTheory => _showTheoryCommand ?? (_showTheoryCommand =
                                           new RelayCommand(ShowTheoryCommand));

        public void ShowTheoryCommand(object parameter)
        {
            try
            {
				HelpWindow helpWindow = new HelpWindow();
                helpWindow.Show();
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