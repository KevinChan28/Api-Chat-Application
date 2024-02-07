using Application_Chat.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application_Chat.DTO
{
    public class AddUserToGroupId
    {
        [Required]
        public string idGroup { get; set; }
        [Required]
        public DateTime JoinedDate { get; set; }
    }
}
