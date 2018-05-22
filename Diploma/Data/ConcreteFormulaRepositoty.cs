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

		private static readonly IData<ConcreteFormula> _data = new DataXML<ConcreteFormula>("Data");
		static ConcreteFormulaRepositoty()
		{
			ConcreteFormula = _data?.Load();
		}

		public static void SaveData()
		{
			_data?.Save(ConcreteFormula);
		}

		public static void DeleteBrandConcrete(BrandConcrete item)
		{
			ConcreteFormula.BrandConcreteList.Remove(item);
		}

		internal static void DeleteMixtureMobility(MixtureMobility item)
		{
			ConcreteFormula.MixtureMobilityList.Remove(item);
		}

		public static void DeleteFineAggregate(FineAggregate item)
		{
			ConcreteFormula.FineAggregateList.Remove(item);
		}

		public static void DeleteCoarseAggregate(CoarseAggregate item)
		{
			ConcreteFormula.CoarseAggregateList.Remove(item);
		}

		public static void DeleteCementBrand(CementBrand item)
		{
			ConcreteFormula.CementBrandList.Remove(item);
		}

		public static void DeleteAdmixtures(Admixtures item)
		{
			ConcreteFormula.AdmixturesList.Remove(item);
		}
	}
}