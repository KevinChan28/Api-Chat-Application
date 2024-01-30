using MongoDB.Bson.Serialization.Attributes;

namespace Application_Chat.Models
{
	public class Message
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public int Id { get; set; }
		public string Content { get; set; } = null!;
		public DateTime SendDate { get; set; }
		public string UserId { get; set; } = null!;
		public string IssueId { get; set; } = null!;
	}
}
