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
                Story.Add(new BlogPosts { Title = "I have a job!", ImagePath = "/images/laborday.jpeg", Content = "I have a job! After quitting my job as a nanny back in May (which was harder than thinking of going without an income), I voyaged on a journey to change careers and become a Software Developer. Four months later, I am officially employed! I definitely would not be where I am without my fantastic husband who pushed me to go after what I wanted and endlessly encouraged me throughout this whole process. I am excited to see what this new opportunity holds and what I will learn in my time there! Our RV dream is still alive and we are working on our timeline.There are sooo many decisions to be made and thankfully we have plenty of time to figure out the answers.We have already decided we want a Class C motorhome and ideally under 24 feet.After looking into different styles of RV’s, we knew what we wanted out of our RV(traveling durability, camping flexibility, and an onboard shower) and we were able to narrow it down for our needs. Our current dilemma is what year we should go with.Do we go with an older model with less miles and buy it outright ? Do we go with a newer model that is slightly more expensive and ideally has less automotive issues ? We currently are leaning 2004 or newer but we are still researching.Our priority is having space for two desk areas since we will be working full - time throughout our journey and room for Augie to relax inside so we are thinking of having a slide out in the living room area. It’s presently difficult discussing anything besides this RV dream and where we will go and what jobs we will have and if Augie will enjoy it.We have read a lot about how dogs will handle this new lifestyle and that has been a decent part of discussion.Augie used to have an issue with traveling in the car but has recently overcome it!We are planning on attempting to follow the 2 - 2 - 2 rule when we travel(no more than two hundred miles, no less than two days in one spot, and no more than two hours?) so we hope that this helps Augie acclimate to our new lifestyle.On our first trip to Europe together, we traveled from Paris and stayed in Lisbon less than 24 hours and then flew to Spain and it was a rough experience. Since that day, we agreed to stay at least one day in each location and it has not disappointed us yet.Augie loves being outside and playing fetch but also cuddling and being around Jordan and myself(he presently is half on my lap asleep as I write this). We know he will love both of us being with him most of the day and spending time outside camping so fingers crossed he loves this new experience as much as we do !", PostCreated = DateTime.Now.ToString("MMMM d, yyyy"), DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new Category { Name = "New Job!", BlogPosts = Story });
                List<BlogPosts> Finances = new List<BlogPosts>();
                Finances.Add(new BlogPosts { Title = "What happens if we need money?", ImagePath = "/images/searchIR.jpeg", Content = "     We currently are a single income household as I continue to actively search for an entry level software position. The past four months have given us a peek into what it would be like living on somewhat of a budget. A decent concern with pursing remote jobs would be taking a cut in our annual salary and deciding if we could afford that change. While we haven’t actively starting looking for remote jobs quite yet, we know they exist and are something that will be looked into in the near future. While we are not pursing this lifestyle as a means to save money, we are open to that possibility (I mean, who wouldn’t be?) We currently have our monthly expenses that include: mortgage, utilities, internet, cell phone, car payment, student loans, gym membership, and food. Some of these will obviously be applicable on the road and ", PostCreated = DateTime.Now.ToString("MMMM d, yyyy"), DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Finances", BlogPosts = Finances });
                List<BlogPosts> Life = new List<BlogPosts>();
                Story.Add(new BlogPosts { Title = "September 3rd. The Day It All Began", ImagePath = "/images/laborday.jpeg", Content = "   I have always had a case of wanderlust. I think it has to do with moving a lot growing up and making the best of a new experience. I knew I wanted to travel as an adult and see places I had never been. The wonder of what is out there and the experiences to be had has always drawn me. Jor and I have already been on two three-week trips to Europe and I lived in Italy for a while out of college but we both knew we wanted to live somewhere else (at least for a while.)    We were driving home from our annual family Labor Day trip and I asked Jordan if traveling across the US in an RV was something we can seriously start discussing and put some thought into, or if it would be one of those dreams that just stays a dream. Thankfully, my husband is amazing and said he would be willing to look into it. I am definitely the dreamer in our relationship and Jordan keeps me from selling our house and moving into an RV in a week (which I may have done.)     After about a week of googling, youtubing, creeping on Instagrams, and having numerous discussions (basically every moment of time we were in person together), we decided we were 90% sure that we were going to be selling our house and living in an RV within the next year. Our tentative goal was September 2019. That would be our five-year anniversary of our house and seemed like the opportune time to sell it, get remote jobs, and move into an RV. (Side note: we have a five-year grant on our house that will cost us $120/mo, if we sell before September.) However, I realized that the route we wanted to take would not necessarily pan out for the best weather after the fall. Ideally, we go through parts of the western states and then head into Canada for a while before traveling down the west coast. I casually decided to mention to Jordan that possibly leaving earlier in the year would be feasible and after some more discussions and multi social network perusing, it became a possibility. May. Seven months from now. The reality of that slowly began to sink in. How could we sell our house, buy an RV, get remote jobs, and tell all of our friends and family that we were leaving in that time period? The questions and what ifs just started building. We knew we would lose money from the grant, on Jor’s lease on his car, and less time for me to get experience at a job. Would it be worth all of it? When all of the talking, all of the planning, all of the how are we going to do this, ends in us traveling and fulfilling this dream, we know it will be.", PostCreated = DateTime.Now.ToString("MMMM d, yyyy"), DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Our Story", BlogPosts = Story });
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
