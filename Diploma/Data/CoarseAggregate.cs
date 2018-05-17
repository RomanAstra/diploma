using System;

namespace Diploma.Data
{
	[Serializable]
	public class CoarseAggregate
	{
		private string _fillerType;

        public string FillerType { get => _fillerType; set => _fillerType = value; }
    }
}