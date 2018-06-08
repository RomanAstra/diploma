using System;
using System.IO;
using System.Windows.Media;
using Diploma.Data;
using Xceed.Words.NET;

namespace Diploma.Export
{
	public class WordExport
	{
		private DocX _document;
		private FontFamily _fontFamily = new FontFamily("Times New Roman");
		private double _fontSizeText = 12;
		private double _fontSizeTitle = 14;
		private double _spacing = 1.5;
		private string _filename;

		public WordExport()
		{
			_filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
				"Test.doc");
				

			try
			{
				using (DocX document = DocX.Create(_filename))
				{
					document.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(@"Произошла ошибка при создании документа. Подробнее: " + e.Message);
			}
		}

		public void Export(CalculationResult result)
		{
			using (DocX document = DocX.Load(_filename))
			{
				_document = document;

				_document.MarginLeft = 85; // 3 cm
				_document.MarginRight = 28.3f;// 1 cm
				_document.MarginTop = 28.3f;
				_document.MarginBottom = 28.3f;
				AddTitle("Отчет по результатам произведенных расчетов.");

				AddDetails(result);
				document.Save();
			}
		}

		private void AddDetails(CalculationResult result)
		{
			Paragraph details = CreateParagraph();
			
			var value = $@"Полученный расход воды по Указаниям: {result.WaterFlowAccordingDirections} л.";
			AddTextLineToParagraph(details, value);

			value = $@"Расход воды с учетом ОК: {result.WaterConsumptionIncludingOK} л.";
			AddTextLineToParagraph(details, value);

			value = $@"Расход воды с учетом Воздухосодержания: {result.WaterFlowWithRegardToAirContent} л.";
			AddTextLineToParagraph(details, value);

			value = $@"Расход воды с учетом влажности песка: {result.WaterConsumptionWithRegardToHumidityOfSand} л.";
			AddTextLineToParagraph(details, value);

			value = $@"Количество цемента по расчету: {result.QuantityOfCementByCalculation} кг.";
			AddTextLineToParagraph(details, value);

			value = $@"Полученное В/Ц из расчета: {result.WCByCalculation}";
			AddTextLineToParagraph(details, value);

			value = $@"Максимально допустимое В/Ц по Указаниям: {result.MaximumPermissibleAccordingWCToInstructions}";
			AddTextLineToParagraph(details, value);

			value = $@"В/Ц принимается минимальное из полученного и допустимого: {result.MinimumOfTheReceivedAndAdmissible}";
			AddTextLineToParagraph(details, value);

			value = $@"Откорректированный расход цемента: {result.CorrectedCementConsumption} кг.";
			AddTextLineToParagraph(details, value);

			value = $@"Объем заполнителей: {result.VolumeOfAggregates} кг.";
			AddTextLineToParagraph(details, value);

			value = $@"Доля песка от общего кол-ва заполнит: {result.TheShareOfSandFromTheTotalNumberWillFill} %";
			AddTextLineToParagraph(details, value);

			value = $@"Доля песка откорректированная  по Мк и В/Ц: {result.TheProportionOfSandCorrectedByMk} %";
			AddTextLineToParagraph(details, value);

			value = $@"Доля песка откорректированная по крупному заполнителю: {result.ShareOfSandCorrectedForGravel} %";
			AddTextLineToParagraph(details, value);

			value = $@"Количество песка(сухого): {result.TheAmountOfSandDry} кг.";
			AddTextLineToParagraph(details, value);

			value = $@"Количество песка(влажного): {result.TheAmountOfSandWet} кг.";
			AddTextLineToParagraph(details, value);

			value = $@"Количество крупного заполнителя: {result.NumberOfCoarseAggregate} кг.";
			AddTextLineToParagraph(details, value);

			value = $@"Химическая добавка: {result.ChemicalAdditive} кг.";
			AddTextLineToParagraph(details, value);

			value = $@"В пересчете на 20% раствор: {result.InTermsOfSolution} л.";
			AddTextLineToParagraph(details, value);

			//value = $@"++++++++++++++++++++++++++++++++++++++++++++++++++++";
			//AddParagraph(value);

			//value = $@"Расчетный состав для {MainWindowViewModel.Instance.Calculation.CountConcrete}";
			//AddParagraph(value);

			//value = $@"Цемент = {calculationResult.CorrectedCementConsumption * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			//AddParagraph(value);

			//value = $@"Вода = {calculationResult.WaterFlowWithRegardToAirContent * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			//AddParagraph(value);

			//value = $@"Песок = {calculationResult.TheAmountOfSandDry * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			//AddParagraph(value);

			//value = $@"Крупный заполнитель = {calculationResult.NumberOfCoarseAggregate * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			//AddParagraph(value);

			//value = $@"Химическая добавка { MainWindowViewModel.Instance.Calculation.Admixtures.Name} {MainWindowViewModel.Instance.Calculation.Admixtures.Value} % от массы цемента = {calculationResult.ChemicalAdditive * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			//AddParagraph(value);
		}

		private void PageBreak()
		{
			if (_document == null) return;
			Paragraph pageBreak = _document.InsertParagraph();
			pageBreak.InsertPageBreakAfterSelf();
		}

		private void AddTitle(string vale)
		{
			if (_document == null) return;
			Paragraph title = _document.InsertParagraph();
			title.Append(vale).Font(_fontFamily.Source).FontSize(_fontSizeTitle).Bold().Alignment = Alignment.center;
		}

		private void AddTextLineToParagraph(Paragraph paragraph, string vale)
		{
			if (_document == null) return;
			paragraph.AppendLine(vale).Font(_fontFamily.Source).FontSize(_fontSizeText);
		}

		private void AddTextAppendToParagraph(Paragraph paragraph, string vale)
		{
			if (_document == null) return;
			paragraph.Append(vale).Font(_fontFamily.Source).FontSize(_fontSizeText);
		}

		private Paragraph CreateParagraph()
		{
			Paragraph paragraph = _document.InsertParagraph();
			paragraph.Spacing(_spacing);
			return paragraph;
		}
	}
}