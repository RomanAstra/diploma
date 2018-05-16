using System;

namespace Diploma.Data
{
	[Serializable]
	public class BrandConcrete
	{
		private string strength;
		public string Discription;

		public string Strength { get => strength; set => strength = value; }
	}
}