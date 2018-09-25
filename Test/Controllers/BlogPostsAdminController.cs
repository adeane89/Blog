using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    public class BlogPostsAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogPostsAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogPostsAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogPosts.ToListAsync());
        }

        // GET: BlogPostsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPosts = await _context.BlogPosts
                .SingleOrDefaultAsync(m => m.ID == id);
            if (blogPosts == null)
            {
                return NotFound();
            }

            return View(blogPosts);
        }

        // GET: BlogPostsAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogPostsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Content,CategoryName,ImagePath,PostCreated,DateCreated,DateLastModified")] BlogPosts blogPosts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPosts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPosts);
        }

        // GET: BlogPostsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPosts = await _context.BlogPosts.SingleOrDefaultAsync(m => m.ID == id);
            if (blogPosts == null)
            {
                return NotFound();
            }
            return View(blogPosts);
        }

        // POST: BlogPostsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Content,CategoryName,ImagePath,PostCreated,DateCreated,DateLastModified")] BlogPosts blogPosts)
        {
            if (id != blogPosts.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPosts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostsExists(blogPosts.ID))
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
            return View(blogPosts);
        }

        // GET: BlogPostsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPosts = await _context.BlogPosts
                .SingleOrDefaultAsync(m => m.ID == id);
            if (blogPosts == null)
            {
                return NotFound();
            }

            return View(blogPosts);
        }

        // POST: BlogPostsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPosts = await _context.BlogPosts.SingleOrDefaultAsync(m => m.ID == id);
            _context.BlogPosts.Remove(blogPosts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostsExists(int id)
        {
            return _context.BlogPosts.Any(e => e.ID == id);
        }
    }
}
