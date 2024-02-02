using Application_Chat.Context;
using Application_Chat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Application_Chat.Repository.Imp
{
    public class ImpGroupRepository : IGroupRepository
    {
        private readonly ChatStoreDatabaseSettings _settings;
        private readonly IMongoCollection<Group> _groups;

        public ImpGroupRepository(IOptions<ChatStoreDatabaseSettings> databaseOptions, IMongoDatabase mongoDatabase
            )
        {
            _settings = databaseOptions.Value;
            _groups = mongoDatabase.GetCollection<Group>("Group");
        }
        public async Task<string> CreateGroup(Group group)
        {
            await _groups.InsertOneAsync (group);

            return group.Id;
        }

        public async Task DeleteGroup(string idGroup)
        {
            await _groups.FindOneAndDeleteAsync(x => x.Id == idGroup);
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _groups.Find(_=> true).ToListAsync();
        }

        public async Task<Group> GetGroupById(string idGroup)
        {
            return _groups.Find(x => x.Id == idGroup).FirstOrDefault();
        }

        public async Task<string> UpdateGroup(Group group)
        {
            _groups.ReplaceOne(x => x.Id == group.Id, group);

            return group.Id;
        }
    }
}
