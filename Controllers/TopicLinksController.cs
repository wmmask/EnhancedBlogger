using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnhancedBlogger.Models;

namespace EnhancedBlogger.Controllers
{
    public class TopicLinksController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public TopicLinksController(EnhancedBloggerContext context)
        {
            _context = context;
        }

        // GET: TopicLinks
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.TopicLink.Include(t => t.TopicItem);
            return View(await enhancedBloggerContext.ToListAsync());
        }

        // GET: TopicLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicLink = await _context.TopicLink
                .Include(t => t.TopicItem)
                .FirstOrDefaultAsync(m => m.TopicLinkId == id);
            if (topicLink == null)
            {
                return NotFound();
            }

            return View(topicLink);
        }

        // GET: TopicLinks/Create
        public IActionResult Create()
        {
            ViewData["TopicItemId"] = new SelectList(_context.TopicItem, "TopicItemId", "TopicItemId");
            return View();
        }

        // POST: TopicLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicLinkId,TopicItemId,TopicLinkRemark,LinkCreateDate")] TopicLink topicLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicItemId"] = new SelectList(_context.TopicItem, "TopicItemId", "TopicItemId", topicLink.TopicItemId);
            return View(topicLink);
        }

        // GET: TopicLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicLink = await _context.TopicLink.FindAsync(id);
            if (topicLink == null)
            {
                return NotFound();
            }
            ViewData["TopicItemId"] = new SelectList(_context.TopicItem, "TopicItemId", "TopicItemId", topicLink.TopicItemId);
            return View(topicLink);
        }

        // POST: TopicLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicLinkId,TopicItemId,TopicLinkRemark,LinkCreateDate")] TopicLink topicLink)
        {
            if (id != topicLink.TopicLinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicLinkExists(topicLink.TopicLinkId))
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
            ViewData["TopicItemId"] = new SelectList(_context.TopicItem, "TopicItemId", "TopicItemId", topicLink.TopicItemId);
            return View(topicLink);
        }

        // GET: TopicLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicLink = await _context.TopicLink
                .Include(t => t.TopicItem)
                .FirstOrDefaultAsync(m => m.TopicLinkId == id);
            if (topicLink == null)
            {
                return NotFound();
            }

            return View(topicLink);
        }

        // POST: TopicLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topicLink = await _context.TopicLink.FindAsync(id);
            _context.TopicLink.Remove(topicLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicLinkExists(int id)
        {
            return _context.TopicLink.Any(e => e.TopicLinkId == id);
        }
    }
}
