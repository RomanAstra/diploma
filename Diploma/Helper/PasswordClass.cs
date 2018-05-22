namespace Diploma.Helper
{
	/// <summary>
	/// На вход подаём зашифрованный пароль, на выходе получаем пароль для email
	/// </summary>
	public static class PasswordClass
	{
		public static string GetPassword(string pSPassw)
		{
			string password = "";
			foreach (char a in pSPassw)
			{
				char ch = a;
				ch--;
				password += ch;
			}
			return password;
		}
		/// <summary>
		/// На вход подаем пароль, на выходе получаем зашифрованный пароль
		/// </summary>
		/// <param name="pSPassword"></param>
		/// <returns></returns>
		public static string GetCodPassword(string pSPassword)
		{
			string sCode = "";
			foreach (char a in pSPassword)
			{
				char ch = a;
				ch++;
				sCode += ch;
			}
			return sCode;
		}
	}
}