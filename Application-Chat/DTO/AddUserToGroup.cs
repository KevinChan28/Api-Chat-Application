using Application_Chat.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application_Chat.DTO
{
	public class AddUserToGroup
	{
		[Required]
		public string email { get; set; }
		[Required]
		public string idGroup { get; set; }
		[Required]
		public DateTime JoinedDate { get; set; }
		[Required]
		public Roles Rol { get; set; }
	}
}
