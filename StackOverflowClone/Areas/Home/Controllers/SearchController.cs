using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Data;
using StackOverflowClone.Models;

namespace StackOverflowClone.Areas.Home.Controllers
{

    [Area("Home")]
    public class SearchController(ApplicationDbContext context) : Controller
    {
        public IActionResult Index(string query, int lastPost)
        {
            IQueryable<Post> filteredPosts = GetPaginatedFullTextResponse(query, lastPost);
            List<Post> result = [.. filteredPosts];
            return View(result);
        }

        public IQueryable<Post> GetPaginatedFullTextResponse(
            string query,
            int lastPostId,
            int pageSize = 10)
        {
            Post? lastPost = context.Posts.FirstOrDefault(p => p.Id == lastPostId);

            if (lastPost == null)
            {
                return context.Posts
                    .Where(e => EF.Functions.Contains(e.Title, $"\"{query}\"") || EF.Functions.Contains(e.Body, $"\"{query}\""))
                    .OrderByDescending(e => e.CreationDate)
                    .ThenByDescending(e => e.FavoriteCount)
                    .ThenBy(e => e.Id)
                    .Take(pageSize);
            }

            return context.Posts
                    .Where(e => EF.Functions.Contains(e.Title, $"\"{query}\"") || EF.Functions.Contains(e.Body, $"\"{query}\""))
                    .OrderByDescending(e => e.CreationDate)
                    .ThenByDescending(e => e.FavoriteCount)
                    .ThenBy(e => e.Id)
                    .Where(e => e.CreationDate < lastPost.CreationDate || e.CreationDate == lastPost.CreationDate && e.Id > lastPost.Id)
                    .Take(pageSize);
        }

    }
}
