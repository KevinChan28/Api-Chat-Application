using Application_Chat.DTO;
using Application_Chat.Hubs;
using Application_Chat.Models;
using Application_Chat.Repository;

namespace Application_Chat.Service.Imp
{
	public class ImpMessage : IMessage
	{
		private IMessageRepository _messageRepository;
		private IAuthorization _authorization;
		private readonly ChatHub _chatHub;
		private readonly IUserRepository _userRepository;

		public ImpMessage(IMessageRepository messageRepository, IAuthorization authorization, ChatHub chatHub, IUserRepository userRepository)
		{
			_messageRepository = messageRepository;
			_authorization = authorization;
			_chatHub = chatHub;
			_userRepository = userRepository;
		}


		public async Task<string> CreateMessage(CreateMessage message)
		{
			Message messageNew = new Message
			{
				Content = message.Text,
				SentDate = message.SendDate,
				GroupId = message.GroupId,
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

		public async Task SendMessage(SendMessageDTO sendMessage)
		{
			await _chatHub.SendMessage(sendMessage.IdUser, sendMessage.Message, sendMessage.idIssue);
		}

		public async Task<List<ListMessagesInfo>> GetMessagesOfGroup(string idGroup)
		{
			List<Message> messages = await _messageRepository.GetAllMessages();
			List<Message> messagesOfGroup = messages.Where(z => z.GroupId == idGroup).ToList();
			List<User> users = await _userRepository.GetAllUsers();

			var groupedMessages = messagesOfGroup
									.GroupBy(msg => msg.SentDate.Date)
									.OrderBy(group => group.Key)
									.ToList();

			List<ListMessagesInfo> messageInfo = groupedMessages.Select(group =>
			   new ListMessagesInfo
			   {
				   Date = group.Key,
				   Messages = group.Select(x => new InfoMessage
				   {
					   SendDate = x.SentDate.ToString("t"),
					   Content = x.Content,
					   IdMessage = x.Id,
					   NameUser = users.FirstOrDefault(z => z.Id == x.UserId)?.UserName
				   }).ToList()
			   }).ToList();

			return messageInfo;
		}
	}
}
