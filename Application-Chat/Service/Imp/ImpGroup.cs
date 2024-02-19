using Application_Chat.DTO;
using Application_Chat.Enums;
using Application_Chat.Models;
using Application_Chat.Repository;

namespace Application_Chat.Service.Imp
{
	public class ImpGroup : IGroup
	{
		private IGroupRepository _groupRepository;
		private IAuthorization _authorization;
		private IIssueRepository _issue;

		public ImpGroup(IGroupRepository groupRepository, IAuthorization authorization, IIssueRepository issue)
		{
			_groupRepository = groupRepository;
			_authorization = authorization;
			_issue = issue;
		}

		public async Task<string> Create(CreateGroup model)
		{
			Group group = new Group
			{
				Name = model.Name,
				Image = model.Image,
				Description = model.Description,
				CreatedDate = model.CreatedDate,
				Visibility = model.Visibility,
				Owner = _authorization.UserCurrent()
			};

			string idGroup = await _groupRepository.CreateGroup(group);

			Issue issue = new Issue
			{
				GroupId = group.Id,
				JoinedDate = model.CreatedDate,
				Rol = Enums.Roles.Administrator,
				UserId = _authorization.UserCurrent(),
			};

			await _issue.CreateIssue(issue);

			return idGroup;
		}

		public async Task<bool> DeleteGroup(string idGroup)
		{
			Group groupFind = await _groupRepository.GetGroupById(idGroup);

			if (groupFind != null)
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

        public async Task<string> UpdateDescriptionOfGroup(string desGroupNew, string idGroup)
        {
            Group group = await _groupRepository.GetGroupById(idGroup);
            if (group != null)
            {
                group.Description = desGroupNew;
                string idUpdateGroup = await _groupRepository.UpdateGroup(group);

                return idUpdateGroup;
            }

            return null;
        }

        public async Task<string> UpdateImagenOfGroup(string imageGroupNew, string idGroup)
        {
            Group group = await _groupRepository.GetGroupById(idGroup);
            if (group != null)
            {
                group.Image = imageGroupNew;
                string idUpdateGroup = await _groupRepository.UpdateGroup(group);

                return idUpdateGroup;
            }

            return null;
        }

        public async Task<string> UpdateNameOfGroup(string nameGroupNew, string idGroup)
        {
			Group group = await _groupRepository.GetGroupById(idGroup);
			if(group != null)
			{
				group.Name = nameGroupNew;
				string idUpdateGroup = await _groupRepository.UpdateGroup(group);

				return idUpdateGroup;
			}

			return null;
        }

        public async Task<string> UpdateVisibilityOfGroup(VisibilityType VisGroupNew, string idGroup)
        {
            Group group = await _groupRepository.GetGroupById(idGroup);
            if (group != null)
            {
                group.Visibility = VisGroupNew;
                string idUpdateGroup = await _groupRepository.UpdateGroup(group);

                return idUpdateGroup;
            }

            return null;
        }

        public async Task<List<Group>> GetAllPublicGroups()
        {
			List<Group> Groups = await _groupRepository.GetAllGroups();
            List<Group> PublicGroups = Groups.Where(z => z.Visibility == (Enums.VisibilityType)0).ToList();

			return PublicGroups; 
        }
    }
}
