using System.Collections.ObjectModel;
using Diploma.Data;
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
		protected override void OnDispose()
		{

		}
	}
}