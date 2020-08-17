using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [HttpGet]
        public ActionResult EditArticle(int? id)
        {
            if (id == null)
            {
                return Index();
            }
            Article article = db.Articles.Find(id);
            if (article != null)
            {
                return View(article);
            }
            return Index();
        }

        [HttpPost]
        public ActionResult EditArticle(Article article)
        {
            db.Entry(article).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateArticle(Article article)
        {
            db.Articles.Add(article);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }   
}