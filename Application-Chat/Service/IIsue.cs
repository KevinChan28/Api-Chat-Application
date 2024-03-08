using Application_Chat.DTO;

namespace Application_Chat.Service
{
	public interface IIsue
	{
		Task<string> AddUserToGroup(AddUserToGroup model);
		Task<List<ListGroups>> GetGroupsBelongUserCurrent();
		Task<string> AddUserByGroupId(AddUserToGroupId model);
		Task<(bool, string)> IsValidToAddGroup(AddUserToGroup model);
	}
}
