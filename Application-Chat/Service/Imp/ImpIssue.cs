using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Repository;

namespace Application_Chat.Service.Imp
{
	public class ImpIssue : IIsue
	{
		private readonly IIssueRepository _issueRepository;
		private readonly IUserRepository _userRepository;
		private readonly IGroupRepository _groupRepository;
		private readonly IAuthorization _authorization;

		public ImpIssue(IIssueRepository issueRepository, IUserRepository user, IGroupRepository groupRepository, IAuthorization authorization)
		{
			_issueRepository = issueRepository;
			_userRepository = user;
			_groupRepository = groupRepository;
			_authorization = authorization;
		}


		public async Task<string> AddUserToGroup(AddUserToGroup model)
		{
			User userFind = await _userRepository.GetUserByEmail(model.email);

			if (userFind != null)
			{
				Issue issue = new Issue
				{
					UserId = userFind.Id,
					GroupId = model.idGroup,
					JoinedDate = model.JoinedDate,
					Rol = model.Rol
				};

				await _issueRepository.CreateIssue(issue);

				return issue.Id;
			}

			return null;
		}

		public async Task<string> AddUserToGroupId(AddUserToGroupId model)
		{
			Group groupFind = await _groupRepository.GetGroupById(model.idGroup);

			if (groupFind != null)
			{
				Issue issue = new Issue
				{
					UserId = _authorization.UserCurrent(),
					GroupId = model.idGroup,
					JoinedDate = model.JoinedDate,
					Rol = (Enums.Roles)3
				};

				await _issueRepository.CreateIssue(issue);

				return issue.Id;
			}

			return null;
		}

		public async Task<List<ListGroups>> GetGroupsBelongUserCurrent()
		{
			string idUser = _authorization.UserCurrent();
			List<Issue> issuesOfUser = await _issueRepository.GetIssuesOfUser(idUser);
			List<Group> groups = await _groupRepository.GetAllGroups();
			List<ListGroups> listGroups = issuesOfUser.Select(x => new ListGroups
			{
				idIssue = x.Id,
				NameGroup = groups.Where(c => c.Id == x.GroupId).Select(a => a.Name).FirstOrDefault(),
				Image = groups.Where(z => z.Id == x.GroupId).Select(n => n.Image).FirstOrDefault(),
				idGroup = x.GroupId

			}).ToList();

			return listGroups;
		}
	}
}
