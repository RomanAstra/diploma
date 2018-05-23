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
				new BrandConcrete {Name = "M100",Value = "100"},
				new BrandConcrete {Name = "M150",Value = "150"},
				new BrandConcrete {Name = "M200",Value = "200"},
				new BrandConcrete {Name = "M250",Value = "250"},
				new BrandConcrete {Name = "M300",Value = "300"},
				new BrandConcrete {Name = "M350",Value = "350"},
				new BrandConcrete {Name = "M400",Value = "400"},
				new BrandConcrete {Name = "M450",Value = "450"},
				new BrandConcrete {Name = "M500",Value = "500"},
				new BrandConcrete {Name = "M550",Value = "550"},
				new BrandConcrete {Name = "M600",Value = "600"}
			};

			CoarseAggregateList = new ObservableCollection<CoarseAggregate>
			{
				new CoarseAggregate {Name = "Щебень|10mm",Value = "4"},
				new CoarseAggregate {Name = "Щебень|20mm",Value = "5"},
				new CoarseAggregate {Name = "Щебень|40mm",Value = "6"},
				new CoarseAggregate {Name = "Щебень|70mm",Value = "7"},
				new CoarseAggregate {Name = "Гравий|10mm",Value = "0"},
				new CoarseAggregate {Name = "Гравий|20mm",Value = "1"},
				new CoarseAggregate {Name = "Гравий|40mm",Value = "2"},
				new CoarseAggregate {Name = "Гравий|70mm",Value = "3"},
			};

			FineAggregateList = new ObservableCollection<FineAggregate>
			{
				new FineAggregate {Name = "Песок мелкий|1.1-1.8mm", Value = "-5"},
				new FineAggregate {Name = "Песок средний|2-2.5mm", Value = "0"},
				new FineAggregate {Name = "Песок крупный|более 2.5mm", Value = "5"},
			};

			CementBrandList = new ObservableCollection<CementBrand>
			{
				new CementBrand {Name = "M300", Value = "300"},
				new CementBrand {Name = "M400",Value = "400"},
				new CementBrand {Name = "M500",Value = "500"},
				new CementBrand {Name = "M600", Value = "600"}
			};

			MixtureMobilityList = new ObservableCollection<MixtureMobility>
			{
				new MixtureMobility {Name = "Ж4", Value = "0"},
				new MixtureMobility {Name = "Ж3", Value = "1"},
				new MixtureMobility {Name = "Ж2", Value = "2"},
				new MixtureMobility {Name = "Ж1", Value = "3"},
				new MixtureMobility {Name = "П1", Value = "4"},
				new MixtureMobility {Name = "П2", Value = "5"},
				new MixtureMobility {Name = "П3", Value = "6"},
				new MixtureMobility {Name = "П4", Value = "7"},
			};

			AdmixturesList = new ObservableCollection<Admixtures>
			{
				new Admixtures { Name="gtx 332" },
				new Admixtures { Name="sft 147" },
				new Admixtures { Name="srt 488" },
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