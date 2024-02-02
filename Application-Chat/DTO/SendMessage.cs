namespace Application_Chat.DTO
{
	public record SendMessage(
		string Text,
		DateTime SendDate,
		string IssueId
		);

}
