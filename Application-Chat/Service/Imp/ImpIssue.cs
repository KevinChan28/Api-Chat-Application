using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Repository;

namespace Application_Chat.Service.Imp
{
	public class ImpIssue : IIsue
	{
		private readonly IIssueRepository _issueRepository;
		private readonly IUserRepository _userRepository;

		public ImpIssue(IIssueRepository issueRepository, IUserRepository user)
		{
			_issueRepository = issueRepository;
			_userRepository = user;
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
	}
}
