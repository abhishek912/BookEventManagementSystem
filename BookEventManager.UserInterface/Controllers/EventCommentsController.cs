using BookEventManager.Business.Logic;
using BookEventManager.Shared.DTO;
using System.Net;
using System.Web.Mvc;

namespace BookEventManager.UserInterface.Controllers
{
    public class EventCommentsController : Controller
    {
        private IReadData readObj;
        private IWriteData writeObj;

        public EventCommentsController(IWriteData writeObj, IReadData readObj)
        {
            this.writeObj = writeObj;
            this.readObj = readObj;
        }
        // GET: EventComments
        /*public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }*/

        // GET: EventComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventComment = readObj.FetchCommentsOfEvent(id);

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
        public ActionResult Create([Bind(Include = "CommentId,Comment,UserId,Email,FullName,EventId,CurrentDate")] EventCommentDTO eventComment)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                eventComment.UserId = readObj.GetUserIdByEmail(User.Identity.Name);
                eventComment.Email = readObj.GetUserEmail(eventComment.UserId);
                eventComment.FullName = readObj.GetUserFullName(eventComment.UserId);

                writeObj.AddComment(eventComment);
                return View();
            }
            ModelState.AddModelError("", "You are not Logged in!!!");
            return View();
        }

        // GET: EventComments/Edit/5
        /*public ActionResult Edit(int? id)
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
        }*/

        // GET: EventComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventCommentDTO eventComment = readObj.GetCommentWithCommentId(id);
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
            EventCommentDTO eventComment = readObj.GetCommentWithCommentId(id);
            writeObj.DeleteComment(eventComment);
            return RedirectToAction("Details", "Events", new { id = eventComment.EventId });
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
