namespace Application_Chat.DTO
{
	public record CreateMessage(
		string Text,
		DateTime SendDate,
		string GroupId
		);

}
