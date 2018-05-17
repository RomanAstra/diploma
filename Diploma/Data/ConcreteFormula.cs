using System;
using System.Collections.Generic;

namespace Diploma.Data
{
	[Serializable]
	public class ConcreteFormula
	{
		public ConcreteFormula()
		{
            //TODO временные данные, для теста
            BrandConcreteList = new List<BrandConcrete>
            {
                new BrandConcrete {Strength = "M50"},
                new BrandConcrete {Strength = "M75"},
                new BrandConcrete {Strength = "M100"},
                new BrandConcrete {Strength = "M150"},
            };

            CoarseAggregateList = new List<CoarseAggregate>
            {
                new CoarseAggregate {FillerType = "Щебень"},
                new CoarseAggregate {FillerType = "Гравий"},
                new CoarseAggregate {FillerType = "Щебень шлаковый"},
                new CoarseAggregate {FillerType = "Твоё очко"},
            };

            FineAggregateList = new List<FineAggregate>
            {
                new FineAggregate {FillerType = "Песок карьерный"},
                new FineAggregate {FillerType = "Песок речной"},
                new FineAggregate {FillerType = "Морские блять ракушки"},
            };

            CementBrandList = new List<CementBrand>
            {
                new CementBrand {Brand = "ПЦ300"},
                new CementBrand {Brand = "ПЦ400"},
                new CementBrand {Brand = "ПЦ500"},
            };

            MixtureMobilityList = new List<MixtureMobility>
            {
                new MixtureMobility {Mobility = "Ж1"},
                new MixtureMobility {Mobility = "Ж2"},
                new MixtureMobility {Mobility = "П1"},
            };

            AdmixturesList = new List<Admixtures>
            {
                new Admixtures {Admixture = "хуй"},
                new Admixtures {Admixture = "пизда"},
                new Admixtures {Admixture = "джигурда"},
            };
        }

		public List<BrandConcrete> BrandConcreteList { get; set; }
        public List<CoarseAggregate> CoarseAggregateList { get; set; }
        public List<FineAggregate> FineAggregateList { get; set; }
        public List<CementBrand> CementBrandList { get; set; }
        public List<MixtureMobility> MixtureMobilityList { get; set; }
        public List<Admixtures> AdmixturesList { get; set; }
    }
}