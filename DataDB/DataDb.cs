using System;
using System.IO;
using System.Xml.Serialization;

namespace DataDB
{
	public class DataDb<T> : IData<T>
	{
		private readonly string _path;
		private readonly XmlSerializer _xmlSerializer;

		public DataDb(string connection)
		{
			_xmlSerializer = new XmlSerializer(typeof(T));
			_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{connection}.xml");
		}

		public void Save(T arg)
		{
			if (!typeof(T).IsSerializable) throw new ArgumentException("Type must be iserializable");
			using (var fs = new FileStream(_path, FileMode.Create))
			{
				_xmlSerializer.Serialize(fs, arg);
			}
		}

		public T Load()
		{
			T result;
			if (!typeof(T).IsSerializable) throw new ArgumentException("Type must be iserializable");
			if (!File.Exists(_path)) return default(T);//throw new Exception("File not found");
			using (var fs = new FileStream(_path, FileMode.Open))
			{
				result = (T)_xmlSerializer.Deserialize(fs);
			}
			return result;
		}
	}
}
