using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Diploma.Data;
using Diploma.Helper;
using Diploma.Infrastructure;
using Diploma.View;

namespace Diploma.ViewModel
{
	public class AdministrationWindowViewModel : ViewModelBase
	{
		private readonly ConcreteFormula _concreteFormula;
		private ICompositions _concreteItem;
		private string _concreteKey;
		private bool _isEdit;
		private Action _action;

		private string _nameItem;
		private string _valueItem;
		private string _descriptionItem;

		public string NameItem
		{
			get => _nameItem;
			set
			{
				_nameItem = value;
				OnPropertyChanged(nameof(NameItem));
			}
		}
		public string ValueItem
		{
			get => _valueItem;
			set
			{
				_valueItem = value;
				OnPropertyChanged(nameof(ValueItem));
			}
		}
		public string DescriptionItem
		{
			get => _descriptionItem;
			set
			{
				_descriptionItem = value;
				OnPropertyChanged(nameof(DescriptionItem));
			}
		}

		private enum Action
		{
			Add,
			Edit,
			Null
		}

		public bool IsEdit
		{
			get => _isEdit;
			set
			{
				_isEdit = value;
				OnPropertyChanged(nameof(IsEdit));
			}
		}

		private string _login;
		private string _password;

		public string Logint
		{
			get => _login;
			set
			{
				_login = value;
				OnPropertyChanged(nameof(Logint));
			}
		}
		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

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
				IsEdit = true;
				_action = Action.Add;
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
				IsEdit = true;
				_action = Action.Edit;
				NameItem = _concreteItem.Name;
				ValueItem = _concreteItem.Value;
				DescriptionItem = _concreteItem.Description;
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

		#region Команда сохранения изменений

		private RelayCommand _saveCommand;

		public ICommand Save => _saveCommand ?? (_saveCommand =
								   new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand));

		public void ExecuteSaveCommand(object parameter)
		{
			try
			{
				if (_action == Action.Add)
				{
					if (CurrentCompositions is IList tempItem)
					{
						var o = tempItem[0] as ICompositions;

						AddCompositions(o);
					}
				}
				if (_action == Action.Edit)
				{
					EditCompositions(_concreteItem);
				}

				IsEdit = false;
				_action = Action.Null;
				_concreteItem = null;

				NameItem = String.Empty;
				ValueItem = String.Empty;
				DescriptionItem = String.Empty;
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		public bool CanExecuteSaveCommand(object parameter)
		{
			return !String.IsNullOrWhiteSpace(NameItem) && !String.IsNullOrWhiteSpace(ValueItem);
		}

		#endregion

		#region Команда отмены

		private RelayCommand _cancelCommand;

		public ICommand Cancel => _cancelCommand ?? (_cancelCommand =
									new RelayCommand(ExecuteCancelCommand));

		public void ExecuteCancelCommand(object parameter)
		{
			try
			{
				IsEdit = false;
				_action = Action.Null;
				_concreteItem = null;

				NameItem = String.Empty;
				ValueItem = String.Empty;
				DescriptionItem = String.Empty;
			}
			catch (Exception e)
			{
				MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
				messageBoxWindow.ShowDialog();
			}
		}

		#endregion

		#region Команда редактирования логина или пароля

		//private RelayCommand _editPasswordCommand;

		//public ICommand EditPassword => _editPasswordCommand ?? (_editPasswordCommand =
		//	                        new RelayCommand(ExecuteEditPasswordCommand, CanExecuteEditPasswordCommand));

		//public void ExecuteEditPasswordCommand(object parameter)
		//{
		//	try
		//	{
		//		if (!String.IsNullOrWhiteSpace(Password))
		//		{
		//			VariablesClass.SetPassword(Password);
		//			VariablesClass.SaveData();
		//		}
		//		//if (!String.IsNullOrWhiteSpace(Logint))
		//		//{
		//		//	VariablesClass.SetPassword(Logint);
		//		//	VariablesClass.SaveData();
		//		//}
		//		Password = String.Empty;
		//		//Logint = String.Empty;
		//	}
		//	catch (Exception e)
		//	{
		//		MessageBoxWindow messageBoxWindow = new MessageBoxWindow(e.Message);
		//		messageBoxWindow.ShowDialog();
		//	}
		//}

		//public bool CanExecuteEditPasswordCommand(object parameter)
		//{
		//	return !String.IsNullOrWhiteSpace(Password) || !String.IsNullOrWhiteSpace(Logint);
		//}

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

			ConcreteFormulaRepositoty.SaveData();
		}

		private void AddCompositions(ICompositions compositions)
		{
			switch (compositions)
			{
				case BrandConcrete item:
					ConcreteFormulaRepositoty.AddBrandConcrete(new BrandConcrete
					{
						Name = NameItem,
						Value = ValueItem,
						Description = DescriptionItem
					});
					break;
				case Admixtures item:
					ConcreteFormulaRepositoty.AddAdmixtures(new Admixtures
					{
						Name = NameItem,
						Value = ValueItem,
						Description = DescriptionItem
					});
					break;
				case CementBrand item:
					ConcreteFormulaRepositoty.AddCementBrand(new CementBrand
					{
						Name = NameItem,
						Value = ValueItem,
						Description = DescriptionItem
					});
					break;
				case CoarseAggregate item:
					ConcreteFormulaRepositoty.AddCoarseAggregate(new CoarseAggregate
					{
						Name = NameItem,
						Value = ValueItem,
						Description = DescriptionItem
					});
					break;
				case FineAggregate item:
					ConcreteFormulaRepositoty.AddFineAggregate(new FineAggregate
					{
						Name = NameItem,
						Value = ValueItem,
						Description = DescriptionItem
					});
					break;
				case MixtureMobility item:
					ConcreteFormulaRepositoty.AddMixtureMobility(new MixtureMobility
					{
						Name = NameItem,
						Value = ValueItem,
						Description = DescriptionItem
					});
					break;
				case null:
					throw new Exception("Не выбран объект");
				default:
					Console.WriteLine("Объект другого типа");
					break;
			}

			ConcreteFormulaRepositoty.SaveData();
		}

		private void EditCompositions(ICompositions compositions)
		{
			switch (compositions)
			{
				case BrandConcrete item:
					ConcreteFormulaRepositoty.EditBrandConcrete(item, NameItem,ValueItem,DescriptionItem);
					break;
				case Admixtures item:
					ConcreteFormulaRepositoty.EditAdmixtures(item, NameItem, ValueItem, DescriptionItem);
					break;
				case CementBrand item:
					ConcreteFormulaRepositoty.EditCementBrand(item, NameItem, ValueItem, DescriptionItem);
					break;
				case CoarseAggregate item:
					ConcreteFormulaRepositoty.EditCoarseAggregate(item, NameItem, ValueItem, DescriptionItem);
					break;
				case FineAggregate item:
					ConcreteFormulaRepositoty.EditFineAggregate(item, NameItem, ValueItem, DescriptionItem);
					break;
				case MixtureMobility item:
					ConcreteFormulaRepositoty.EditMixtureMobility(item, NameItem, ValueItem, DescriptionItem);
					break;
				case null:
					throw new Exception("Не выбран объект");
				default:
					Console.WriteLine("Объект другого типа");
					break;
			}

			ConcreteFormulaRepositoty.SaveData();
		}

		protected override void OnDispose()
		{

		}
	}
}