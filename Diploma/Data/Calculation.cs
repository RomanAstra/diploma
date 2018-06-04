using System;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Data
{
	[Serializable]
	public class Calculation
	{
		public string Name { get; set; }
		public DateTime DateTime { get; set; }
		public float CountConcrete { get; set; } = 1;
		public Admixtures Admixtures { get; set; }
		public BrandConcrete BrandConcrete { get; set; }
		public CementBrand CementBrand { get; set; }
		public CoarseAggregate CoarseAggregate { get; set; }
		public FineAggregate FineAggregate { get; set; }
		public MixtureMobility MixtureMobility { get; set; }
		public BrandConcreteFrostResistance BrandConcreteFrostResistance { get; set; }
		public HardeningConditions HardeningConditions { get; set; }
		public CalculationResult CalculationResult;

		private double[,] _consWater = new double[,]
		{
			{190,200},
			{165,175},
			{145,160},
			{140,150}
		};

		private double[,] _frostHardening = new double[,]
		{
			{0.6,0.55},
			{0.57,0.52},
			{0.55,0.5},
			{0.47,0.45}
		};

		private double[,] _volumeFractionOfSandInAggregates = new double[,]
		{
			{52,53},
			{41,43},
			{33,36},
			{30,32}
		};

		#region Old

		//private double[,] _consWater = new double[,]
		//{
		//    {150,135,125,120,160,150,135,130},
		//    {160,145,130,125,170,160,145,140},
		//    {165,150,135,130,175,165,150,145},
		//    {175,160,145,140,185,175,160,155},
		//    {190,175,160,155,200,190,175,170},
		//    {200,185,170,165,210,200,185,180},
		//    {205,190,175,170,215,205,190,185},
		//    {215,205,190,180,225,215,200,190},
		//    {220,210,197,185,230,220,207,195},
		//    {227,218,203,192,237,228,213,202},
		//};

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

		private void CalcGravelAndSand()
		{
			double vn = (1 - bulkDensityGravel / trueDensityGravel);
			GravelValue = 1000.0 / (1.1 * vn / 1.48 + 1 / trueDensityGravel);
			SandValue = (1000 - (CementValue / trueDensityCement) - WaterValue - (GravelValue / trueDensityGravel)) * trueDensitySand;
		}

		#endregion

		private void CalcWaterAndCementValues()
		{
			var column = CoarseAggregate.Name.Contains("Гравий") ? 0 : 1;
			if (!Int32.TryParse(CoarseAggregate.Value, out var result)) return;
			CalculationResult.WaterFlowAccordingDirections = _consWater[result, column];
			if (!Single.TryParse(MixtureMobility.Value, out var resultMixtureMobility)) return;
			CalculationResult.WaterConsumptionIncludingOK = CalculationResult.WaterFlowAccordingDirections + (resultMixtureMobility - 5) * 3;
			CalculationResult.WaterFlowWithRegardToAirContent = CalculationResult.WaterConsumptionIncludingOK - (4-2) * 3;
			if (!Single.TryParse(BrandConcrete.Value, out var brandConcrete)) return;
			if (!Double.TryParse(CementBrand.Value, out var cementBrand)) return;
			CalculationResult.QuantityOfCementByCalculation = (((brandConcrete/(0.55 * cementBrand))+0.5)*(CalculationResult.WaterFlowWithRegardToAirContent + 10 * 4)) - 22;
			CalculationResult.WCByCalculation = CalculationResult.WaterFlowWithRegardToAirContent/CalculationResult.QuantityOfCementByCalculation;

			if (!Int32.TryParse(BrandConcreteFrostResistance.Value, out var rowFrostHardening)) return;
			if (!Int32.TryParse(HardeningConditions.Value, out var columnFrostHardening)) return;
			CalculationResult.MaximumPermissibleAccordingWCToInstructions = _frostHardening[rowFrostHardening, columnFrostHardening];
			
			CalculationResult.MinimumOfTheReceivedAndAdmissible =  new List<Double>(){ CalculationResult.MaximumPermissibleAccordingWCToInstructions, CalculationResult.WCByCalculation }.Min();

			CalculationResult.CorrectedCementConsumption = CalculationResult.WaterFlowWithRegardToAirContent / CalculationResult.MinimumOfTheReceivedAndAdmissible;
			CalculationResult.VolumeOfAggregates = 1000 - (CalculationResult.CorrectedCementConsumption / 3.1) -
			                                       CalculationResult.WaterFlowWithRegardToAirContent - 10 * 4;
			CalculationResult.TheShareOfSandFromTheTotalNumberWillFill = _volumeFractionOfSandInAggregates[result, column];

			CalculationResult.TheProportionOfSandCorrectedByMk = CalculationResult.TheShareOfSandFromTheTotalNumberWillFill + (((2.26 - 2.5)/0.1)*0.5) + ((CalculationResult.MinimumOfTheReceivedAndAdmissible-0.55)/0.05);
			CalculationResult.ShareOfSandCorrectedForGravel = CalculationResult.TheProportionOfSandCorrectedByMk*(1+(4/100.0));
			CalculationResult.TheAmountOfSandDry = (CalculationResult.ShareOfSandCorrectedForGravel/100)*CalculationResult.VolumeOfAggregates*2.6;
			CalculationResult.TheAmountOfSandWet = CalculationResult.TheAmountOfSandDry * (1+(1/100.0));
			CalculationResult.NumberOfCoarseAggregate = ((100-CalculationResult.ShareOfSandCorrectedForGravel)/100)*CalculationResult.VolumeOfAggregates*2.7;


			if (!Single.TryParse(Admixtures.Value, out var resultAdmixtures)) return;
			CalculationResult.ChemicalAdditive = (resultAdmixtures * CalculationResult.CorrectedCementConsumption) / 100;
			CalculationResult.InTermsOfSolution = CalculationResult.ChemicalAdditive/(1.09*(20/100.0));
			CalculationResult.WaterConsumptionWithRegardToHumidityOfSand = CalculationResult.WaterFlowWithRegardToAirContent - (CalculationResult.TheAmountOfSandWet - CalculationResult.TheAmountOfSandDry);
		}

        public void StartCalculations()
        {
            CalcWaterAndCementValues();
        }
    }
}