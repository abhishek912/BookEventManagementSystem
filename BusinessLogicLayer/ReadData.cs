using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.DataAccessLayer;
using BookEventManager.Shared;
using BookEventManager.Data.Model;
using BookEventManager.Shared.DTO;

namespace BookEventManager.Business.Logic
{
    public class ReadData : IReadData
    {
        IBookReadOperations obj = (BookReadOperations)Activator.CreateInstance(typeof(BookReadOperations));

        public ReadData(IBookReadOperations obj)
        {
            this.obj = obj;
        }

        public ReadData()
        {
        }

        private EventDTO CreateEventDTOObject(Event record)
        {
            //Automapper can be used
            EventDTO @event = new EventDTO
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
                UserId = record.UserId,
                UserEmail = GetUserEmail(record.UserId),
                UserFullName = GetUserFullName(record.UserId),
                CountOfInvitedTo = GetInvitedToEmailCount(record.InvitedTo)
            };
            return @event;
        }

        public PublicEventsDTO FetchPublicEvents()
        {
            var AllEvents = obj.GetEvents();

            var past = AllEvents.Where(x => x.DateOfEvent < DateTime.Now && x.Type == true).OrderByDescending(x=>x.DateOfEvent).ToList();

            var future = AllEvents.Where(x => x.DateOfEvent >= DateTime.Now && x.Type == true).OrderByDescending(x => x.DateOfEvent).ToList();

            PublicEventsDTO events = new PublicEventsDTO(past, future);
            
            return events;
        }

        public PublicEventsDTO FetchPublicEvents(string email)
        {
            var cred = obj.GetCredentials();
            var record = cred.Find(x=>x.Email.Equals(email));

            //Case for Admin User
            if(record!= null && Admin.AdminEmail.Contains(record.Email))
            {
                var events = obj.GetEvents();
                var past = events.Where(x => x.DateOfEvent < DateTime.Now).OrderByDescending(x => x.DateOfEvent).ToList();
                var future = events.Where(x => x.DateOfEvent >= DateTime.Now).OrderByDescending(x => x.DateOfEvent).ToList();
                PublicEventsDTO pubEvents = new PublicEventsDTO(past, future);
                return pubEvents;
            }
            else
            {
                //Case For Normal User
                return FetchPublicEvents();
            }
        }

        public EventDTO FetchEventDetails(int? id)
        {
            var events = obj.GetEvents();
            Event record = events.Where(x => x.EventId == id).FirstOrDefault();

            EventDTO @event = CreateEventDTOObject(record);

            return @event;
        }

        public string GetUserEmail(int? UID)
        {
            string email = obj.GetUsers().Where(x => x.UserId == UID).Select(x => x.Email).SingleOrDefault();
            return email;
        }

        public int GetUserIdByEmail(string email)
        {
            int userId = obj.GetUsers().Where(x => x.Email.Equals(email)).Select(x => x.UserId).SingleOrDefault();
            return userId;
        }

        public int GetUserIdByEventId(int eventId)
        {
            int UID = obj.GetEvents().Where(x => x.EventId == eventId).Select(x => x.UserId).SingleOrDefault();
            return UID;
        }

        public IEnumerable<EventDTO> FetchMyEvents(int UID)
        {
            var events = obj.GetEvents().Where(x => x.UserId == UID).OrderBy(x=>x.DateOfEvent);

            List<EventDTO> records = new List<EventDTO>();
            foreach(var @event in events)
            {
                var record = CreateEventDTOObject(@event);
                records.Add(record);
            }

            return records;
        }

        public IEnumerable<EventDTO> FetchEventsInvitedTo(string email)
        {
            var events = obj.GetEvents();
            var records = events.Where(x => (x.InvitedTo!=null && x.InvitedTo.Contains(email)));

            List<EventDTO> invitedEvents = new List<EventDTO>();
            foreach(var record in records)
            {
                var @event = CreateEventDTOObject(record);
                invitedEvents.Add(@event);
            }

            return invitedEvents;
        }

        public string GetUserFullName(int userId)
        {
            string name = obj.GetUsers().Where(x => x.UserId == userId).Select(x => x.FullName).SingleOrDefault();
            return name;
        }

        public IEnumerable<EventCommentDTO> FetchCommentsOfEvent(int? id)
        {
            var comments = obj.GetComments();
            var eventComments = comments.Where(u => u.EventId == id).OrderBy(u => u.CurrentDate).ToList();

            List<EventCommentDTO> records = new List<EventCommentDTO>();
            foreach(var comment in eventComments)
            {
                EventCommentDTO record = CreateEventCommentDTOObject(comment);
                records.Add(record);
            }

            return records;
        }

        public int GetInvitedToEmailCount(string Emails)
        {
            if(Emails == null)
            {
                return 0;
            }
            string[] mails = Emails.Split(',');
            return mails.Count();
        }

        private EventCommentDTO CreateEventCommentDTOObject(EventComment comment)
        {
            EventCommentDTO cmnt = new EventCommentDTO
            {
                Comment = comment.Comment,
                CommentId = comment.CommentId,
                CurrentDate = comment.CurrentDate,
                EventId = comment.EventId,
                UserId = comment.UserId,
                Email = GetUserEmail(comment.UserId),
                FullName = GetUserFullName(comment.UserId)
            };
            return cmnt;
        }

        public EventCommentDTO GetCommentWithCommentId(int? id)
        {
            var comment = obj.GetComments().Where(x => x.CommentId == id).FirstOrDefault();
            EventCommentDTO cmnt = CreateEventCommentDTOObject(comment);
            return cmnt;
        }
    }
}
