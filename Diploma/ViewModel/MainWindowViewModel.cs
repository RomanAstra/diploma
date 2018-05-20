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
		public static MainWindowViewModel Instance { get; private set; }
		private string _nameCalculate;
		public MainWindowViewModel()
		{
			Instance = this;
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

		#region Команда расчета бетонной смеси

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
			return IsCompletedForm();
		}

		#endregion


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
			                               new RelayCommand(ExecuteSaveCalculationCommand,
				                               CanExecuteSaveCalculationCommand));

		public void ExecuteSaveCalculationCommand(object parameter)
		{
			try
			{
				SetNameCalculateWindow nameCalculateWindow = new SetNameCalculateWindow();
				nameCalculateWindow.ShowDialog();
				if (!String.IsNullOrEmpty(_nameCalculate))
				{
					Calculation.Name = _nameCalculate;
					Calculation.DateTime = DateTime.Now;
					CalculationListRepositoty.AddItem(Calculation);
					CalculationListRepositoty.SaveData();
				}
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		public bool CanExecuteSaveCalculationCommand(object parameter)
		{
			return IsCompletedForm();
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

        #region Команда авторизации
        private RelayCommand _logInCommand;

        public ICommand LogIn => _logInCommand ?? (_logInCommand =
                                           new RelayCommand(LogInCommand));

        public void LogInCommand(object parameter)
        {
            try
            {
                LogInWindow logInWindow = new LogInWindow();
                logInWindow.ShowDialog();
                if (logInWindow.DialogResult.HasValue && logInWindow.DialogResult.Value)
                    MessageBox.Show("успешно!");
                else
                    MessageBox.Show("всё хуйня");
            }
            catch (Exception e)
            {
                MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
                messageBoxWindow.ShowDialog();
            }
        }
        #endregion

        #endregion

		/// <summary>
		/// Проверяет хорошо ли заполнена форма
		/// </summary>
		/// <returns></returns>
		private bool IsCompletedForm()
		{
			return Calculation.BrandConcrete != null;
		}

		/// <summary>
		/// Задает текущий расчет
		/// </summary>
		/// <param name="calculation"></param>
		public void SetCalculation(Calculation calculation)
		{
			Calculation = calculation;
		}

		/// <summary>
		/// Задает имя расчета для сохранения
		/// </summary>
		/// <param name="nameCalculate"></param>
		public void SetNameCalculation(string nameCalculate)
		{
			_nameCalculate = nameCalculate;
		}

		protected override void OnDispose()
		{
			// Очистка ресурсов
		}
	}
}