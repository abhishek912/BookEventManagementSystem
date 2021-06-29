using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookEventManagementSystem.Models;

namespace BookEventManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        EntityContext conn = new EntityContext();
        public ActionResult Index()
        {
            conn.Database.CreateIfNotExists();
            var events = conn.Events.Where(u=> u.Type == true).ToList();
            if(User.Identity.Name.Equals("myadmin@bookevents.com"))
            {
                return RedirectToAction("AllEvents", "Events");
            }
            return View(events);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}