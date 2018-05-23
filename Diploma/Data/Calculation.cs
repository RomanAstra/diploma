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

        //TODO пока не используется, нужны для более точного расчёта песка и щебня, пока по умолчанию - 1.1
        private double[,] _spreadingFactor = new double[,]
        {
            {0,0,0,1.26,1.32,1.38},
            {0,0,1.3,1.36,1.42,0},
            {0,1.32,1.38,1.44,0,0},
            {1.31,1.4,1.46,0,0,0},
            {1.44,1.52,1.56,0,0,0},
            {1.52,1.56,0,0,0,0},
        };

        double bulkDensityCement = 1.3;
        double bulkDensitySand = 1.5;
        double bulkDensityGravel = 1.48;
        double trueDensityCement = 3.1;
        double trueDensitySand = 2.63;
        double trueDensityGravel = 2.6;


        /// <summary>
        /// Водно-цементное соотношение
        /// </summary>
        public double WnC { get; set; }
        /// <summary>
        /// Воды на куб. метр при заданных параметрах
        /// </summary>
        public double WaterValue { get; set; }
        /// <summary>
        ///  Цемент на куб. метр при заданных параметрах
        /// </summary>
        public double CementValue { get; set; }
        /// <summary>
        ///  Расход гравия\щебня на куб. метр
        /// </summary>
        public double GravelValue { get; set; }
        /// <summary>
        ///  Расход песка на куб. метр
        /// </summary>
        public double SandValue { get; set; }

        private void CalcWnC()
		{
			if (!Double.TryParse(BrandConcrete.Value, out var brandConcrete)) throw new Exception($"Плохое значение {brandConcrete}");
			if (!Double.TryParse(CementBrand.Value, out var cementBrand)) throw new Exception($"Плохое значение {cementBrand}");
			var coefA = 0.6;
            WnC = (coefA * cementBrand) / (brandConcrete + 0.5 * coefA * cementBrand);
		}

		private void CalcWaterAndCementValues()
		{
			CalcWnC();
			if (!Int32.TryParse(MixtureMobility.Value, out var mixtureMobility)) throw new Exception($"Плохое значение {mixtureMobility}");
			if (!Int32.TryParse(CoarseAggregate.Value, out var coarseAggregate)) throw new Exception($"Плохое значение {coarseAggregate}");
            if (!Int32.TryParse(FineAggregate.Value, out var fineAggregate)) throw new Exception($"Плохое значение {fineAggregate}");
            WaterValue = _consWater[mixtureMobility, coarseAggregate] + fineAggregate;
			CementValue = WaterValue / WnC;
		}

        private void CalcGravelAndSand()
        {
            double vn = (1 - bulkDensityGravel/trueDensityGravel);
            GravelValue = 1000.0 / (1.1*vn/1.48 + 1/trueDensityGravel);
            SandValue = (1000 - (CementValue / trueDensityCement) - WaterValue - (GravelValue / trueDensityGravel)) * trueDensitySand;
        }

        public void StartCalculations()
        {
            CalcWnC();
            CalcWaterAndCementValues();
            CalcGravelAndSand();
        }
    }
}