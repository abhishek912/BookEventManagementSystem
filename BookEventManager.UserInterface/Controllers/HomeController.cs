using BookEventManager.Business.Logic;
using BookEventManager.Shared.DTO;
using System.Web.Mvc;

namespace BookEventManager.UserInterface.Controllers
{
    public class HomeController : Controller
    {
        private IReadData obj;

        public HomeController(IReadData obj)
        {
            this.obj = obj;
        }
        public ActionResult Index()
        {
            PublicEventsDTO events;
            if (User.Identity.IsAuthenticated)
            {
                events = obj.FetchPublicEvents(User.Identity.Name);
            }
            else
            {
                events = obj.FetchPublicEvents();
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