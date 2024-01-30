using Application_Chat.Context;
using Application_Chat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Application_Chat.Repository.Imp
{
	public class ImpUserRepository : IUserRepository
	{
		private readonly ChatStoreDatabaseSettings _settings;
		private readonly IMongoCollection<User> _users;

		public ImpUserRepository(IOptions<ChatStoreDatabaseSettings> databseOptions, IMongoDatabase mongoDatabase)
		{
			_settings = databseOptions.Value;
			_users = mongoDatabase.GetCollection<User>("User");
		}


		public async Task<string> CreateUser(User user)
		{
			await _users.InsertOneAsync(user);

			return user.Id;
		}

		public async Task DeleteUser(string idUser)
		{
			await _users.FindOneAndDeleteAsync(x => x.Id == idUser);
		}

		public async Task<List<User>> GetAllUsers()
		{
			return await _users.Find(_ => true).ToListAsync();
		}

		public async Task<User> GetUserById(string idUser)
		{
			return _users.Find(i => i.Id == idUser).FirstOrDefault();
		}

		public async Task<string> UpdateUser(User user)
		{
			_users.ReplaceOne(x => x.Id == user.Id, user);

			return user.Id;
		}
	}
}
