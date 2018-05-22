using System;
using System.Collections.Generic;
using Diploma.Data;
using Diploma.DataSave;

namespace Diploma.Helper
{
	public static class VariablesClass
	{
		public static Dictionary<string, string> Senders { get; }

		private static readonly Admin _admin;
		private static readonly IData<Admin> _data = new DataXML<Admin>("Admin");
		static VariablesClass()
		{
			_admin = _data?.Load();
			if (_admin == null)
			{
				_admin = new Admin
				{
					Login = "Admin",
					Password = "234"
				};
			}

			Senders = new Dictionary<string, string>
			{
				{_admin.Login, PasswordClass.GetPassword(_admin.Password)}
			};
		}

		public static void SaveData()
		{
			if (_admin != null) _data?.Save(_admin);
		}

		[Serializable]
		public class Admin
		{
			public string Login { get; set; }
			public string Password { get; set; }
		}
	}
}