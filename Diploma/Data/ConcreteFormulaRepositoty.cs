using System;
using DataDB;

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

		private static readonly IData<ConcreteFormula> _data = new DataDb<ConcreteFormula>("Data");
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

		public static void AddBrandConcrete(BrandConcrete item)
		{
			ConcreteFormula.BrandConcreteList.Add(item);
		}

		internal static void AddMixtureMobility(MixtureMobility item)
		{
			ConcreteFormula.MixtureMobilityList.Add(item);
		}

		public static void AddFineAggregate(FineAggregate item)
		{
			ConcreteFormula.FineAggregateList.Add(item);
		}

		public static void AddCoarseAggregate(CoarseAggregate item)
		{
			ConcreteFormula.CoarseAggregateList.Add(item);
		}

		public static void AddCementBrand(CementBrand item)
		{
			ConcreteFormula.CementBrandList.Add(item);
		}

		public static void AddAdmixtures(Admixtures item)
		{
			ConcreteFormula.AdmixturesList.Add(item);
		}

		public static void EditBrandConcrete(BrandConcrete item, string name, string value, string description)
		{
			for (var i = 0; i < ConcreteFormula.BrandConcreteList.Count; i++)
			{
				if (ConcreteFormula.BrandConcreteList[i] == item)
				{
					ConcreteFormula.BrandConcreteList[i] = new BrandConcrete
					{
						Name = name,
						Value = value,
						Description = description
					};
				}
			}
		}

		internal static void EditMixtureMobility(MixtureMobility item, string name, string value, string description)
		{
			for (var i = 0; i < ConcreteFormula.BrandConcreteList.Count; i++)
			{
				if (ConcreteFormula.MixtureMobilityList[i] == item)
				{
					ConcreteFormula.BrandConcreteList[i] = new BrandConcrete
					{
						Name = name,
						Value = value,
						Description = description
					};
				}
			}
		}

		public static void EditFineAggregate(FineAggregate item, string name, string value, string description)
		{
			for (var i = 0; i < ConcreteFormula.BrandConcreteList.Count; i++)
			{
				if (ConcreteFormula.FineAggregateList[i] == item)
				{
					ConcreteFormula.BrandConcreteList[i] = new BrandConcrete
					{
						Name = name,
						Value = value,
						Description = description
					};
				}
			}
		}

		public static void EditCoarseAggregate(CoarseAggregate item, string name, string value, string description)
		{
			for (var i = 0; i < ConcreteFormula.BrandConcreteList.Count; i++)
			{
				if (ConcreteFormula.CoarseAggregateList[i] == item)
				{
					ConcreteFormula.BrandConcreteList[i] = new BrandConcrete
					{
						Name = name,
						Value = value,
						Description = description
					};
				}
			}
		}

		public static void EditCementBrand(CementBrand item, string name, string value, string description)
		{
			for (var i = 0; i < ConcreteFormula.BrandConcreteList.Count; i++)
			{
				if (ConcreteFormula.CementBrandList[i] == item)
				{
					ConcreteFormula.BrandConcreteList[i] = new BrandConcrete
					{
						Name = name,
						Value = value,
						Description = description
					};
				}
			}
		}

		public static void EditAdmixtures(Admixtures item, string name, string value, string description)
		{
			for (var i = 0; i < ConcreteFormula.BrandConcreteList.Count; i++)
			{
				if (ConcreteFormula.AdmixturesList[i] == item)
				{
					ConcreteFormula.BrandConcreteList[i] = new BrandConcrete
					{
						Name = name,
						Value = value,
						Description = description
					};
				}
			}
		}
	}
}