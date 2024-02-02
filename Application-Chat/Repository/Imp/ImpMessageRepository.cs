using Application_Chat.Context;
using Application_Chat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Application_Chat.Repository.Imp
{
	public class ImpMessageRepository : IMessageRepository
	{
		private readonly ChatStoreDatabaseSettings _settings;
		private readonly IMongoCollection<Message> _messages;

		public ImpMessageRepository(IOptions<ChatStoreDatabaseSettings> settings, IMongoDatabase messages)
		{
			_settings = settings.Value;
			_messages = messages.GetCollection<Message>("Message");
		}


		public async Task Create(Message message)
		{
			await _messages.InsertOneAsync(message);
		}

		public async Task Delete(Message message)
		{
			await _messages.DeleteOneAsync(i => i.Id == message.Id);
		}

		public async Task<List<Message>> GetAllMessages()
		{
			try
			{
				return await _messages.Find(_ => true).ToListAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<Message> GetMessageById(string idMessage)
		{
			return _messages.Find(x => x.Id == idMessage).FirstOrDefault();
		}

		public async Task Update(Message message)
		{
			await _messages.ReplaceOneAsync(z => z.Id == message.Id, message);
		}
	}
}
