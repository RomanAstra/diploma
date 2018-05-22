using System;

namespace Diploma.Data
{
	[Serializable]
	public class BrandConcrete : ICompositions
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public string Discription { get; set; }
	}
}