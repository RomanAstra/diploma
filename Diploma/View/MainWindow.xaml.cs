using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using Diploma.Export;
using Diploma.ViewModel;
using System.Net;
using System.IO;
using System.Windows.Media;
using System.Windows.Threading;
using Diploma.Data;
using Diploma.View;

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
			MainWindowViewModel.Instance.CalculationEvent += Calc;
			MainWindowViewModel.Instance.UpdateComboBoxEvent += UpdateComboBox;

			showColumnChart();
			
		}
		ObservableCollection<KeyValuePair<double, double>> Power = new ObservableCollection<KeyValuePair<double, double>>();
		
		private void showColumnChart()
		{
			lineChart.DataContext = Power;
			Power.Add(new KeyValuePair<double, double>(3, 45));
			Power.Add(new KeyValuePair<double, double>(2, 100));
			Power.Add(new KeyValuePair<double, double>(1, 30));
			Power.Add(new KeyValuePair<double, double>(4, 3));
		}

		private void Calc(object sender, System.EventArgs eventArgs)
		{
			MainRichTextBox.Document.Blocks.Clear();
			var calculationResult = MainWindowViewModel.Instance.Calculation.CalculationResult;

			var value = $@"Полученный расход воды по Указаниям: {calculationResult.WaterFlowAccordingDirections: .##} л.";
			AddParagraph(value);

			value = $@"Расход воды с учетом ОК: {calculationResult.WaterConsumptionIncludingOK: .##} л.";
			AddParagraph(value);

			value = $@"Расход воды с учетом Воздухосодержания: {calculationResult.WaterFlowWithRegardToAirContent: .##} л.";
			AddParagraph(value);

			value = $@"Расход воды с учетом влажности песка: {calculationResult.WaterConsumptionWithRegardToHumidityOfSand: .##} л.";
			AddParagraph(value);

			value = $@"Количество цемента по расчету: {calculationResult.QuantityOfCementByCalculation: .##} кг.";
			AddParagraph(value);

			value = $@"Полученное В/Ц из расчета: {calculationResult.WCByCalculation: .##}";
			AddParagraph(value);

			value = $@"Максимально допустимое В/Ц по Указаниям: {calculationResult.MaximumPermissibleAccordingWCToInstructions: .##}";
			AddParagraph(value);

			value = $@"В/Ц принимается минимальное из полученного и допустимого: {calculationResult.MinimumOfTheReceivedAndAdmissible: .##}";
			AddParagraph(value);

			value = $@"Откорректированный расход цемента: {calculationResult.CorrectedCementConsumption: .##} кг.";
			AddParagraph(value);

			value = $@"Объем заполнителей: {calculationResult.VolumeOfAggregates: .##} кг.";
			AddParagraph(value);

			value = $@"Доля песка от общего кол-ва заполнит: {calculationResult.TheShareOfSandFromTheTotalNumberWillFill: .##} %";
			AddParagraph(value);

			value = $@"Доля песка откорректированная  по Мк и В/Ц: {calculationResult.TheProportionOfSandCorrectedByMk: .##} %";
			AddParagraph(value);

			value = $@"Доля песка откорректированная по крупному заполнителю: {calculationResult.ShareOfSandCorrectedForGravel: .##} %";
			AddParagraph(value);

			value = $@"Количество песка(сухого): {calculationResult.TheAmountOfSandDry: .##} кг.";
			AddParagraph(value);

			value = $@"Количество песка(влажного): {calculationResult.TheAmountOfSandWet: .##} кг.";
			AddParagraph(value);

			value = $@"Количество крупного заполнителя: {calculationResult.NumberOfCoarseAggregate: .##} кг.";
			AddParagraph(value);

			value = $@"Химическая добавка: {calculationResult.ChemicalAdditive: .##} кг.";
			AddParagraph(value);

			value = $@"В пересчете на 20% раствор: {calculationResult.InTermsOfSolution: .##} л.";
			AddParagraph(value);

			value = $@"++++++++++++++++++++++++++++++++++++++++++++++++++++";
			AddParagraph(value);

			value = $@"Расчетный состав для {MainWindowViewModel.Instance.Calculation.CountConcrete: .##} м³";
			AddParagraph(value);

			value = $@"Цемент = {calculationResult.CorrectedCementConsumption * MainWindowViewModel.Instance.Calculation.CountConcrete: .##} кг.";
			AddParagraph(value);

			value = $@"Вода = {calculationResult.WaterFlowWithRegardToAirContent * MainWindowViewModel.Instance.Calculation.CountConcrete: .##} л.";
			AddParagraph(value);

			value = $@"Песок = {calculationResult.TheAmountOfSandDry * MainWindowViewModel.Instance.Calculation.CountConcrete: .##} кг.";
			AddParagraph(value);

			value = $@"Крупный заполнитель = {calculationResult.NumberOfCoarseAggregate * MainWindowViewModel.Instance.Calculation.CountConcrete: .##}  кг.";
			AddParagraph(value);

			value = $@"Химическая добавка { MainWindowViewModel.Instance.Calculation.Admixtures.Name} {MainWindowViewModel.Instance.Calculation.Admixtures.Value} % от массы цемента = {calculationResult.ChemicalAdditive * MainWindowViewModel.Instance.Calculation.CountConcrete: .##} кг.";
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
			try
			{
				var fileName = $"Рассчет от {DateTime.Now:U}.doc".Replace(':', '.');
				var export = new WordExport(fileName);
				export.Export(MainWindowViewModel.Instance.Calculation.CalculationResult);

				var mess = new MessageBoxWindow($"Расчет ({fileName}) сформирован на рабочем столе");
				mess.ShowDialog();
			}
			catch (Exception exception)
			{
				var mess = new MessageBoxWindow(exception.Message);
				mess.ShowDialog();
			}
		}

        private void SendCalculationsButton_Click(object sender, RoutedEventArgs e)
        {
            var calculationResult = MainWindowViewModel.Instance.Calculation.CalculationResult;
            var result = "-1";
            var fcmServerAddress = "https://fcm.googleapis.com/fcm/send";
            var serverKey = "AAAA4LGhV78:APA91bFcC8w3KlsYsW6GDR0Sp9B1CuGuS3HBnUjL5qrIY3_FWNGEL1J_8qy3sDZHXq9BfqPoRNtFEMtCtD_8LN6yS6DgIqxqvY9TO7UyRK62dMFoME8NK2b3yULCk3N_VSB3inqhJ_wv";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(fcmServerAddress);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=" + serverKey);
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string notificationJsonBody = @"{""to"": ""/topics/CalculationsResults"",";
                //notificationJsonBody += @"""notification"": {";
                //notificationJsonBody += @"""title"": """ + "Расчёты выполнены для:" + @""",";
                //notificationJsonBody += @"""text"": """ + MainWindowViewModel.Instance.Calculation.CountConcrete.ToString()
                //    + " куб.м. бетона марки " + MainWindowViewModel.Instance.BrandConcrete.Name + @""",";
                //notificationJsonBody += @"""sound"": ""default""},";
                notificationJsonBody += @"""data"": {";

                notificationJsonBody += @"""Title"": """ + "Расчёты выполнены для:" + @""",";
                notificationJsonBody += @"""Text"": """ + MainWindowViewModel.Instance.Calculation.CountConcrete.ToString()
                    + " куб.м. бетона марки " + MainWindowViewModel.Instance.BrandConcrete.Name + @""",";

                notificationJsonBody += @"""CountConcrete"": """ + MainWindowViewModel.Instance.Calculation.CountConcrete.ToString() + @""",";
                notificationJsonBody += @"""BrandConcrete"": """ + MainWindowViewModel.Instance.BrandConcrete.Name + @""",";
                notificationJsonBody += @"""CementBrand"": """ + MainWindowViewModel.Instance.CementBrand.Name + @""",";
                notificationJsonBody += @"""CoarseAggregate"": """ + MainWindowViewModel.Instance.CoarseAggregate.Name + @""",";
                notificationJsonBody += @"""FineAggregate"": """ + MainWindowViewModel.Instance.FineAggregate.Name + @""",";
                notificationJsonBody += @"""Admixture"": """ + MainWindowViewModel.Instance.Admixtures.Name + @""",";

                notificationJsonBody += @"""CementValue"": """ + calculationResult.CorrectedCementConsumption.ToString() + @""",";
                notificationJsonBody += @"""WaterValue"": """ + calculationResult.WaterFlowWithRegardToAirContent.ToString() + @""",";
                notificationJsonBody += @"""SandValue"": """ + calculationResult.TheAmountOfSandDry.ToString() + @""",";
                notificationJsonBody += @"""CoarseAggregateValue"": """ + calculationResult.NumberOfCoarseAggregate.ToString() + @""",";
                notificationJsonBody += @"""ChemicalAdditiveValue"": """ + calculationResult.ChemicalAdditive + @"""}}";

                streamWriter.Write(notificationJsonBody);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
        }

		private void UpdateComboBox(object sender, EventArgs e)
		{
			CountConcreteTextBox.Text = MainWindowViewModel.Instance.CountConcrete.ToString();
			for (var index = 0; index < MainWindowViewModel.Instance.ConcreteFormula.BrandConcreteList.Count; index++)
			{
				var value = MainWindowViewModel.Instance.ConcreteFormula.BrandConcreteList[index];
				if (value.Name == MainWindowViewModel.Instance.BrandConcrete.Name)
				{
					BrandConcreteComboBox.SelectedIndex = index;
				}
			}

			for (var index = 0; index < MainWindowViewModel.Instance.ConcreteFormula.BrandConcreteFrostResistancesList.Count; index++)
			{
				var value = MainWindowViewModel.Instance.ConcreteFormula.BrandConcreteFrostResistancesList[index];
				if (value.Name == MainWindowViewModel.Instance.BrandConcreteFrostResistance.Name)
				{
					BrandConcreteFrostResistanceComboBox.SelectedIndex = index;
				}
			}

			for (var index = 0; index < MainWindowViewModel.Instance.ConcreteFormula.HardeningConditionsesList.Count; index++)
			{
				var value = MainWindowViewModel.Instance.ConcreteFormula.HardeningConditionsesList[index];
				if (value.Name == MainWindowViewModel.Instance.HardeningConditions.Name)
				{
					HardeningConditionsComboBox.SelectedIndex = index;
				}
			}

			for (var index = 0; index < MainWindowViewModel.Instance.ConcreteFormula.FineAggregateList.Count; index++)
			{
				var value = MainWindowViewModel.Instance.ConcreteFormula.FineAggregateList[index];
				if (value.Name == MainWindowViewModel.Instance.FineAggregate.Name)
				{
					FineAggregateComboBox.SelectedIndex = index;
				}
			}

			for (var index = 0; index < MainWindowViewModel.Instance.ConcreteFormula.CementBrandList.Count; index++)
			{
				var value = MainWindowViewModel.Instance.ConcreteFormula.CementBrandList[index];
				if (value.Name == MainWindowViewModel.Instance.CementBrand.Name)
				{
					CementBrandComboBox.SelectedIndex = index;
				}
			}

			for (var index = 0; index < MainWindowViewModel.Instance.ConcreteFormula.MixtureMobilityList.Count; index++)
			{
				var value = MainWindowViewModel.Instance.ConcreteFormula.MixtureMobilityList[index];
				if (value.Name == MainWindowViewModel.Instance.MixtureMobility.Name)
				{
					MixtureMobilityComboBox.SelectedIndex = index;
				}
			}

			for (var index = 0; index < MainWindowViewModel.Instance.ConcreteFormula.AdmixturesList.Count; index++)
			{
				var value = MainWindowViewModel.Instance.ConcreteFormula.AdmixturesList[index];
				if (value.Name == MainWindowViewModel.Instance.Admixtures.Name)
				{
					AdmixturesComboBox.SelectedIndex = index;
				}
			}
		}
	}
}
