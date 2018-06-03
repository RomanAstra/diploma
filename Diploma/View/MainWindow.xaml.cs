using System.Windows;
using System.Windows.Documents;
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
			var paragraph = new Paragraph();
			paragraph.Inlines.Add(new Run(MainWindowViewModel.Instance.Calculation.CalculationResult.WaterFlowAccordingDirections.ToString()));
			MainRichTextBox.Document.Blocks.Add(paragraph);

			paragraph = new Paragraph();
			paragraph.Inlines.Add(new Run(MainWindowViewModel.Instance.Calculation.CalculationResult.WaterConsumptionIncludingOK.ToString()));
			MainRichTextBox.Document.Blocks.Add(paragraph);

			paragraph = new Paragraph();
			paragraph.Inlines.Add(new Run(MainWindowViewModel.Instance.Calculation.CalculationResult.WaterFlowWithRegardToAirContent.ToString()));
			MainRichTextBox.Document.Blocks.Add(paragraph);

			paragraph = new Paragraph();
			paragraph.Inlines.Add(new Run(MainWindowViewModel.Instance.Calculation.CalculationResult.QuantityOfCementByCalculation.ToString()));
			MainRichTextBox.Document.Blocks.Add(paragraph);

			paragraph = new Paragraph();
			paragraph.Inlines.Add(new Run(MainWindowViewModel.Instance.Calculation.CalculationResult.WCByCalculation.ToString()));
			MainRichTextBox.Document.Blocks.Add(paragraph);

			paragraph = new Paragraph();
			paragraph.Inlines.Add(new Run(MainWindowViewModel.Instance.Calculation.CalculationResult.MaximumPermissibleAccordingWCToInstructions.ToString()));
			MainRichTextBox.Document.Blocks.Add(paragraph);
		}
	}
}
