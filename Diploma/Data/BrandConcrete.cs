﻿using System;

namespace Diploma.Data
{
	[Serializable]
	public class BrandConcrete
	{
		private string _strength;
		public string Discription;

		public string Strength { get => _strength; set => _strength = value; }
	}
}