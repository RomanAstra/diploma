using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Diploma.Data
{
	[Serializable]
	public class ConcreteFormula
	{
		public ConcreteFormula()
		{
            //TODO временные данные, для теста
            BrandConcreteList = new ObservableCollection<BrandConcrete>
            {
                new BrandConcrete {Strength = "M50"},
                new BrandConcrete {Strength = "M75"},
                new BrandConcrete {Strength = "M100"},
                new BrandConcrete {Strength = "M150"},
            };

            CoarseAggregateList = new ObservableCollection<CoarseAggregate>
            {
                new CoarseAggregate {FillerType = "Щебень"},
                new CoarseAggregate {FillerType = "Гравий"},
                new CoarseAggregate {FillerType = "Щебень шлаковый"},
                new CoarseAggregate {FillerType = "Твоё очко"},
            };

            FineAggregateList = new ObservableCollection<FineAggregate>
            {
                new FineAggregate {FillerType = "Песок карьерный"},
                new FineAggregate {FillerType = "Песок речной"},
                new FineAggregate {FillerType = "Морские блять ракушки"},
            };

            CementBrandList = new ObservableCollection<CementBrand>
            {
                new CementBrand {Brand = "ПЦ300"},
                new CementBrand {Brand = "ПЦ400"},
                new CementBrand {Brand = "ПЦ500"},
            };

            MixtureMobilityList = new ObservableCollection<MixtureMobility>
            {
                new MixtureMobility {Mobility = "Ж1"},
                new MixtureMobility {Mobility = "Ж2"},
                new MixtureMobility {Mobility = "П1"},
            };

            AdmixturesList = new ObservableCollection<Admixtures>
            {
                new Admixtures { Admixture="хуй" },
                new Admixtures { Admixture="пизда" },
                new Admixtures { Admixture="джигурда" },
            };
        }

		public ObservableCollection<BrandConcrete> BrandConcreteList { get; set; }
        public ObservableCollection<CoarseAggregate> CoarseAggregateList { get; set; }
        public ObservableCollection<FineAggregate> FineAggregateList { get; set; }
        public ObservableCollection<CementBrand> CementBrandList { get; set; }
        public ObservableCollection<MixtureMobility> MixtureMobilityList { get; set; }
        public ObservableCollection<Admixtures> AdmixturesList { get; set; }
    }
}