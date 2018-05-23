using System;

namespace Diploma.Data
{
	[Serializable]
	public class Calculation
	{
		public string Name { get; set; }
		public DateTime DateTime { get; set; }
		public Admixtures Admixtures { get; set; }
		public BrandConcrete BrandConcrete { get; set; }
		public CementBrand CementBrand { get; set; }
		public CoarseAggregate CoarseAggregate { get; set; }
		public FineAggregate FineAggregate { get; set; }
		public MixtureMobility MixtureMobility { get; set; }

		private double[,] _consWater = new double[,]
		{
			{150,135,125,120,160,150,135,130},
			{160,145,130,125,170,160,145,140},
			{165,150,135,130,175,165,150,145},
			{175,160,145,140,185,175,160,155},
			{190,175,160,155,200,190,175,170},
			{200,185,170,165,210,200,185,180},
			{205,190,175,170,215,205,190,185},
			{215,205,190,180,225,215,200,190},
			{220,210,197,185,230,220,207,195},
			{227,218,203,192,237,228,213,202},
		};

		public double VDelC()
		{
			if (!Double.TryParse(BrandConcrete.Value, out var brandConcrete)) throw new Exception($"Плохое значение {brandConcrete}");
			if (!Double.TryParse(CementBrand.Value, out var cementBrand)) throw new Exception($"Плохое значение {cementBrand}");
			var coefA = 0.6;
			return (coefA * cementBrand) / (brandConcrete + 0.5 * coefA * cementBrand);
		}

		public double WaterAndCementValues()
		{
			double wсСoefficient = VDelC();
			if (!Int32.TryParse(MixtureMobility.Value, out var mixtureMobility)) throw new Exception($"Плохое значение {mixtureMobility}");
			if (!Int32.TryParse(CoarseAggregate.Value, out var coarseAggregate)) throw new Exception($"Плохое значение {coarseAggregate}");
			double waterValue = _consWater[mixtureMobility, coarseAggregate];
			return waterValue / wсСoefficient;
		}
	}
}