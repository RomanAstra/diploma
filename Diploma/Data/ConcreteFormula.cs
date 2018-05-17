using System;
using System.Collections.Generic;

namespace Diploma.Data
{
	[Serializable]
	public class ConcreteFormula
	{
		public ConcreteFormula()
		{
			//BrandConcreteList = new List<CurrentBrandConcrete>
			//{
			//	new CurrentBrandConcrete {Strength = "wewewew"},
			//	new CurrentBrandConcrete {Strength = "fdfdfd"},
			//	new CurrentBrandConcrete {Strength = "fdhdfhfcb"},
			//	new CurrentBrandConcrete {Strength = "dfgfdbvxcb"},

			//};
		}

		public List<BrandConcrete> BrandConcreteList { get; set; }
	}
}