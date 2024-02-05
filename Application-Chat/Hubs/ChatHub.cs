using Microsoft.AspNetCore.SignalR;

namespace Application_Chat.Hubs
{
	//[Authorize]
	public class ChatHub : Hub
	{
		public async Task SendMessage(string userId, string message, string issueId)
		{
			try
			{
				await Clients.Group(issueId).SendAsync("ReceiveMessage", userId, message);
			}
			catch (Exception ex)
			{

				Console.WriteLine($"Error in SendMessage: {ex.Message}"); ;
			}
		}

		public async Task AddToGroup(string group)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, group);
			await Clients.Group(group).SendAsync("ShowWho", $"Alguien se conectó {Context.ConnectionId}");
		}
	}
}
