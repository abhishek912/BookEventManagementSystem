using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookEventManagementSystem.Models;

namespace BookEventManagementSystem.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: Events
        public ActionResult Index()
        {
            var UID = db.Users.Where(u => u.Email.Equals(User.Identity.Name)).Select(u => u.UserId).SingleOrDefault(); 
            var events = db.Events.Where(u=> u.UserId == UID).OrderBy(u=> u.DateOfEvent).ToList();
            return View(events);
        }

        public ActionResult InvitedTo()
        {
            var events = db.Events.Where(u => u.InvitedTo.Contains(User.Identity.Name)).ToList();
            List<string> emails = new List<string>();
            foreach(var e in events)
            {
                var email = db.Users.Where(u => u.UserId == e.UserId).Select(u=>u.Email).SingleOrDefault();
                emails.Add(email);
            }
            ViewBag.Emails = emails;
            return View(events);
        }

        // GET: Events/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.email = db.Users.Where(u => u.UserId == @event.UserId).Select(u => u.Email).SingleOrDefault();
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                //int UID = db.Users.Where(u => u.Email.Equals(User.Identity.Name)).Select(u=> u.UserId).SingleOrDefault();

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
        public ActionResult Create([Bind(Include = "EventId,UserId,Title,DateOfEvent,StartTime,Location,Type,Duration,Description,OtherDetails,InvitedTo,EventCreateDateTime")] Event @event)
        {
            if (ModelState.IsValid)
            {
                int UID = db.Users.Where(u => u.Email.Equals(User.Identity.Name)).Select(u => u.UserId).SingleOrDefault();
                @event.UserId = UID;

                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
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

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,UserId,Title,DateOfEvent,StartTime,Location,Type,Duration,Description,OtherDetails,InvitedTo,EventCreateDateTime")] Event @event)
        {
            if (ModelState.IsValid)
            {
                var UID = db.Events.Where(u => u.EventId == @event.EventId).Select(u => u.UserId).SingleOrDefault();
                //var UID = db.Users.Where(u => u.Email.Equals(User.Identity.Name)).Select(u => u.UserId).SingleOrDefault();
                @event.UserId = UID;
                if (@event.DateOfEvent == null)
                {
                    var date = db.Events.Where(u => u.EventId == @event.EventId).Select(u => u.DateOfEvent).SingleOrDefault();
                    @event.DateOfEvent = date;
                }
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                if (User.Identity.Name.Equals("myadmin@bookevents.com"))
                {
                    return RedirectToAction("AllEvents", "Events");
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
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.email = db.Users.Where(u => u.UserId == @event.UserId).Select(u => u.Email).SingleOrDefault();
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            if(User.Identity.Name.Equals("myadmin@bookevents.com"))
            {
                return RedirectToAction("AllEvents", "Events");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AllEvents()
        {
            return View(db.Events.ToList());
        }
    }
}
