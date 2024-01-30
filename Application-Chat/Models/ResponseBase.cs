namespace Application_Chat.Models
{
	public class ResponseBase
	{
		public string Message { get; set; }
		public bool Success { get; set; }
		public object Data { get; set; }
	}
}
