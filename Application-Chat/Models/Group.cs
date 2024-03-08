using Application_Chat.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Application_Chat.Models
{
	public class Group
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; } = null!;
		public string? Image { get; set; }
		public DateTime CreatedDate { get; set; }
		public VisibilityType Visibility { get; set; }
		public string Owner { get; set; } = null!;
	}
}
