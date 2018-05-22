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
		private string _concreteKey;

		private Dictionary<string, ICollection> _compositions;
		
		public Dictionary<string, ICollection> Compositions
		{
			get => _compositions;
			set
			{
				_compositions = value;
				OnPropertyChanged(nameof(Compositions));
			}
		}

		private ICollection _currentCompositions;

		public ICollection CurrentCompositions
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
			Compositions = new Dictionary<string, ICollection>
			{
				{"Класс бетона", _concreteFormula?.BrandConcreteList},
				{"Крупный заполнитель", _concreteFormula?.CoarseAggregateList},
				{"Мелкий заполнитель", _concreteFormula?.FineAggregateList},
				{"Марка цемента", _concreteFormula?.CementBrandList},
				{"Подвижность смеси", _concreteFormula?.MixtureMobilityList},
				{"Примеси", _concreteFormula?.AdmixturesList},
			};
			_currentCompositions = Compositions["Класс бетона"];
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
			try
			{
				_concreteKey = o.ToString();
				if (!Compositions.ContainsKey(_concreteKey)) return;
				CurrentCompositions = Compositions[_concreteKey];
				_concreteItem = null;
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

		#region Команда добавления элемента

		private RelayCommand _addCommand;

		public ICommand Add => _addCommand ?? (_addCommand =
									new RelayCommand(ExecuteAddCommand));

		public void ExecuteAddCommand(object parameter)
		{
			try
			{
				_concreteItem = null;
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion

		#region Команда редактирования элемента

		private RelayCommand _editCommand;

		public ICommand Edit => _editCommand ?? (_editCommand =
									 new RelayCommand(ExecuteEditCommand,
										 CanExecuteEditCommand));

		public void ExecuteEditCommand(object parameter)
		{
			try
			{
				_concreteItem = null;
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		public bool CanExecuteEditCommand(object parameter)
		{
			return _concreteItem != null;
		}

		#endregion

		#region Команда Удаление элемента

		private RelayCommand _deleteCommand;

		public ICommand Delete => _deleteCommand ?? (_deleteCommand =
									 new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand));

		public void ExecuteDeleteCommand(object parameter)
		{
			try
			{
				DeleteCompositions(_concreteItem);
				_concreteItem = null;
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		public bool CanExecuteDeleteCommand(object parameter)
		{
			return _concreteItem != null;
		}

		#endregion

		private void DeleteCompositions(ICompositions compositions)
		{
			switch (compositions)
			{
				case BrandConcrete item:
					ConcreteFormulaRepositoty.DeleteBrandConcrete(item);
					break;
				case Admixtures item:
					ConcreteFormulaRepositoty.DeleteAdmixtures(item);
					break;
				case CementBrand item:
					ConcreteFormulaRepositoty.DeleteCementBrand(item);
					break;
				case CoarseAggregate item:
					ConcreteFormulaRepositoty.DeleteCoarseAggregate(item);
					break;
				case FineAggregate item:
					ConcreteFormulaRepositoty.DeleteFineAggregate(item);
					break;
				case MixtureMobility item:
					ConcreteFormulaRepositoty.DeleteMixtureMobility(item);
					break;
				case null:
					throw new Exception("Не выбран объект");
				default:
					Console.WriteLine("Объект другого типа");
					break;
			}

			//ConcreteFormulaRepositoty.SaveData();
		}

		protected override void OnDispose()
		{
			
		}
	}
}