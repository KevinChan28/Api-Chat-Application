using Application_Chat.DTO;
using Application_Chat.Models;

namespace Application_Chat.Service
{
	public interface IUser
	{
		Task<string> Create(CreateUser model);
		Task<List<User>> GetAllUsers();
		Task<bool> Delete(string idUser);
		Task<User> GetUserById(string idUser);
		Task<string> UpdateNameOfUser(string nameUserNew, string idUser);
	}
}
