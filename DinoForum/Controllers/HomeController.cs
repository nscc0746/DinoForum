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
            var discussions = await _context.Discussion.ToListAsync();

            return View(discussions);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
