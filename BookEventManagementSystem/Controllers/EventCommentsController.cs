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
    public class EventCommentsController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: EventComments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: EventComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventComment = db.Comments.Where(u => u.EventId == id).OrderByDescending(u=> u.CurrentDate).ToList();
            List<string> emails = new List<string>();
            foreach(var ele in eventComment)
            {
                var email = db.Users.Where(u => u.UserId == ele.UserId).Select(u => u.Email).SingleOrDefault();
                emails.Add(email);
            }
            ViewBag.emails = emails;
            if (eventComment == null)
            {
                return HttpNotFound();
            }
            return View(eventComment);
        }

        // GET: EventComments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Comment,UserId,EventId,CurrentDate")] EventComment eventComment)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                eventComment.UserId = db.Users.Where(u=> u.Email.Equals(User.Identity.Name)).Select(u=>u.UserId).SingleOrDefault();
                db.Comments.Add(eventComment);
                db.SaveChanges();
                return View();
            }
            ModelState.AddModelError("", "You are not Logged in!!!");
            return View();
        }

        // GET: EventComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventComment eventComment = db.Comments.Find(id);
            if (eventComment == null)
            {
                return HttpNotFound();
            }
            return View(eventComment);
        }

        // POST: EventComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Comment,UserId,EventId,CurrentDate")] EventComment eventComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventComment);
        }

        // GET: EventComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventComment eventComment = db.Comments.Find(id);
            if (eventComment == null)
            {
                return HttpNotFound();
            }
            return View(eventComment);
        }

        // POST: EventComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventComment eventComment = db.Comments.Find(id);
            db.Comments.Remove(eventComment);
            db.SaveChanges();
            return RedirectToAction("Details", "Events", new { id = eventComment.EventId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
