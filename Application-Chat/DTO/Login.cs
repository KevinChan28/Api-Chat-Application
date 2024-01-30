using System.ComponentModel.DataAnnotations;

namespace Application_Chat.DTO
{
	public record Login(
		[Required]
		string User,
		[Required]
		string Password
		);
}
