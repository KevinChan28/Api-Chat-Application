using Application_Chat.DTO;
using Application_Chat.Enums;
using Application_Chat.Models;

namespace Application_Chat.Service
{
    public interface IGroup
    {
        Task<string> Create(CreateGroup model);
        Task<List<Group>> GetAllGroups();
        Task<string> UpdateNameOfGroup(string nameGroupNew, string idGroup);
        Task<string> UpdateDescriptionOfGroup(string desGroupNew, string idGroup);
        Task<string> UpdateImagenOfGroup(string imageGroupNew, string idGroup);
        Task<string> UpdateVisibilityOfGroup(VisibilityType visGroupNew, string idGroup);
        Task<bool> DeleteGroup(string idGroup);
        Task<Group> GetGroupById(string idGroup);
    }
}
