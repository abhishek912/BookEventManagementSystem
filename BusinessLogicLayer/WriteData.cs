using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.DataAccessLayer;
using BookEventManager.Data.Model;
using BookEventManager.Shared;
using BookEventManager.Shared.DTO;

namespace BookEventManager.Business.Logic
{
    public class WriteData : IWriteData
    {
        IBookWriteOperations obj = (BookWriteOperations)Activator.CreateInstance(typeof(BookWriteOperations));

        public WriteData(IBookWriteOperations obj)
        {
            this.obj = obj;
        }

        public WriteData() { }

        private User CreateUserObject(UserDTO user)
        {
            User userObj = new User
            {
                UserId = user.UserId,
                Email = user.Email,
                FullName = user.FullName,
                Password = user.Password
            };
            return userObj;
        }
        
        public bool SignUp(UserDTO user)
        {
            user.Email = user.Email.ToLower();
            user.Password = Crypt.Hash(user.Password);
            User userObj = CreateUserObject(user);
            return obj.RegisterUser(userObj);
        }

        public bool CreateEvent(EventDTO record)
        {
            Event @event = CreateEventObject(record);

            return obj.AddEvent(@event);
        }

        private Event CreateEventObject(EventDTO @record)
        {
            //Automapper can be used
            Event @event = new Event
            {
                DateOfEvent = record.DateOfEvent,
                Description = record.Description,
                Duration = record.Duration,
                EventCreateDateTime = record.EventCreateDateTime,
                EventId = record.EventId,
                InvitedTo = record.InvitedTo,
                Location = record.Location,
                OtherDetails = record.OtherDetails,
                StartTime = record.StartTime,
                Title = record.Title,
                Type = record.Type,
                UserId = record.UserId
            };
            return @event;
        }

        private EventComment CreateEventCommentDTOObject(EventCommentDTO obj)
        {
            EventComment comment = new EventComment
            {
                Comment = obj.Comment,
                CommentId = obj.CommentId,
                CurrentDate = obj.CurrentDate,
                EventId = obj.EventId,
                UserId = obj.UserId
            };
            return comment;
        }

        public bool AddComment(EventCommentDTO comment)
        {
            EventComment cmnt = CreateEventCommentDTOObject(comment);
            return obj.AddComment(cmnt);
        }

        public void EditEvent(EventDTO record)
        {
            Event @event = CreateEventObject(record);
            obj.EditEvent(@event);
        }

        public int DeleteEvent(int id)
        {
            ReadData r = new ReadData();
            EventDTO @event = r.FetchEventDetails(id);
            Event record = CreateEventObject(@event);
            return obj.DeleteEvent(record);
        }

        //For testing purpose only
        public int DeleteEvent(IBookReadOperations obj1, int id)
        {
            ReadData r = new ReadData(obj1);
            EventDTO @event = r.FetchEventDetails(id);
            Event record = CreateEventObject(@event);
            int result = obj.DeleteEvent(record);
            return result;
        }

        public void DeleteComment(EventCommentDTO comment)
        {
            EventComment cmnt = CreateEventCommentDTOObject(comment);
            obj.DeleteComment(cmnt);
        }
    }
}
