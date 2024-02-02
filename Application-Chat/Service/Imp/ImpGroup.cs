using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Repository;
using Application_Chat.Security;

namespace Application_Chat.Service.Imp
{
    public class ImpGroup : IGroup
    {
        private IGroupRepository _groupRepository;
        private JwtSettings _jwtSettings;

        public ImpGroup(IGroupRepository groupRepository, JwtSettings jwtSettings)
        {
            _groupRepository = groupRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> Create(CreateGroup model)
        {
            Group group = new Group
            {
                Name = model.Name,
                Image = model.Image,
                Description = model.Description
            };

            string idGroup = await _groupRepository.CreateGroup(group);
            return idGroup;
        }

        public async Task<bool> DeleteGroup(string idGroup)
        {
            Group groupFind = await _groupRepository.GetGroupById(idGroup);
            if(groupFind != null)
            {
                await _groupRepository.DeleteGroup(idGroup);

                return true;
            }

            return false;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _groupRepository.GetAllGroups();
        }

        public async Task<Group> GetGroupById(string idGroup)
        {
            Group group = await _groupRepository.GetGroupById(idGroup);
            if (group != null)
            {
                return group;
            }

            return null;
        }

        public async Task<string> UpdateGroup(ChangeGroup model)
        {
            Group group = await _groupRepository.GetGroupById(model.id);
            if (group != null)
            {
                group.Image = model.Image;
                group.Name = model.Name;
                group.Description = model.Description;
                string id = await _groupRepository.UpdateGroup(group);

                return id;
            }

            return null;
        }

    }
}
