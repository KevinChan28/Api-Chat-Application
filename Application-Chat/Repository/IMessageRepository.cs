using Application_Chat.Models;

namespace Application_Chat.Repository
{
	public interface IMessageRepository
	{
		Task Create(Message message);
		Task Update(Message message);
		Task<List<Message>> GetAllMessages();
		Task<Message> GetMessageById(string idMessage);
		Task Delete(Message message);
	}
}
