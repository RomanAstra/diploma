namespace DataDB
{
	public interface IData<T>
	{
		void Save(T mass);
		T Load();
	}
}