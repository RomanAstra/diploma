using System;
using System.Collections.Generic;
using DataDB;

namespace Diploma.Helper
{
	public static class VariablesClass
	{
		public static Dictionary<string, string> Senders { get; private set; }

		private static readonly Admin _admin;
		private static readonly IData<Admin> _data = new DataDb<Admin>("Admin");
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
			SetSenders();
		}

		public static void SaveData()
		{
			if (_admin != null) _data?.Save(_admin);
		}

		public static void SetLogin(string login)
		{
			_admin.Login = login;
			SetSenders();
		}

		public static void SetPassword(string password)
		{
			_admin.Password = PasswordClass.GetCodPassword(password);
			SetSenders();
		}

		private static void SetSenders()
		{
			if (_admin == null) return;
			Senders = new Dictionary<string, string>
			{
				{_admin.Login, PasswordClass.GetPassword(_admin.Password)}
			};
		}

		[Serializable]
		public class Admin
		{
			public string Login { get; set; }
			public string Password { get; set; }
		}
	}
}