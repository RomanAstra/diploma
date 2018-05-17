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
            //TODO временно вернул, для теста
            _concreteFormula = new ConcreteFormula();
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
			get => _currentCurrentBrandConcrete ?? ConcreteFormula?.BrandConcreteList[0];
			set
			{
				_currentCurrentBrandConcrete = value;
				OnPropertyChanged(nameof(CurrentBrandConcrete));
			}
		}

        private CoarseAggregate _currentCoarseAggregate;
        public CoarseAggregate CurrentCoarseAggregate
        {
            get => _currentCoarseAggregate ?? ConcreteFormula?.CoarseAggregateList[0];
            set
            {
                _currentCoarseAggregate = value;
                OnPropertyChanged(nameof(CurrentCoarseAggregate));
            }
        }

        private FineAggregate _currentFineAggregate;
        public FineAggregate CurrentFineAggregate
        {
            get => _currentFineAggregate ?? ConcreteFormula?.FineAggregateList[0];
            set
            {
                _currentFineAggregate = value;
                OnPropertyChanged(nameof(CurrentFineAggregate));
            }
        }

        private CementBrand _currentCementBrand;
        public CementBrand CurrentCementBrand
        {
            get => _currentCementBrand ?? ConcreteFormula?.CementBrandList[0];
            set
            {
                _currentCementBrand = value;
                OnPropertyChanged(nameof(CurrentCementBrand));
            }
        }

        private MixtureMobility _currentMixtureMobility;
        public MixtureMobility CurrentMixtureMobility
        {
            get => _currentMixtureMobility ?? ConcreteFormula?.MixtureMobilityList[0];
            set
            {
                _currentMixtureMobility = value;
                OnPropertyChanged(nameof(CurrentMixtureMobility));
            }
        }

        private Admixtures _currentAdmixtures;
        public Admixtures CurrentAdmixtures
        {
            get => _currentAdmixtures ?? ConcreteFormula?.AdmixturesList[0];
            set
            {
                _currentAdmixtures = value;
                OnPropertyChanged(nameof(CurrentAdmixtures));
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