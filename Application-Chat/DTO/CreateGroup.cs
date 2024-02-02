using Application_Chat.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application_Chat.DTO
{
	public record CreateGroup(
		[Required]
		string Name,
		string? Image,
		string? Description,
		[Required]
		VisibilityType Visibility,
		[Required]
		DateTime CreatedDate
		);
}
