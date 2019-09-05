using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.Models
{
    public class BlogPostModel
    {
        // General properties  
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }

        // Time based properties  
        //public DateTime CreateTime { get; set; }

        // Other properties and settings may include UserID, RoleID etc.  
    }
}