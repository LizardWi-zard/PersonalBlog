using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        ArticleContext db = new ArticleContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Article> articles = db.Articles;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Articles = articles;
            // возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult ReadArticle(int id)
        {
            IEnumerable<Article> articles = db.Articles;
            ViewBag.Articles = articles;
            ViewBag.ArticleId = id;
            return View();
        }
    }
}