using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DinoForum.Data;
using DinoForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DinoForum.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly DinoForumContext _context;
        private readonly UserManager<DinoForumUser> _userManager;

        public CommentsController(DinoForumContext context, UserManager<DinoForumUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["DiscussionId"] = id;

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,CreateDate,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.DinoForumUserId = _userManager.GetUserId(User);
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }

            return View(comment);
        }
    }
}
