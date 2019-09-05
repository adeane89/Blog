using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext _context;
        public BlogController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index(string category)
        {
            if (await _context.BlogPosts.CountAsync() == 0)
            {
                List<BlogPosts> Story = new List<BlogPosts>();
                Story.Add(new BlogPosts { Title = "Three Months on the Road", ImagePath = "/images/RVCollegiate.jpg", Content = "We have officially been full time RVing for three months!", PostCreated = DateTime.Now.ToString("MMMM d, yyyy"), DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Three Months Fulltime!", BlogPosts = Story });
               
                await _context.SaveChangesAsync();
            }
            ViewBag.selectedCategory = category;
            List<BlogPosts> model;
            if (string.IsNullOrEmpty(category))
            {
                model = await this._context.BlogPosts.ToListAsync();
            }
            else
            {
                model = await this._context.BlogPosts.Where(x => x.CategoryName == category).ToListAsync();
            }
            ViewData["Categories"] = await this._context.Categories.Select(x => x.Name).ToArrayAsync();


            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            BlogPosts model = await _context.BlogPosts.FindAsync(id);
            return View(model);
        }

        public IActionResult Photos(int? id)
        {
            return View();
        }
    }
}
