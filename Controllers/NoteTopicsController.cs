using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnhancedBlogger.Models;
using EnhancedBlogger.ViewModels;

namespace EnhancedBlogger.Controllers
{
    public class NoteTopicsController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public NoteTopicsController(EnhancedBloggerContext context)
        {
            _context = context;
        }

        // GET: NoteTopics
        public IActionResult  APersonalLogViewModel()
        {   // need to dewfine instances that are the equivalent here 
            // of the following that are in the View
            /*  logListGroups
                itemsListInGroup
                logListNote            
             */
            APersonalLogViewModel personalLog = new APersonalLogViewModel();
            
            return View(personalLog);
        }

        // GET: NoteTopics
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.NoteTopic.Include(n => n.UserProfile);
            return View(await enhancedBloggerContext.ToListAsync());
        }

        // GET: NoteTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteTopic = await _context.NoteTopic
                .Include(n => n.UserProfile)
                .FirstOrDefaultAsync(m => m.NoteTopicId == id);
            if (noteTopic == null)
            {
                return NotFound();
            }

            return View(noteTopic);
        }

        // GET: NoteTopics/Create
        public IActionResult Create()
        {
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio");
            return View();
        }

        // POST: NoteTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteTopicId,UserProfileId,NoteTopicTitle,NoteTopicComment,TopicCreateDate")] NoteTopic noteTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noteTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", noteTopic.UserProfileId);
            return View(noteTopic);
        }

        // GET: NoteTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteTopic = await _context.NoteTopic.FindAsync(id);
            if (noteTopic == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", noteTopic.UserProfileId);
            return View(noteTopic);
        }

        // POST: NoteTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteTopicId,UserProfileId,NoteTopicTitle,NoteTopicComment,TopicCreateDate")] NoteTopic noteTopic)
        {
            if (id != noteTopic.NoteTopicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noteTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteTopicExists(noteTopic.NoteTopicId))
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
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", noteTopic.UserProfileId);
            return View(noteTopic);
        }

        // GET: NoteTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteTopic = await _context.NoteTopic
                .Include(n => n.UserProfile)
                .FirstOrDefaultAsync(m => m.NoteTopicId == id);
            if (noteTopic == null)
            {
                return NotFound();
            }

            return View(noteTopic);
        }

        // POST: NoteTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noteTopic = await _context.NoteTopic.FindAsync(id);
            _context.NoteTopic.Remove(noteTopic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteTopicExists(int id)
        {
            return _context.NoteTopic.Any(e => e.NoteTopicId == id);
        }
    }
}
