using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index (string category)
        {
            if (await _context.BlogPosts.CountAsync() == 0)
            {
                List<BlogPosts> Story = new List<BlogPosts>();
                Story.Add(new BlogPosts { Title = "September 3rd. The Day It All Began", ImagePath = "./images/IMG_4333.jpg", Content = "I have always had a case of wanderlust. I think it has to do with moving a lot growing up and making the best of a new experience. I knew I wanted to travel as an adult and see places I had never been. The wonder of what is out there and the experiences to be had has always drawn me. Jor and I have already been on two three-week trips to Europe and I lived in Italy for a while out of college but we both knew we wanted to live somewhere else (at least for a while.) We were driving home from our annual family Labor Day trip and I asked Jordan if traveling across the US in an RV was something we can seriously start discussing and put some thought into, or if it would be one of those dreams that just stays a dream. Thankfully, my husband is amazing and said he would be willing to look into it. I am definitely the dreamer in our relationship and Jordan keeps me from selling our house and moving into an RV in a week (which I may have done.) After about a week of googling, youtubing, creeping on Instagrams, and having numerous discussions (basically every moment of time we were in person together), we decided we were 90% sure that we were going to be selling our house and living in an RV within the next year. Our tentative goal was September 2019. That would be our five-year anniversary of our house and seemed like the opportune time to sell it, get remote jobs, and move into an RV. (Side note: we have a five-year grant on our house that will cost us $120/mo, if we sell before September.) However, I realized that the route we wanted to take would not necessarily pan out for the best weather after the fall. Ideally, we go through parts of the western states and then head into Canada for a while before traveling down the west coast. I casually decided to mention to Jordan that possibly leaving earlier in the year would be feasible and after some more discussions and multi social network perusing, it became a possibility. May. Seven months from now. The reality of that slowly began to sink in. How could we sell our house, buy an RV, get remote jobs, and tell all of our friends and family that we were leaving in that time period? The questions and what ifs just started building. We knew we would lose money from the grant, on Jor’s lease on his car, and less time for me to get experience at a job. Would it be worth all of it? When all of the talking, all of the planning, all of the how are we going to do this, ends in us traveling and fulfilling this dream, we know it will be.", DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Our Story", BlogPosts = Story });

                List<BlogPosts> Finances = new List<BlogPosts>();
                Finances.Add(new BlogPosts { Title = "What happens if we need money?", ImagePath = "./images/IMG_4333.jpg", Content = "We currently are a single income household as I continue to actively search for an entry level software position. The past four months have given us a peek into what it would be like living on somewhat of a budget. A decent concern with pursing remote jobs would be taking a cut in our annual salary and deciding if we could afford that change. While we haven’t actively starting looking for remote jobs quite yet, we know they exist and are something that will be looked into in the near future. While we are not pursing this lifestyle as a means to save money, we are open to that possibility (I mean, who wouldn’t be?) We currently have our monthly expenses that include: mortgage, utilities, internet, cell phone, car payment, student loans, gym membership, and food. Some of these will obviously be applicable on the road and ", DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Finances", BlogPosts = Finances });

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
    }
}