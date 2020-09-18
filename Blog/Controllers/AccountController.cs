using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.Owin.Security;
// using Blog.ViewModels; // пространство имен LoginViewModel
using Blog.Models; // пространство имен моделей
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        UserContext db = new UserContext();
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginView(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                //  User user = Task.Run(() => db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password)).Result;
              
                
                //User user = db.Users.Include(u => u.Role).ToList().Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();

                //System.Diagnostics.Debugger.NotifyOfCrossThreadDependency();
                //User user = await db.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync(); 

                //IQueryable<User> users = db.Users.Include(x => x.Role);
                //
                //var mails = users.Where(x => x.Email == model.Email);
                //var passwords = mails.Where(x => x.Password == model.Password);
                //
                //User user = passwords.First();

                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "OWIN Provider", ClaimValueTypes.String));
                    if (user.Role != null)
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name, ClaimValueTypes.String));

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}