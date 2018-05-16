using System;
using System.Collections.Generic;

namespace Diploma.Data
{
	[Serializable]
	public class ConcreteFormula
	{
		private List<BrandConcrete> BrandConcretes;

		public ConcreteFormula()
		{
			BrandConcreteList = new List<BrandConcrete>
			{
				new BrandConcrete {Strength = "wewewew"},
				new BrandConcrete {Strength = "fdfdfd"},
				new BrandConcrete {Strength = "fdhdfhfcb"},
				new BrandConcrete {Strength = "dfgfdbvxcb"},

			};
		}

		public List<BrandConcrete> BrandConcreteList
		{
			get { return BrandConcretes; }
			set { BrandConcretes = value; }
		}
	}
}