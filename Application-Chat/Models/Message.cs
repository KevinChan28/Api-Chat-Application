using MongoDB.Bson.Serialization.Attributes;

namespace Application_Chat.Models
{
	public class Message
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string Id { get; set; }
		[BsonElement("Content")]
		public string Content { get; set; } = null!;
		[BsonElement("SentDate")]
		public DateTime SentDate { get; set; }
		[BsonElement("UserId")]
		public string UserId { get; set; } = null!;
		[BsonElement("IssueId")]
		public string IssueId { get; set; } = null!;
	}
}
