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
    public class BlogInfoesController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public BlogInfoesController(EnhancedBloggerContext context)
        {
            _context = context;
        }

        // GET: BlogInfoes
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.BlogInfo.Include(b => b.BlogSubTheme).Include(b => b.UserProfile);
            return View(await enhancedBloggerContext.ToListAsync());
        }

        // GET: BlogInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo
                .Include(b => b.BlogSubTheme)
                .Include(b => b.UserProfile)
                .FirstOrDefaultAsync(m => m.BlogInfoId == id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            return View(blogInfo);
        }

        // GET: BlogInfoes/Create
        public IActionResult Create()
        {
            ViewData["BlogSubThemeId"] = new SelectList(_context.BlogSubTheme, "BlogSubThemeId", "BlogSubThemetitle");
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio");
            return View();
        }

        // POST: BlogInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogInfoId,BlogSubThemeId,BlogInfoTitle,UserProfileId,InfoCreateDate")] BlogInfo blogInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogSubThemeId"] = new SelectList(_context.BlogSubTheme, "BlogSubThemeId", "BlogSubThemetitle", blogInfo.BlogSubThemeId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", blogInfo.UserProfileId);
            return View(blogInfo);
        }

        // GET: BlogInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo.FindAsync(id);
            if (blogInfo == null)
            {
                return NotFound();
            }
            ViewData["BlogSubThemeId"] = new SelectList(_context.BlogSubTheme, "BlogSubThemeId", "BlogSubThemetitle", blogInfo.BlogSubThemeId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", blogInfo.UserProfileId);
            return View(blogInfo);
        }

        // POST: BlogInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogInfoId,BlogSubThemeId,BlogInfoTitle,UserProfileId,InfoCreateDate")] BlogInfo blogInfo)
        {
            if (id != blogInfo.BlogInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogInfoExists(blogInfo.BlogInfoId))
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
            ViewData["BlogSubThemeId"] = new SelectList(_context.BlogSubTheme, "BlogSubThemeId", "BlogSubThemetitle", blogInfo.BlogSubThemeId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "Bio", blogInfo.UserProfileId);
            return View(blogInfo);
        }

        // GET: BlogInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo
                .Include(b => b.BlogSubTheme)
                .Include(b => b.UserProfile)
                .FirstOrDefaultAsync(m => m.BlogInfoId == id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            return View(blogInfo);
        }

        // POST: BlogInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogInfo = await _context.BlogInfo.FindAsync(id);
            _context.BlogInfo.Remove(blogInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogInfoExists(int id)
        {
            return _context.BlogInfo.Any(e => e.BlogInfoId == id);
        }
    }
}
