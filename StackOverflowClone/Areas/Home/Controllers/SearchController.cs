using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Data;
using StackOverflowClone.Models;

namespace StackOverflowClone.Areas.Home.Controllers
{
	[Area("Home")]
	public class SearchController(ApplicationDbContext context) : Controller
	{
		public IActionResult Index(string searchQuery, int pageNumber = 1)
		{
			ViewData["query"] = searchQuery;
			return View(GetPaginatedFullTextResponse(searchQuery, pageNumber));
		}

		public List<PostDto> GetPaginatedFullTextResponse(
			string query,
			int pageNumber = 1,
			int pageSize = 10)
		{
			int top_n = pageNumber * pageSize;

			return [.. context.Database.SqlQueryRaw<PostDto>(@"
				SELECT fts.Rank AS PostRank,
					p.Id AS PostId,
					p.Title AS PostTitle,
					p.Body AS PostDescription,
					COUNT(DISTINCT v.Id) AS TotalVotes,
					p.AnswerCount AS TotalAnswers,
					u.DisplayName AS UserName,
					u.Reputation AS UserReputation,
					STRING_AGG(CAST(b.Name AS NVARCHAR(MAX)), ', ') AS UserBadges
				FROM Posts p
				INNER JOIN (
					SELECT *
					FROM CONTAINSTABLE(Posts, (
								Title,
								Body
								), {0}, {1})
					) fts ON fts.[KEY] = p.Id
				LEFT JOIN Votes v ON v.PostId = p.Id
				INNER JOIN PostUsers u ON u.Id = p.OwnerUserId
				LEFT JOIN Badges b ON b.UserId = u.Id
				WHERE p.PostTypeId IN(1, 2)
				GROUP BY fts.Rank,
					p.Id,
					p.Title,
					p.Body,
					p.AnswerCount,
					u.DisplayName,
					u.Reputation
				ORDER BY fts.Rank DESC;
			", query, top_n)];
		}
	}

	public class PostDto
	{
		public int PostRank { get; set; }
		public int PostId { get; set; }
		public string? PostTitle { get; set; }

		public string? PostDescription { get; set; }

		public int? TotalVotes { get; set; }
		public int? TotalAnswers { get; set; }
		public string? UserName { get; set; }
		public int? UserReputation { get; set; }
		public string? UserBadges { get; set; }

	}
}
