using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Diploma.Helper
{
	public static class ExtensionMethod
	{
		public static T DeepCopy<T>(this T self)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("Type must be iserializable");
			}
			if (ReferenceEquals(self, null))
			{
				return default(T);
			}

			var formatter = new BinaryFormatter();
			using (var stream = new MemoryStream())
			{
				formatter.Serialize(stream, self);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}