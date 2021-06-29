using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.Model;
using BookEventManager.Data.DataAccessLayer;
using BookEventManager.Shared.DTO;

namespace BookEventManager.Business.Logic
{
    public interface IReadData
    {
        /*
         * Test Method: TestFetchPublicEvents()
         * with parameter as two types of user 
         * type1: admin
         * type2: normal user
         * and without parameter
        */
        PublicEventsDTO FetchPublicEvents();
        PublicEventsDTO FetchPublicEvents(string email);

        int GetInvitedToEmailCount(string str);

        //Test Method: TestFetchEventDetails()
        EventDTO FetchEventDetails(int? id);
        string GetUserEmail(int? UID);
        int GetUserIdByEmail(string email);
        int GetUserIdByEventId(int eventId);
        IEnumerable<EventDTO> FetchMyEvents(int UID);
        IEnumerable<EventDTO> FetchEventsInvitedTo(string email);
        string GetUserFullName(int userId);
        IEnumerable<EventCommentDTO> FetchCommentsOfEvent(int? id);
        EventCommentDTO GetCommentWithCommentId(int? id);
    }
}
