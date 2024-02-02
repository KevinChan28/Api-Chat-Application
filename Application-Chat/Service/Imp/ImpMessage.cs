using Application_Chat.DTO;
using Application_Chat.Models;
using Application_Chat.Repository;

namespace Application_Chat.Service.Imp
{
	public class ImpMessage : IMessage
	{
		private IMessageRepository _messageRepository;
		private IAuthorization _authorization;

		public ImpMessage(IMessageRepository messageRepository, IAuthorization authorization)
		{
			_messageRepository = messageRepository;
			_authorization = authorization;
		}


		public async Task<string> CreateMessage(SendMessage message)
		{
			Message messageNew = new Message
			{
				Content = message.Text,
				SentDate = message.SendDate,
				IssueId = message.IssueId,
				UserId = _authorization.UserCurrent()
			};

			await _messageRepository.Create(messageNew);

			return messageNew.Id;
		}

		public async Task<bool> DeleteMessage(string idMessage)
		{
			Message message = await _messageRepository.GetMessageById(idMessage);

			if (message != null)
			{
				await _messageRepository.Delete(message);

				return true;
			}

			return false;
		}

		public async Task<List<Message>> GetAllMessages()
		{
			return await _messageRepository.GetAllMessages();
		}

		public async Task<string> ChangeContentMessage(string newText, string idMessage)
		{
			Message message = await _messageRepository.GetMessageById(idMessage);

			if (message != null)
			{
				message.Content = newText;

				await _messageRepository.Update(message);

				return message.Id;
			}

			return null;
		}
	}
}
