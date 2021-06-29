using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.Model;

namespace BookEventManager.Data.DataAccessLayer
{
    public interface IBookReadOperations
    {
        List<Event> GetEvents();
        List<EventComment> GetComments();
        List<User> GetUsers();
        List<Credential> GetCredentials();
    }
}
