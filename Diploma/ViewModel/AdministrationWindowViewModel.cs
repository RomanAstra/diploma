using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Diploma.Data;
using Diploma.Infrastructure;
using Diploma.View;

namespace Diploma.ViewModel
{
	public class AdministrationWindowViewModel : ViewModelBase
	{
		private readonly ConcreteFormula _concreteFormula;
		private ICompositions _concreteItem;

		private Dictionary<string, IList> _compositions;
		
		public Dictionary<string, IList> Compositions
		{
			get => _compositions;
			set
			{
				_compositions = value;
				OnPropertyChanged(nameof(Compositions));
			}
		}

		private IList _currentCompositions;

		public IList CurrentCompositions
		{
			get => _currentCompositions;
			set
			{
				_currentCompositions = value;
				OnPropertyChanged(nameof(CurrentCompositions));
			}
		}

		public AdministrationWindowViewModel()
		{
			_concreteFormula = ConcreteFormulaRepositoty.ConcreteFormula;
			Compositions = new Dictionary<string, IList>
			{
				{"Класс бетона", _concreteFormula?.BrandConcreteList.ToList()},
				{"Крупный заполнитель", _concreteFormula?.CoarseAggregateList.ToList()},
				{"Мелкий заполнитель", _concreteFormula?.FineAggregateList.ToList()},
				{"Марка цемента", _concreteFormula?.CementBrandList.ToList()},
				{"Подвижность смеси", _concreteFormula?.MixtureMobilityList.ToList()},
				{"Примеси", _concreteFormula?.AdmixturesList.ToList()},
			};
		}

		#region Команда закрытия формы

		private RelayCommand _closeCommand;

		public ICommand Close => _closeCommand ?? (_closeCommand =
			                         new RelayCommand(ExecuteCloseCommand));
		public void ExecuteCloseCommand(object parameter)
		{
			try
			{
				if (parameter is AdministrationWindow temp)
				{
					temp.Close();
				}
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion

		#region Команда выбора элемента combobox

		private RelayCommand _selectioCommand;

		public ICommand SelectioCommand => _selectioCommand
		                                   ?? (_selectioCommand = new RelayCommand(ExecuteSelectioCommand));

		private void ExecuteSelectioCommand(object o)
		{
			var temp = o.ToString();
			try
			{
				if (!Compositions.ContainsKey(temp)) return;
				CurrentCompositions = Compositions[temp];
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion
		
		#region Команда выбора элемента списка

		private RelayCommand _selectioListItemCommand;

		public ICommand SelectioListItemCommand => _selectioListItemCommand
										   ?? (_selectioListItemCommand = 
			                                   new RelayCommand(ExecuteSelectioListItemCommand));

		private void ExecuteSelectioListItemCommand(object o)
		{
			try
			{
				if (o is ICompositions temp)
				{
					_concreteItem = temp;
				}
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion

		protected override void OnDispose()
		{
			
		}
	}
}