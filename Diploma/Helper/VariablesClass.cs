using System;
using System.Collections.Generic;
using DataDB;

namespace Diploma.Helper
{
	public static class VariablesClass
	{
		public static Dictionary<string, string> Admins { get; private set; }

		private static readonly Admin _admin;
		private static readonly IData<Admin> _data = new DataDb<Admin>("Admin");
		static VariablesClass()
		{
			_admin = _data?.Load();
			if (_admin == null)
			{
				_admin = new Admin
				{
					Login = "admin",
					Password = "234"
				};
			}
			SetAdmin();
		}

		public static void SaveData()
		{
			if (_admin != null) _data?.Save(_admin);
		}

		public static void SetLogin(string login)
		{
			_admin.Login = login;
			SetAdmin();
		}

		public static void SetPassword(string password)
		{
			_admin.Password = PasswordClass.GetCodPassword(password);
			SetAdmin();
		}

		private static void SetAdmin()
		{
			if (_admin == null) return;
			Admins = new Dictionary<string, string>
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