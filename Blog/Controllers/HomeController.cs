using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace Blog.Controllers
{
    public class HomeController :  AccountController
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditArticle(Article article)
        {
            db.Entry(article).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateArticle()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateArticle(Article article)
        {
            db.Articles.Add(article);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // public ActionResult LoginView()
        // {
        //     return View();
        // }


        // [HttpPost]
        // public ActionResult LoginView(string email, string password)
        // {
        //     if(email == "hello" && password == "12345")
        //     {
        //         return RedirectToAction("EditArticle");
        //     }
        //
        //     return RedirectToAction("Index");
        // }
    }   
}