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
                new BrandConcrete {Name = "M50"},
                new BrandConcrete {Name = "M75"},
                new BrandConcrete {Name = "M100"},
                new BrandConcrete {Name = "M150"},
            };

            CoarseAggregateList = new ObservableCollection<CoarseAggregate>
            {
                new CoarseAggregate {Name = "Щебень"},
                new CoarseAggregate {Name = "Гравий"},
                new CoarseAggregate {Name = "Щебень шлаковый"},
                new CoarseAggregate {Name = "Твоё очко"},
            };

            FineAggregateList = new ObservableCollection<FineAggregate>
            {
                new FineAggregate {Name = "Песок карьерный"},
                new FineAggregate {Name = "Песок речной"},
                new FineAggregate {Name = "Морские блять ракушки"},
            };

            CementBrandList = new ObservableCollection<CementBrand>
            {
                new CementBrand {Name = "ПЦ300"},
                new CementBrand {Name = "ПЦ400"},
                new CementBrand {Name = "ПЦ500"},
            };

            MixtureMobilityList = new ObservableCollection<MixtureMobility>
            {
                new MixtureMobility {Name = "Ж1"},
                new MixtureMobility {Name = "Ж2"},
                new MixtureMobility {Name = "П1"},
            };

            AdmixturesList = new ObservableCollection<Admixtures>
            {
                new Admixtures { Name="хуй" },
                new Admixtures { Name="пизда" },
                new Admixtures { Name="джигурда" },
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