using MongoDB.Bson.Serialization.Attributes;

namespace Application_Chat.Models
{
	public class User
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonElement("Email")]
		public string Email { get; set; } = null!;
		[BsonElement("Password")]
		public string Password { get; set; } = null!;
		[BsonElement("UserName")]
		public string UserName { get; set; } = null!;
		[BsonElement("Image")]
		public string? Image { get; set; }
	}
}
