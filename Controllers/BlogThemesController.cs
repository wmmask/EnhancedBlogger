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
    public class BlogThemesController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public BlogThemesController(EnhancedBloggerContext context)
        {
            _context = context;
        }
        public IActionResult ThemesAndSubThemes(int id)//calling BlogThemeId
        {
            AThemesWithSubThemesViewModel myThemesSubs = new AThemesWithSubThemesViewModel();
            myThemesSubs.blogTheme = _context.BlogTheme.FirstOrDefault(t => t.BlogThemeId == id);
            myThemesSubs.blogSubThemes = _context.BlogSubTheme.Where(s => s.BlogThemeId == id);

            return View(myThemesSubs);
        }


        // GET: BlogThemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogTheme.ToListAsync());
        }

        // GET: BlogThemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTheme = await _context.BlogTheme
                .FirstOrDefaultAsync(m => m.BlogThemeId == id);
            if (blogTheme == null)
            {
                return NotFound();
            }

            return View(blogTheme);
        }

        // GET: BlogThemes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogThemes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogThemeId,BlogThemetitle,ThemeCreateDate")] BlogTheme blogTheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogTheme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogTheme);
        }

        // GET: BlogThemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTheme = await _context.BlogTheme.FindAsync(id);
            if (blogTheme == null)
            {
                return NotFound();
            }
            return View(blogTheme);
        }

        // POST: BlogThemes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogThemeId,BlogThemetitle,ThemeCreateDate")] BlogTheme blogTheme)
        {
            if (id != blogTheme.BlogThemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogTheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogThemeExists(blogTheme.BlogThemeId))
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
            return View(blogTheme);
        }

        // GET: BlogThemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTheme = await _context.BlogTheme
                .FirstOrDefaultAsync(m => m.BlogThemeId == id);
            if (blogTheme == null)
            {
                return NotFound();
            }

            return View(blogTheme);
        }

        // POST: BlogThemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogTheme = await _context.BlogTheme.FindAsync(id);
            _context.BlogTheme.Remove(blogTheme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogThemeExists(int id)
        {
            return _context.BlogTheme.Any(e => e.BlogThemeId == id);
        }
    }
}
