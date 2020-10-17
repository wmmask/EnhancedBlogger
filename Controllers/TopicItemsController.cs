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
    public class TopicItemsController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public TopicItemsController(EnhancedBloggerContext context)
        {
            _context = context;
        }

        // GET: TopicItems
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.TopicItem.Include(t => t.BlogPost).Include(t => t.NoteTopic);
            return View(await enhancedBloggerContext.ToListAsync());
        }

        // GET: TopicItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicItem = await _context.TopicItem
                .Include(t => t.BlogPost)
                .Include(t => t.NoteTopic)
                .FirstOrDefaultAsync(m => m.TopicItemId == id);
            if (topicItem == null)
            {
                return NotFound();
            }

            return View(topicItem);
        }

        // GET: TopicItems/Create
        public IActionResult Create()
        {
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "BlogPostId", "BlogText");
            ViewData["NoteTopicId"] = new SelectList(_context.NoteTopic, "NoteTopicId", "NoteTopicComment");
            return View();
        }

        // POST: TopicItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicItemId,NoteTopicId,BlogPostId,TopicItemThought,ItemCreateDate")] TopicItem topicItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "BlogPostId", "BlogText", topicItem.BlogPostId);
            ViewData["NoteTopicId"] = new SelectList(_context.NoteTopic, "NoteTopicId", "NoteTopicComment", topicItem.NoteTopicId);
            return View(topicItem);
        }

        // GET: TopicItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicItem = await _context.TopicItem.FindAsync(id);
            if (topicItem == null)
            {
                return NotFound();
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "BlogPostId", "BlogText", topicItem.BlogPostId);
            ViewData["NoteTopicId"] = new SelectList(_context.NoteTopic, "NoteTopicId", "NoteTopicComment", topicItem.NoteTopicId);
            return View(topicItem);
        }

        // POST: TopicItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicItemId,NoteTopicId,BlogPostId,TopicItemThought,ItemCreateDate")] TopicItem topicItem)
        {
            if (id != topicItem.TopicItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicItemExists(topicItem.TopicItemId))
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
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "BlogPostId", "BlogText", topicItem.BlogPostId);
            ViewData["NoteTopicId"] = new SelectList(_context.NoteTopic, "NoteTopicId", "NoteTopicComment", topicItem.NoteTopicId);
            return View(topicItem);
        }

        // GET: TopicItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicItem = await _context.TopicItem
                .Include(t => t.BlogPost)
                .Include(t => t.NoteTopic)
                .FirstOrDefaultAsync(m => m.TopicItemId == id);
            if (topicItem == null)
            {
                return NotFound();
            }

            return View(topicItem);
        }

        // POST: TopicItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topicItem = await _context.TopicItem.FindAsync(id);
            _context.TopicItem.Remove(topicItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicItemExists(int id)
        {
            return _context.TopicItem.Any(e => e.TopicItemId == id);
        }
    }
}
