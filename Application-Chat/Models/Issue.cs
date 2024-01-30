using Application_Chat.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Application_Chat.Models
{
	public class Issue
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public int Id { get; set; }
		public string UserId { get; set; } = null!;
		public string GroupId { get; set; } = null!;
		public Roles Rol { get; set; }
		public DateTime JoinedDate { get; set; }
	}
}
