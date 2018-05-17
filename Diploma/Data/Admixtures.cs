using System;

namespace Diploma.Data
{
    [Serializable]
    public class Admixtures
    {
        private string _admixture;
        public string Description;

        public string Admixture { get => _admixture; set => _admixture = value; }
    }
}
