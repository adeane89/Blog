using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;
using Newtonsoft.Json;


namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index(string category)
        {
            if (await _context.BlogPosts.CountAsync() == 0)
            {
                //List<BlogPosts> Story = new List<BlogPosts>();
                //Story.Add(new BlogPosts { Title = "I have a job!", ImagePath = "/images/ann.jpg", Content = "", PostCreated = DateTime.Now.ToString("MMMM d, yyyy"), DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                //_context.Categories.Add(new Category { Name = "New Job!", BlogPosts = Story });

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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
