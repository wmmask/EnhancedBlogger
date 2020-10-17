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
    public class BlogSubThemesController : Controller
    {
        private readonly EnhancedBloggerContext _context;

        public BlogSubThemesController(EnhancedBloggerContext context)
        {
            _context = context;
        }

        public IActionResult ThemeSubsPosts(int td) // calling in BlogSubThemeId
        {
            ASubthemesBlogsViewModel mySubsPosts = new ASubthemesBlogsViewModel();
            mySubsPosts.thisSubThemes = _context.BlogSubTheme.FirstOrDefault
                    (s => s.BlogSubThemeId == td);

            var queryInfo = _context.BlogInfo.Where(i => i.BlogSubThemeId == td);
            //var subsBlogs = from i in queryInfo select i.BlogInfoId;
            mySubsPosts.blogInfo = queryInfo ;
                //_context.BlogInfo.Where(i => i.BlogSubThemeId == td);
                        //int mytheme;
            //mytheme = (int)mySubsPosts.thisSubThemes.BlogThemeId;
            //mySubsPosts.thisTheme=_context.BlogTheme.FirstOrDefault(t=>t.BlogSubThemeId==thisSubThemes.BlogThemeId);

            return View(mySubsPosts);
        }

        // GET: BlogSubThemes
        public async Task<IActionResult> Index()
        {
            var enhancedBloggerContext = _context.BlogSubTheme.Include(b => b.BlogTheme);
            return View(await enhancedBloggerContext.ToListAsync());
        }


        // GET: BlogSubThemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogSubTheme = await _context.BlogSubTheme
                .Include(b => b.BlogTheme)
                .FirstOrDefaultAsync(m => m.BlogSubThemeId == id);
            if (blogSubTheme == null)
            {
                return NotFound();
            }

            return View(blogSubTheme);
        }

        // GET: BlogSubThemes/Create
        public IActionResult Create()
        {
            ViewData["BlogThemeId"] = new SelectList(_context.BlogTheme, "BlogThemeId", "BlogThemetitle");
            return View();
        }

        // POST: BlogSubThemes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogSubThemeId,BlogThemeId,BlogSubThemetitle,SubThemeCreateDate")] BlogSubTheme blogSubTheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogSubTheme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogThemeId"] = new SelectList(_context.BlogTheme, "BlogThemeId", "BlogThemetitle", blogSubTheme.BlogThemeId);
            return View(blogSubTheme);
        }

        // GET: BlogSubThemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogSubTheme = await _context.BlogSubTheme.FindAsync(id);
            if (blogSubTheme == null)
            {
                return NotFound();
            }
            ViewData["BlogThemeId"] = new SelectList(_context.BlogTheme, "BlogThemeId", "BlogThemetitle", blogSubTheme.BlogThemeId);
            return View(blogSubTheme);
        }

        // POST: BlogSubThemes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogSubThemeId,BlogThemeId,BlogSubThemetitle,SubThemeCreateDate")] BlogSubTheme blogSubTheme)
        {
            if (id != blogSubTheme.BlogSubThemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogSubTheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogSubThemeExists(blogSubTheme.BlogSubThemeId))
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
            ViewData["BlogThemeId"] = new SelectList(_context.BlogTheme, "BlogThemeId", "BlogThemetitle", blogSubTheme.BlogThemeId);
            return View(blogSubTheme);
        }

        // GET: BlogSubThemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogSubTheme = await _context.BlogSubTheme
                .Include(b => b.BlogTheme)
                .FirstOrDefaultAsync(m => m.BlogSubThemeId == id);
            if (blogSubTheme == null)
            {
                return NotFound();
            }

            return View(blogSubTheme);
        }

        // POST: BlogSubThemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogSubTheme = await _context.BlogSubTheme.FindAsync(id);
            _context.BlogSubTheme.Remove(blogSubTheme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogSubThemeExists(int id)
        {
            return _context.BlogSubTheme.Any(e => e.BlogSubThemeId == id);
        }
    }
}
