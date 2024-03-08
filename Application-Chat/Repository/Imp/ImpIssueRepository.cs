using Application_Chat.Context;
using Application_Chat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Application_Chat.Repository.Imp
{
	public class ImpIssueRepository : IIssueRepository
	{
		private readonly ChatStoreDatabaseSettings _settings;
		private readonly IMongoCollection<Issue> _issues;

		public ImpIssueRepository(IOptions<ChatStoreDatabaseSettings> databaseOptions, IMongoDatabase mongoDatabase)
		{
			_settings = databaseOptions.Value;
			_issues = mongoDatabase.GetCollection<Issue>("Issue");
		}


		public async Task CreateIssue(Issue issue)
		{
			await _issues.InsertOneAsync(issue);
		}

		public async Task<bool> ExistUserInGroup(string idUser, string idGroup)
		{
			FilterDefinition<Issue> filter = Builders<Issue>.Filter.And(
			Builders<Issue>.Filter.Eq("GroupId", idGroup),
			Builders<Issue>.Filter.Eq("UserId", idUser));

			Issue existingIssue = await _issues.Find(filter).FirstOrDefaultAsync();

			return existingIssue != null;
		}

		public async Task<List<Issue>> GetIssuesOfUser(string idUser)
		{
			try
			{
				return await _issues.Find(x => x.UserId == idUser).ToListAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
