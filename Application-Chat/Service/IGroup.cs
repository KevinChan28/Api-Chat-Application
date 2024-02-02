using Application_Chat.DTO;
using Application_Chat.Models;

namespace Application_Chat.Service
{
    public interface IGroup
    {
        Task<string> Create(CreateGroup model);
        Task<List<Group>> GetAllGroups();
        Task<string> UpdateGroup(ChangeGroup group);
        Task<bool> DeleteGroup(string idGroup);
        Task<Group> GetGroupById(string idGroup);
    }
}
