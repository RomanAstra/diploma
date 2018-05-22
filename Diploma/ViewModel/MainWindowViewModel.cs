using System;
using System.Windows;
using System.Windows.Input;
using Diploma.Data;
using Diploma.Helper;
using Diploma.Infrastructure;
using Diploma.View;

namespace Diploma.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		public static MainWindowViewModel Instance { get; private set; }
		private string _nameCalculate;

		private bool _isShowCalculation;
		public bool IsShowCalculation
		{
			get => _isShowCalculation;
			set
			{
				_isShowCalculation = value;
				OnPropertyChanged(nameof(IsShowCalculation));
			}
		}
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
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(Calculation.BrandConcrete.Name);
				messageBoxWindow.ShowDialog();
				IsShowCalculation = true;
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

		#region Команда создания нового расчета

		private RelayCommand _newCalculateCommand;

		public ICommand NewCalculate => _newCalculateCommand ?? (_newCalculateCommand =
			                                 new RelayCommand(ExecuteNewCalculateCommand));

		public void ExecuteNewCalculateCommand(object parameter)
		{
			try
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
				IsShowCalculation = false;
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
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

		/// <summary>
		/// Проверка результата авторизации
		/// </summary>
		/// <param name="logInResult"></param>
		public void SetLogInResult(bool logInResult)
        {
			if (logInResult)
			{
				AdministrationWindow administrationWindow = new AdministrationWindow();
				administrationWindow.ShowDialog();
			}
			else
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow("Ошибка входа");
				messageBoxWindow.ShowDialog();
			}
		}

        protected override void OnDispose()
		{
			// Очистка ресурсов
		}
	}
}