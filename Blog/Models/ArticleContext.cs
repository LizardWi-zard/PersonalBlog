using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace Blog.Models
{
    public class ArticleContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
    }
}