using System.Windows;
using System.Windows.Documents;
using Diploma.Export;
using Diploma.ViewModel;

namespace Diploma
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			MainRichTextBox.Document.Blocks.Clear();
			var calculationResult = MainWindowViewModel.Instance.Calculation.CalculationResult;

			var value = $@"Полученный расход воды по Указаниям: {calculationResult.WaterFlowAccordingDirections}";
			AddParagraph(value);

			value = $@"Расход воды с учетом ОК: {calculationResult.WaterConsumptionIncludingOK}";
			AddParagraph(value);

			value = $@"Расход воды с учетом Воздухосодержания: {calculationResult.WaterFlowWithRegardToAirContent}";
			AddParagraph(value);

			value = $@"Расход воды с учетом влажности песка: {calculationResult.WaterConsumptionWithRegardToHumidityOfSand}";
			AddParagraph(value);

			value = $@"Количество цемента по расчету: {calculationResult.QuantityOfCementByCalculation}";
			AddParagraph(value);

			value = $@"Полученное В/Ц из расчета: {calculationResult.WCByCalculation}";
			AddParagraph(value);

			value = $@"Максимально допустимое В/Ц по Указаниям: {calculationResult.MaximumPermissibleAccordingWCToInstructions}";
			AddParagraph(value);

			value = $@"В/Ц принимается минимальное из полученного и допустимого: {calculationResult.MinimumOfTheReceivedAndAdmissible}";
			AddParagraph(value);

			value = $@"Откорректированный расход цемента: {calculationResult.CorrectedCementConsumption}";
			AddParagraph(value);

			value = $@"Объем заполнителей: {calculationResult.VolumeOfAggregates}";
			AddParagraph(value);

			value = $@"Доля песка от общего кол-ва заполнит: {calculationResult.TheShareOfSandFromTheTotalNumberWillFill} %";
			AddParagraph(value);

			value = $@"Доля песка откорректированная  по Мк и В/Ц: {calculationResult.TheProportionOfSandCorrectedByMk} %";
			AddParagraph(value);

			value = $@"Доля песка откорректированная по крупному заполнителю: {calculationResult.ShareOfSandCorrectedForGravel} %";
			AddParagraph(value);

			value = $@"Количество песка(сухого): {calculationResult.TheAmountOfSandDry}";
			AddParagraph(value);

			value = $@"Количество песка(влажного): {calculationResult.TheAmountOfSandWet}";
			AddParagraph(value);

			value = $@"Количество крупного заполнителя: {calculationResult.NumberOfCoarseAggregate}";
			AddParagraph(value);

			value = $@"Химическая добавка: {calculationResult.ChemicalAdditive} кг.";
			AddParagraph(value);

			value = $@"В пересчете на 20% раствор: {calculationResult.InTermsOfSolution} л.";
			AddParagraph(value);

			value = $@"++++++++++++++++++++++++++++++++++++++++++++++++++++";
			AddParagraph(value);

			value = $@"Расчетный состав для {MainWindowViewModel.Instance.Calculation.CountConcrete}";
			AddParagraph(value);

			value = $@"Цемент = {calculationResult.CorrectedCementConsumption * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			AddParagraph(value);

			value = $@"Вода = {calculationResult.WaterFlowWithRegardToAirContent * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			AddParagraph(value);

			value = $@"Песок = {calculationResult.TheAmountOfSandDry * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			AddParagraph(value);

			value = $@"Крупный заполнитель = {calculationResult.NumberOfCoarseAggregate * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			AddParagraph(value);

			value = $@"Химическая добавка { MainWindowViewModel.Instance.Calculation.Admixtures.Name} {MainWindowViewModel.Instance.Calculation.Admixtures.Value} % от массы цемента = {calculationResult.ChemicalAdditive * MainWindowViewModel.Instance.Calculation.CountConcrete}";
			AddParagraph(value);
		}

		private void AddParagraph(string str)
		{
			var paragraph = new Paragraph();
			paragraph.Inlines.Add(new Run(str));
			MainRichTextBox.Document.Blocks.Add(paragraph);
		}

		private void ButtonExport_OnClick(object sender, RoutedEventArgs e)
		{
			//var export = new WordExport();
			//export.Export();
		}
	}
}
