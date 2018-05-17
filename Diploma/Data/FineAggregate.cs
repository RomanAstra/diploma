using System;

namespace Diploma.Data
{
    [Serializable]
    public class FineAggregate
	{
		private string _fillerType;

        public string FillerType { get => _fillerType; set => _fillerType = value; }
    }
}