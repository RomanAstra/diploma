using System;
using Diploma.DataSave;

namespace Diploma.Data
{
	public static class ConcreteFormulaRepositoty
	{
		private static ConcreteFormula _concreteFormula;

		public static ConcreteFormula ConcreteFormula
		{
			get => _concreteFormula
			       ?? (_concreteFormula = new ConcreteFormula());

			set => _concreteFormula = value;
		}

		private static readonly IData<ConcreteFormula> _data = new DataXML<ConcreteFormula>();
		static ConcreteFormulaRepositoty()
		{
			ConcreteFormula = _data?.Load();
		}

		public static void SaveData()
		{
			Console.WriteLine(1);
			_data?.Save(ConcreteFormula);
		}
	}
}