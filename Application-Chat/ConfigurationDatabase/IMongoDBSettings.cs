namespace Application_Chat.Context
{
	public interface IMongoDBSettings
	{
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}
