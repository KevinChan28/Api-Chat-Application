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
	}
}
