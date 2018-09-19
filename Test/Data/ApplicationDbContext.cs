using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BlogPosts> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasKey(x => x.Name);
            builder.Entity<Category>().Property(x => x.DateCreated).HasDefaultValueSql("GetDate()");
            builder.Entity<Category>().Property(x => x.DateLastModified).HasDefaultValueSql("GetDate()");
            builder.Entity<Category>().Property(x => x.Name).HasMaxLength(100);
        }
    }
}
