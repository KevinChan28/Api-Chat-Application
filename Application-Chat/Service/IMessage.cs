using Application_Chat.DTO;
using Application_Chat.Models;

namespace Application_Chat.Service
{
	public interface IMessage
	{
		Task<string> CreateMessage(CreateMessage message);
		Task<List<Message>> GetAllMessages();
		Task<string> ChangeContentMessage(string newText, string idMessage);
		Task<bool> DeleteMessage(string idMessage);
		Task SendMessage(SendMessageDTO sendMessage);
	}
}
