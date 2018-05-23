using System.Collections.ObjectModel;
using System.Linq;
using DataDB;

namespace Diploma.Data
{
	public static class CalculationListRepositoty
	{
		private static ObservableCollection<Calculation> _concreteFormula;

		public static ObservableCollection<Calculation> ConcreteFormula
		{
			get => _concreteFormula
			       ?? (_concreteFormula = new ObservableCollection<Calculation>());

			set => _concreteFormula = value;
		}

		private static readonly IData<Calculation[]> _data = new DataDb<Calculation[]>("Calculations");
		static CalculationListRepositoty()
		{
			var tempList = _data?.Load();

			if (tempList == null) return;

			foreach (var calculation in tempList)
			{
				ConcreteFormula.Add(calculation);
			}
		}

		public static void SaveData()
		{
			_data?.Save(ConcreteFormula.ToArray());
		}

		public static void AddItem(Calculation calculation)
		{
			ConcreteFormula.Add(calculation);
		}

		public static void RemoveItem(Calculation calculation)
		{
			ConcreteFormula.Remove(calculation);
		}
	}
}