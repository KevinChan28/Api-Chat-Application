using System.Text.Json.Serialization;

namespace Application_Chat.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum VisibilityType
	{
		Public, Private
	}
}
