using Application_Chat.Models;

namespace Application_Chat.Repository
{
    public interface IGroupRepository
    {
        Task<string> CreateGroup(Group group);
        Task<List<Group>> GetAllGroups();
        Task<string> UpdateGroup(Group group);
        Task DeleteGroup(string idGroup);
        Task<Group> GetGroupById(string idGroup);

    }
}
