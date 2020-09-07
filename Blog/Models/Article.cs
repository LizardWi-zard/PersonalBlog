using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string MainContent { get; set; }
    }
}