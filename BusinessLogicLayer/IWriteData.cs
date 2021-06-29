using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.Model;
using BookEventManager.Shared.DTO;

namespace BookEventManager.Business.Logic
{
    public interface IWriteData
    {
        bool SignUp(UserDTO user);
        bool CreateEvent(EventDTO @event);
        bool AddComment(EventCommentDTO comment);
        void EditEvent(EventDTO @event);
        int DeleteEvent(int id);
        void DeleteComment(EventCommentDTO comment);
    }
}
