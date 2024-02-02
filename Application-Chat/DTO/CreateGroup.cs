using System.ComponentModel.DataAnnotations;

namespace Application_Chat.DTO
{
    public record CreateGroup(
        [Required]
        string Name,
        string? Image,
        string? Description
        );
}
