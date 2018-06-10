using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Diploma.Helper;

namespace Diploma.View
{
	/// <summary>
	/// Логика взаимодействия для AdministrationWindow.xaml
	/// </summary>
	public partial class AdministrationWindow : Window
	{
		public AdministrationWindow()
		{
			InitializeComponent();
			MainPasswordBox.Visibility = Visibility.Visible;
			MainTextBox.Visibility = Visibility.Collapsed;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				var password = MainPasswordBox.Password;
				if (String.IsNullOrWhiteSpace(password))
				{
					MessageBoxWindow messageBoxWindow = new MessageBoxWindow("Пароль не должен быть пустым");
					messageBoxWindow.ShowDialog();
					return;
				}
				if (VariablesClass.Admins.ContainsValue(password))
				{
					MessageBoxWindow messageBoxWindow = new MessageBoxWindow("Пароль уже задан");
					messageBoxWindow.ShowDialog();
					return;
				}
				VariablesClass.SetPassword(password);
				VariablesClass.SaveData();
				MainPasswordBox.Password = String.Empty;
			}
			catch (Exception exception)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(exception.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		private void MainImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MainImage.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"/Resources/vis.png"));
			MainPasswordBox.Visibility = Visibility.Collapsed;
			MainTextBox.Text = MainPasswordBox.Password;
			MainTextBox.Visibility = Visibility.Visible;
		}

		private void MainImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			MainImage.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"/Resources/anvis.png"));
			MainPasswordBox.Visibility = Visibility.Visible;
			MainPasswordBox.Password = MainTextBox.Text;
			MainTextBox.Visibility = Visibility.Collapsed;
		}
	}
}
