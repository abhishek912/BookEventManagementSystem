using BookEventManager.Business.Logic;
using BookEventManager.Shared.DTO;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace BookEventManager.UserInterface.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private IReadData readObj;
        private IWriteData writeObj;

        public EventsController(IWriteData writeObj, IReadData readObj)
        {
            this.writeObj = writeObj;
            this.readObj = readObj;
        }
        // GET: Events
        public ActionResult Index()
        {
            var UID = readObj.GetUserIdByEmail(User.Identity.Name);
            var events = readObj.FetchMyEvents(UID);
            return View(events);
        }
        public ActionResult InvitedTo()
        {
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<EventDTO> events = readObj.FetchEventsInvitedTo(User.Identity.Name);
                return View(events);
            }
            return RedirectToAction("Login", "User");
        }

        // GET: Events/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDTO @event = readObj.FetchEventDetails(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<string> timeList = new List<string>();
                for (int i = 0; i < 24; i++)
                {
                    if (i < 10)
                        timeList.Add("0" + i + ":00");
                    else
                        timeList.Add(i + ":00");
                }
                ViewBag.List = timeList;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,UserId,UserEmail,UserFullName,Title,DateOfEvent,StartTime,Location,Type,Duration,Description,OtherDetails,CountOfInvitedTo,InvitedTo,EventCreateDateTime")] EventDTO @event)
        {
            if (ModelState.IsValid)
            {
                //if (@event.Duration > 0 && @event.Duration < 5)
                //{
                if(@event.Type == null)
                {
                    @event.Type = true;
                }
                    int UID = readObj.GetUserIdByEmail(User.Identity.Name);
                    @event.UserId = UID;
                    string email = readObj.GetUserEmail(UID);
                    @event.UserEmail = email;
                    string name = readObj.GetUserFullName(UID);
                    @event.UserFullName = name;
                    int countOfInvitedTo = readObj.GetInvitedToEmailCount(@event.InvitedTo);
                    @event.CountOfInvitedTo = countOfInvitedTo;
                    writeObj.CreateEvent(@event);
                    return RedirectToAction("Index");
               }
                //else
                //{
                    //ModelState.AddModelError("", "Duration should lie between 1 and 4");
                //}
            //}
            List<string> timeList = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                    timeList.Add("0" + i + ":00");
                else
                    timeList.Add(i + ":00");
            }
            ViewBag.List = timeList;

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.IsAuthenticated)
            {
                EventDTO @event = readObj.FetchEventDetails(id);
                if (@event == null)
                {
                    return HttpNotFound();
                }
                List<string> timeList = new List<string>();
                for (int i = 0; i < 24; i++)
                {
                    if (i < 10)
                        timeList.Add("0" + i + ":00");
                    else
                        timeList.Add(i + ":00");
                }
                ViewBag.List = timeList;
                return View(@event);
            }
            return RedirectToAction("Login", "User");
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,UserId,UserEmail,UserFullName,Title,DateOfEvent,StartTime,Location,Type,Duration,Description,OtherDetails,InvitedTo,EventCreateDateTime")] EventDTO @event)
        {
            if (ModelState.IsValid)
            {
                var UID = readObj.GetUserIdByEventId(@event.EventId);
                @event.UserId = UID;
                string email = readObj.GetUserEmail(UID);
                @event.UserEmail = email;
                string name = readObj.GetUserFullName(UID);
                @event.UserFullName = name;
                writeObj.EditEvent(@event);
                
                if (User.Identity.Name.Equals("myadmin@bookevents.com"))
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.IsAuthenticated)
            {
                EventDTO @event = readObj.FetchEventDetails(id);
                if (@event == null)
                {
                    return HttpNotFound();
                }
                return View(@event);
            }
            return RedirectToAction("Login", "User");
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            writeObj.DeleteEvent(id);
            if(User.Identity.Name.Equals("myadmin@bookevents.com"))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
