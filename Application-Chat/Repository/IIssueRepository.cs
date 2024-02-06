﻿using Application_Chat.Models;

namespace Application_Chat.Repository
{
	public interface IIssueRepository
	{
		Task CreateIssue(Issue issue);
		Task<List<Issue>> GetIssuesOfUser(string idUser);
	}
}