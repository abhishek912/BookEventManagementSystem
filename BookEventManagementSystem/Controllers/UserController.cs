using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookEventManagementSystem.Models;
using System.Web.Security;

namespace BookEventManagementSystem.Controllers
{
    public class UserController : Controller
    {
        EntityContext db = new EntityContext();
        // GET: User
        public ActionResult Signup()
        {
            db.Database.CreateIfNotExists();
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            bool isValid = db.Users.Any( x=> x.Email == user.Email );
            if(isValid)
            {
                ModelState.AddModelError("", "Email already exists, try with some other Email Address.");
                return View();
            }
            else
            {
                db.Users.Add(user);
                var login = new Credential { Email = user.Email, Password = user.Password };
                db.Credentials.Add(login);
                db.SaveChanges();
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Login()
        {
            db.Database.CreateIfNotExists();
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Credential cred)
        {
            bool isValid = db.Credentials.Any( x=> x.Email == cred.Email && x.Password == cred.Password );
            if(isValid)
            {
                FormsAuthentication.SetAuthCookie(cred.Email, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credientials!");
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}