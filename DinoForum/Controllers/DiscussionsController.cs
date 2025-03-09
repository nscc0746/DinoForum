using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DinoForum.Data;
using DinoForum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DinoForum.Controllers
{
    [Authorize]
    public class DiscussionsController : Controller
    {
        private readonly DinoForumContext _context;
        private readonly UserManager<DinoForumUser> _userManager;

        public DiscussionsController(DinoForumContext context, UserManager<DinoForumUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Discussions by user id
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);

            var posts = await _context.Discussion
                .Where(p => p.DinoForumUserId == userId)
                .ToListAsync();

            return View(posts);
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionId,Title,Content,ImageFile,CreateDate")] Discussion discussion)
        {

            if (ModelState.IsValid)
            {
                //Save the image file first, then save the record
                if (discussion.ImageFile != null)
                {
                    discussion.DinoForumUserId = _userManager.GetUserId(User);
                    discussion.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", discussion.ImageFilename);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussion.ImageFile.CopyToAsync(fileStream);
                    }
                }

                _context.Add(discussion);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetDiscussion", "Home", new { id = discussion.DiscussionId });
            }

            return View(discussion);
        }

        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = _userManager.GetUserId(User);

            var discussion = await _context.Discussion
                .Where(p => p.DinoForumUserId == userId)
                .FirstOrDefaultAsync(p => p.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // POST: Discussions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,ImageFilename,CreateDate,DinoForumUserId")] Discussion discussion)
        {
            string userId = _userManager.GetUserId(User);

            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.DiscussionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = _userManager.GetUserId(User);

            var discussion = await _context.Discussion
                .Where(p => p.DinoForumUserId == userId)
                .FirstOrDefaultAsync(p => p.DiscussionId == id); 
            
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = _userManager.GetUserId(User);

            var discussion = await _context.Discussion
                .Where(p => p.DinoForumUserId == userId)
                .FirstOrDefaultAsync(p => p.DiscussionId == id);

            if (discussion != null)
            {
                _context.Discussion.Remove(discussion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id);
        }
    }
}
