using System;

namespace Diploma.Data
{
	[Serializable]
	public struct CalculationResult
	{
		/// <summary>
		/// Полученный расход воды по Указаниям
		/// </summary>
		public double WaterFlowAccordingDirections;
		/// <summary>
		/// Расход воды с учетом ОК
		/// </summary>
		public double WaterConsumptionIncludingOK;
		/// <summary>
		/// Расход воды с учетом Воздухосодержания
		/// </summary>
		public double WaterFlowWithRegardToAirContent;
		/// <summary>
		/// Расход воды с учетом влажности песка
		/// </summary>
		public double WaterConsumptionWithRegardToHumidityOfSand;
		/// <summary>
		/// Количество цемента по расчету
		/// </summary>
		public double QuantityOfCementByCalculation;
		/// <summary>
		/// Полученное В/Ц из расчета 
		/// </summary>
		public double WCByCalculation;
		/// <summary>
		/// В/Ц принимается минимальное из полученного и допустимого 
		/// </summary>
		public double MinimumOfTheReceivedAndAdmissible;
		/// <summary>
		/// Максимально допустимое В/Ц по "Указаниям"
		/// </summary>
		public double MaximumPermissibleAccordingWCToInstructions;
		/// <summary>
		/// Откорректированный расход цемента
		/// </summary>
		public double CorrectedCementConsumption;
		/// <summary>
		/// Объем заполнителей
		/// </summary>
		public double VolumeOfAggregates;
		/// <summary>
		/// Доля песка от общего кол-ва заполнит
		/// </summary>
		public double TheShareOfSandFromTheTotalNumberWillFill;
		/// <summary>
		/// Доля песка откорректированная  по Мк и В/Ц
		/// </summary>
		public double TheProportionOfSandCorrectedByMk;
		/// <summary>
		/// Доля песка откорректированная по гравию
		/// </summary>
		public double ShareOfSandCorrectedForGravel;
		/// <summary>
		/// Количество песка(сухого)
		/// </summary>
		public double TheAmountOfSandDry;
		/// <summary>
		/// Количество песка(Влажного)
		/// </summary>
		public double TheAmountOfSandWet;
		/// <summary>
		/// Количество крупного заполнителя
		/// </summary>
		public double NumberOfCoarseAggregate;
		/// <summary>
		/// Химическая добавка
		/// </summary>
		public double ChemicalAdditive;
		/// <summary>
		/// В пересчете на 20% раствор
		/// </summary>
		public double InTermsOfSolution;
	}
}