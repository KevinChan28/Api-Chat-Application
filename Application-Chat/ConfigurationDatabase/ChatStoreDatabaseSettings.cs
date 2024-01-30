namespace Application_Chat.Context
{
	public class ChatStoreDatabaseSettings : IMongoDBSettings
	{
		public string ConnectionString { get; set; } = null!;
		public string DatabaseName { get; set; } = null!;
	}
}
