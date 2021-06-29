using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.Model;
using BookEventManager.Data.ModelContext;

namespace BookEventManager.Data.DataAccessLayer
{
    public class BookWriteOperations : IBookWriteOperations
    {
        EntityContext db = (EntityContext)Activator.CreateInstance(typeof(EntityContext));
    
        public bool RegisterUser(User user)
        {
            var cred = new Credential { Email = user.Email, Password = user.Password };
            bool result = AddToCredential(cred);
            if(result)
            {
                result = AddToUser(user);
                return result;
            }
            return false;
        }

        public bool AddToUser(User user)
        {
            if (db.Users.Add(user) != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddToCredential(Credential cred)
        {
            if (db.Credentials.Add(cred) != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddEvent(Event @event)
        {
            if(db.Events.Add(@event) != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public void EditEvent(Event @event)
        {
            db.Entry(@event).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public int DeleteEvent(Event @event)
        {
            var entry = db.Entry(@event);
            if (entry.State == System.Data.Entity.EntityState.Detached)
                db.Events.Attach(@event);
            
            db.Events.Remove(@event);
            return db.SaveChanges();
        }

        public bool AddComment(EventComment comment)
        {
            if(db.Comments.Add(comment) != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteComment(EventComment comment)
        {
            var entry = db.Entry(comment);
            if (entry.State == System.Data.Entity.EntityState.Detached)
                db.Comments.Attach(comment);

            db.Comments.Remove(comment);
            db.SaveChanges();
        }
    }
}
