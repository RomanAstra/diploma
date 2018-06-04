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
				new BrandConcrete {Name = "B7.5",Value = "98,2"},
				new BrandConcrete {Name = "B10",Value = "131"},
				new BrandConcrete {Name = "B12.5",Value = "163,7"},
				new BrandConcrete {Name = "B15",Value = "196,5"},
				new BrandConcrete {Name = "B20",Value = "261,9"},
				new BrandConcrete {Name = "B22.5",Value = "294,4"},
				new BrandConcrete {Name = "B25",Value = "327,4"},
				new BrandConcrete {Name = "B30",Value = "392,9"},
				new BrandConcrete {Name = "B35",Value = "458,4"},
				new BrandConcrete {Name = "B40",Value = "523,9"},
				new BrandConcrete {Name = "B45",Value = "589,4"}
			};

			CoarseAggregateList = new ObservableCollection<CoarseAggregate>
			{
				new CoarseAggregate {Name = "Гравий|10mm",Value = "0"},
				new CoarseAggregate {Name = "Гравий|20mm",Value = "1"},
				new CoarseAggregate {Name = "Гравий|40mm",Value = "2"},
				new CoarseAggregate {Name = "Гравий|70mm",Value = "3"},
				new CoarseAggregate {Name = "Щебень|10mm",Value = "0"},
				new CoarseAggregate {Name = "Щебень|20mm",Value = "1"},
				new CoarseAggregate {Name = "Щебень|40mm",Value = "2"},
				new CoarseAggregate {Name = "Щебень|70mm",Value = "3"},
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
				new MixtureMobility {Name = "П1", Value = "2,5"},
				new MixtureMobility {Name = "П2", Value = "7"},
				new MixtureMobility {Name = "П3", Value = "12"},
				new MixtureMobility {Name = "П4", Value = "16"},
			};

			AdmixturesList = new ObservableCollection<Admixtures>
			{
				new Admixtures { Name="MasterGlenium 101", Value = "0,4"},
				new Admixtures { Name="MasterGlenium 116W", Value = "0,5" },
				new Admixtures { Name="MasterGlenium 806 PAV", Value = "0,6" },
			};

			BrandConcreteFrostResistancesList = new ObservableCollection<BrandConcreteFrostResistance>
			{
				new BrandConcreteFrostResistance { Name="F100", Value = "0"},
				new BrandConcreteFrostResistance { Name="F150", Value = "1" },
				new BrandConcreteFrostResistance { Name="F200" , Value = "2"},
				new BrandConcreteFrostResistance { Name="F300", Value = "3" },
			};

			HardeningConditionsesList = new ObservableCollection<HardeningConditions>
			{
				new HardeningConditions { Name="Естественные условия", Value = "0"},
				new HardeningConditions { Name="Тепловая обработка", Value = "1" },
			};
		}

		public ObservableCollection<BrandConcrete> BrandConcreteList { get; set; }
        public ObservableCollection<CoarseAggregate> CoarseAggregateList { get; set; }
        public ObservableCollection<FineAggregate> FineAggregateList { get; set; }
        public ObservableCollection<CementBrand> CementBrandList { get; set; }
        public ObservableCollection<MixtureMobility> MixtureMobilityList { get; set; }
        public ObservableCollection<Admixtures> AdmixturesList { get; set; }
        public ObservableCollection<BrandConcreteFrostResistance> BrandConcreteFrostResistancesList { get; set; }
        public ObservableCollection<HardeningConditions> HardeningConditionsesList { get; set; }
    }
}