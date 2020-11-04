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
    public class GroupedItemsController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public GroupedItemsController(EnhancedBloggerContext context)
        {
            _context = context;
        }

        // GET: GroupedItems
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.GroupedItem.Include(g => g.BlogPost).Include(g => g.LogGrouping);
            return View(await enhancedBloggerContext.ToListAsync());
        }

        // GET: GroupedItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupedItem = await _context.GroupedItem
                .Include(g => g.BlogPost)
                .Include(g => g.LogGrouping)
                .FirstOrDefaultAsync(m => m.GroupedItemId == id);
            if (groupedItem == null)
            {
                return NotFound();
            }

            return View(groupedItem);
        }

        // GET: GroupedItems/Create
        public IActionResult Create()
        {
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "BlogPostId", "BlogText");
            ViewData["LogGroupingId"] = new SelectList(_context.LogGrouping, "LogGroupingId", "LogGroupingComment");
            return View();
        }

        // POST: GroupedItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupedItemId,LogGroupingId,BlogPostId," +
            "GroupedItemComment,ItemCreateDate")] GroupedItem groupedItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupedItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "BlogPostId", "BlogText", 
                groupedItem.BlogPostId);
            ViewData["LogGroupingId"] = new SelectList(_context.LogGrouping, "LogGroupingId", 
                "LogGroupingComment", groupedItem.LogGroupingId);
            return View(groupedItem);
        }


        // GET: GroupedItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupedItem = await _context.GroupedItem.FindAsync(id);
            if (groupedItem == null)
            {
                return NotFound();
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "BlogPostId", "BlogText", groupedItem.BlogPostId);
            ViewData["LogGroupingId"] = new SelectList(_context.LogGrouping, "LogGroupingId", "LogGroupingComment", groupedItem.LogGroupingId);
            return View(groupedItem);
        }

        // POST: GroupedItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupedItemId,LogGroupingId,BlogPostId,GroupedItemComment,ItemCreateDate")] GroupedItem groupedItem)
        {
            if (id != groupedItem.GroupedItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupedItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupedItemExists(groupedItem.GroupedItemId))
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
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "BlogPostId", "BlogText", groupedItem.BlogPostId);
            ViewData["LogGroupingId"] = new SelectList(_context.LogGrouping, "LogGroupingId", "LogGroupingComment", groupedItem.LogGroupingId);
            return View(groupedItem);
        }

        // GET: GroupedItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupedItem = await _context.GroupedItem
                .Include(g => g.BlogPost)
                .Include(g => g.LogGrouping)
                .FirstOrDefaultAsync(m => m.GroupedItemId == id);
            if (groupedItem == null)
            {
                return NotFound();
            }

            return View(groupedItem);
        }

        // POST: GroupedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupedItem = await _context.GroupedItem.FindAsync(id);
            _context.GroupedItem.Remove(groupedItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupedItemExists(int id)
        {
            return _context.GroupedItem.Any(e => e.GroupedItemId == id);
        }
    }
}
