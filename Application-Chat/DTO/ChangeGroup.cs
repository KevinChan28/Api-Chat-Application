using Application_Chat.Enums;

namespace Application_Chat.DTO
{
    public record ChangeGroup(
        string id,
        string Name,
        string Description,
        string Image,
        VisibilityType Visibility
        );
}
