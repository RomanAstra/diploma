using System;

namespace Diploma.Data
{
    [Serializable]
    public class MixtureMobility
	{
        private string _mobility;
        public string Description;

        public string Mobility { get => _mobility; set => _mobility = value; }
    }
}