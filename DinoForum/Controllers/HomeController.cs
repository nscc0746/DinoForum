using System.Diagnostics;
using DinoForum.Data;
using DinoForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DinoForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly DinoForumContext _context;

        public HomeController(DinoForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion.Include(p => p.Comments).FirstOrDefaultAsync(p => p.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
