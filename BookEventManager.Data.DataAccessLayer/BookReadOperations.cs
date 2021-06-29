using BookEventManager.Data.Model;
using BookEventManager.Data.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEventManager.Data.DataAccessLayer
{
    public class BookReadOperations : IBookReadOperations
    {
        EntityContext db = (EntityContext)Activator.CreateInstance(typeof(EntityContext));
        public List<EventComment> GetComments()
        {
            List<EventComment> comments = db.Comments.ToList();
            return comments;
        }

        public List<Event> GetEvents()
        {
            List<Event> events = db.Events.ToList();
            return events;
        }

        public List<User> GetUsers()
        {
            List<User> users = db.Users.ToList();
            return users;
        }

        public List<Credential> GetCredentials()
        {
            List<Credential> credentials = db.Credentials.ToList();
            return credentials;
        }
    }
}
