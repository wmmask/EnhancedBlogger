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
    public class TopicKeywordsController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public TopicKeywordsController(EnhancedBloggerContext context)
        {
            _context = context;
        }

        // GET: TopicKeywords
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.TopicKeyword.Include(t => t.TopicItem);
            return View(await enhancedBloggerContext.ToListAsync());
        }

        // GET: TopicKeywords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicKeyword = await _context.TopicKeyword
                .Include(t => t.TopicItem)
                .FirstOrDefaultAsync(m => m.TopicKeywordId == id);
            if (topicKeyword == null)
            {
                return NotFound();
            }

            return View(topicKeyword);
        }

        // GET: TopicKeywords/Create
        public IActionResult Create()
        {
            ViewData["TopicItemId"] = new SelectList(_context.TopicItem, "TopicItemId", "TopicItemId");
            return View();
        }

        // POST: TopicKeywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicKeywordId,TopicItemId,TopicKeywordName,TopicKeywordRemark,KeywordCreateDate")] TopicKeyword topicKeyword)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicKeyword);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicItemId"] = new SelectList(_context.TopicItem, "TopicItemId", "TopicItemId", topicKeyword.TopicItemId);
            return View(topicKeyword);
        }

        // GET: TopicKeywords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicKeyword = await _context.TopicKeyword.FindAsync(id);
            if (topicKeyword == null)
            {
                return NotFound();
            }
            ViewData["TopicItemId"] = new SelectList(_context.TopicItem, "TopicItemId", "TopicItemId", topicKeyword.TopicItemId);
            return View(topicKeyword);
        }

        // POST: TopicKeywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicKeywordId,TopicItemId,TopicKeywordName,TopicKeywordRemark,KeywordCreateDate")] TopicKeyword topicKeyword)
        {
            if (id != topicKeyword.TopicKeywordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicKeyword);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicKeywordExists(topicKeyword.TopicKeywordId))
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
            ViewData["TopicItemId"] = new SelectList(_context.TopicItem, "TopicItemId", "TopicItemId", topicKeyword.TopicItemId);
            return View(topicKeyword);
        }

        // GET: TopicKeywords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicKeyword = await _context.TopicKeyword
                .Include(t => t.TopicItem)
                .FirstOrDefaultAsync(m => m.TopicKeywordId == id);
            if (topicKeyword == null)
            {
                return NotFound();
            }

            return View(topicKeyword);
        }

        // POST: TopicKeywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topicKeyword = await _context.TopicKeyword.FindAsync(id);
            _context.TopicKeyword.Remove(topicKeyword);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicKeywordExists(int id)
        {
            return _context.TopicKeyword.Any(e => e.TopicKeywordId == id);
        }
    }
}
