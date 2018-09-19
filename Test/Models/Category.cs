using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Category
    {
        public Category()
        {
            this.BlogPosts = new HashSet<BlogPosts>();
        }
        public string Name { get; set; }
        public ICollection<BlogPosts> BlogPosts { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
