﻿namespace Application_Chat.Security
{
	public class UserTokens
	{
		public string Token { get; set; }
		public string UserName { get; set; }
		public TimeSpan Validty { get; set; }
		public string RefreshToken { get; set; }
		public string Id { get; set; }
		public string Email { get; set; }
		public Guid GuidId { get; set; }
		public DateTime ExpiredTime { get; set; }
		public string Rol { get; set; }
	}
}
