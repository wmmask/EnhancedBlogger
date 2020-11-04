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
    public class LogGroupingsController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public LogGroupingsController(EnhancedBloggerContext context)
        {
            _context = context;
        }

        // GET: LogGroupings
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.LogGrouping.Include(l => l.UserProfile);
            return View(await enhancedBloggerContext.ToListAsync());
        }

        // GET: LogGroupings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logGrouping = await _context.LogGrouping
                .Include(l => l.UserProfile)
                .FirstOrDefaultAsync(m => m.LogGroupingId == id);
            if (logGrouping == null)
            {
                return NotFound();
            }

            return View(logGrouping);
        }

        // GET: LogGroupings/Create
        public IActionResult Create()
        {
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio");
            return View();
        }


        // POST: LogGroupings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogGroupingId,UserProfileId,LogGroupingtitle," +
            "LogGroupingComment,GroupingCreateDate")] LogGrouping logGrouping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logGrouping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", logGrouping.UserProfileId);
            return View(logGrouping);
        }

        // GET: LogGroupings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logGrouping = await _context.LogGrouping.FindAsync(id);
            if (logGrouping == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", logGrouping.UserProfileId);
            return View(logGrouping);
        }

        // POST: LogGroupings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogGroupingId,UserProfileId,LogGroupingtitle,LogGroupingComment,GroupingCreateDate")] LogGrouping logGrouping)
        {
            if (id != logGrouping.LogGroupingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logGrouping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogGroupingExists(logGrouping.LogGroupingId))
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
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", logGrouping.UserProfileId);
            return View(logGrouping);
        }

        // GET: LogGroupings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logGrouping = await _context.LogGrouping
                .Include(l => l.UserProfile)
                .FirstOrDefaultAsync(m => m.LogGroupingId == id);
            if (logGrouping == null)
            {
                return NotFound();
            }

            return View(logGrouping);
        }

        // POST: LogGroupings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logGrouping = await _context.LogGrouping.FindAsync(id);
            _context.LogGrouping.Remove(logGrouping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogGroupingExists(int id)
        {
            return _context.LogGrouping.Any(e => e.LogGroupingId == id);
        }
    }
}
