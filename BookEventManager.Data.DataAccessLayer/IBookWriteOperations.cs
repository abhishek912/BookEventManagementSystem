using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.Model;

namespace BookEventManager.Data.DataAccessLayer
{
    public interface IBookWriteOperations
    {
        bool RegisterUser(User user);
        bool AddToUser(User user);
        bool AddToCredential(Credential cred);
        bool AddEvent(Event @event);
        bool AddComment(EventComment comment);
        void EditEvent(Event @event);
        int DeleteEvent(Event @event);
        void DeleteComment(EventComment comment);
    }
}
