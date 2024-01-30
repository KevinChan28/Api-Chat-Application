using System.ComponentModel.DataAnnotations;

namespace Application_Chat.DTO
{
	public record CreateUser(
		[Required]
		string Email,
		[Required]
		string Password,
		[Required]
		string UserName,
		string? Image
		);
}
