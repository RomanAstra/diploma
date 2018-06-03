using System;

namespace Diploma.Data
{
	[Serializable]
	public class HardeningConditions : ICompositions
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
	}
}