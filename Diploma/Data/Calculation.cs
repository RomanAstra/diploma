using System;

namespace Diploma.Data
{
	[Serializable]
	public class Calculation
	{
		public string Name { get; set; }
		public DateTime DateTime { get; set; }
		public Admixtures Admixtures { get; set; }
		public BrandConcrete BrandConcrete { get; set; }
		public CementBrand CementBrand { get; set; }
		public CoarseAggregate CoarseAggregate { get; set; }
		public FineAggregate FineAggregate { get; set; }
		public MixtureMobility MixtureMobility { get; set; }
	}
}