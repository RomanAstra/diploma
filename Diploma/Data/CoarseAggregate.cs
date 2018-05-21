using System;

namespace Diploma.Data
{
	[Serializable]
	public class CoarseAggregate
	{ 
        public string FillerType { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
	}
}