﻿using Application_Chat.Models;

namespace Application_Chat.Repository
{
	public interface IUserRepository
	{
		Task<string> CreateUser(User user);
		Task<List<User>> GetAllUsers();
		Task<User> GetUserById(string idUser);
		Task DeleteUser(string idUser);
		Task<string> UpdateUser(User user);
	}
}
