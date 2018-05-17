using System;

namespace Diploma.Data
{
	[Serializable]
	public class CementBrand
	{
		private string _brand;
		public string Description;

        public string Brand { get => _brand; set => _brand = value; }
    }
}