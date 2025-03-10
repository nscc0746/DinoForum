using System.Diagnostics;
using DinoForum.Data;
using DinoForum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DinoForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly DinoForumContext _context;
        private readonly UserManager<DinoForumUser> _userManager;

        public HomeController(DinoForumContext context, UserManager<DinoForumUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .Include(p => p.Comments)
                .Include(p => p.DinoForumUser)
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

            var discussion = await _context.Discussion
                .Include(p => p.DinoForumUser)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.DinoForumUser)
                .FirstOrDefaultAsync(p => p.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        public async Task<IActionResult> Profile(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            //Get the users posts
            var discussions = await _context.Discussion
                .Include(p => p.Comments)
                .Where(p => p.DinoForumUserId == id)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();

            var viewData = new
            {
                User = user,
                Posts = discussions
            };

            if (user == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
