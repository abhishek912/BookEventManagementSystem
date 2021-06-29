using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using BookEventManager.Business.Logic;
using BookEventManager.Data.Model;
using BookEventManager.Data.DataAccessLayer;
using BookEventManager.Data.ModelContext;
using System.Collections.Generic;
using BookEventManager.Shared.DTO;
using BookEventManager.Shared;

namespace BookEventManager.Test
{
    [TestClass]
    public class TestReadData
    {
        [TestMethod]
        public void TestFetchEventDetails()
        {
            Mock<IBookReadOperations> mockBookReadOperations = new Mock<IBookReadOperations>();
            
            /*
             * Mocking the Data Access Layer functions
             * function1: GetEvents()
             * function2: GetUsers()
             */

            mockBookReadOperations.Setup(t => t.GetEvents()).Returns(GetEventList());
            mockBookReadOperations.Setup(t => t.GetUsers()).Returns(GetUserList());

            int eventId = 1;
            ReadData readObj = new ReadData(mockBookReadOperations.Object);
            EventDTO @event = readObj.FetchEventDetails(eventId);
            Assert.IsNotNull(@event);
        }

        [TestMethod]
        public void TestFetchPublicEvents()
        {
            Mock<IBookReadOperations> mockBookReadOperations = new Mock<IBookReadOperations>();

            mockBookReadOperations.Setup(t => t.GetCredentials()).Returns(GetCredList());
            mockBookReadOperations.Setup(t => t.GetEvents()).Returns(GetEventList());

            //Testing FetchPublicEvents method if the logged in user is admin
            string adminEmail = "myadmin@bookevents.com";
            ReadData readObj = new ReadData(mockBookReadOperations.Object);
            var events = readObj.FetchPublicEvents(adminEmail);
            Assert.IsNotNull(events);

            //Testing FetchPublicEvents method if the logged in user is regular/normal user
            string regularUserEmail = "abhishek.sharma11@nagarro.com";
            events = readObj.FetchPublicEvents(regularUserEmail);
            Assert.IsNotNull(events);

            //Testing FetchPublicEvents method if User is not logged in
            events = readObj.FetchPublicEvents();
            Assert.IsNotNull(events);
        }

        [TestMethod]
        public void TestValidateUser()
        {
            Mock<IBookReadOperations> mockBookReadOperations = new Mock<IBookReadOperations>();

            mockBookReadOperations.Setup(t => t.GetCredentials()).Returns(GetCredList());

            Validate v = new Validate(mockBookReadOperations.Object);

            //Validate if the given information is correct
            string email = "abhishek.sharma11@nagarro.com", password = "1234";
            int result = v.ValidateUser(email, password);
            Assert.AreEqual(result, 1);

            email = "abhishek.sharma11@nagarro.com";
            password = "admin";
            result = v.ValidateUser(email, password);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void TestEmailExists()
        {
            Mock<IBookReadOperations> mockBookReadOperations = new Mock<IBookReadOperations>();

            mockBookReadOperations.Setup(t => t.GetUsers()).Returns(GetUserList());

            Validate v = new Validate(mockBookReadOperations.Object);

            //Validate if email already exixts in the User Database at the time of signup
            string email = "abhishek.sharma11@nagarro.com";
            int result = v.EmailExists(email);
            Assert.AreEqual(result, -1);

            //Validate if email does not exixts in the User Database at the time of signup
            email = "abhishek.mcs19.du@gmail.com";
            result = v.EmailExists(email);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void TestDeleteEvent()
        {
            Mock<IBookReadOperations> mockBookReadOperations = new Mock<IBookReadOperations>();
            Mock<IBookWriteOperations> mockBookWriteOperations = new Mock<IBookWriteOperations>();

            int eventId = 1;
            Event e = GetEventList()[0];

            mockBookReadOperations.Setup(t => t.GetEvents()).Returns(GetEventList());
            mockBookReadOperations.Setup(t => t.GetUsers()).Returns(GetUserList());
            mockBookWriteOperations.Setup(t => t.DeleteEvent(It.IsAny<Event>())).Returns(DeleteEvent(e));

            WriteData writeObj = new WriteData(mockBookWriteOperations.Object);
            int r = writeObj.DeleteEvent(mockBookReadOperations.Object, eventId);

            Assert.AreNotEqual(r, 0);

        }

        private int DeleteEvent(Event e)
        {
            var events = GetEventList();
            foreach(var @event in events)
            {
                if(@event.EventId == e.EventId)
                {
                    //Event deleted successfully
                    events.Remove(@event);
                    return 1;
                }
            }
            return -1;
        }

        private List<Event> GetEventList()
        {
            List<Event> @event = new List<Event>
            {
                new Event
                {
                    EventId = 1,
                    UserId = 1,
                    DateOfEvent = DateTime.Now,
                    Location = "Delhi",
                    StartTime = "10:00",
                    Title = "Fun Event",
                    Type = true
                },

                new Event
                {
                    EventId = 2,
                    UserId = 2,
                    DateOfEvent = DateTime.Parse("20-02-2020 00:00:00"),
                    Location = "Delhi",
                    StartTime = "10:00",
                    Title = "Fun Event",
                    Type = true
                },

                new Event
                {
                    EventId = 3,
                    UserId = 1,
                    DateOfEvent = DateTime.Parse("20-02-2022 00:00:00"),
                    Location = "Delhi",
                    StartTime = "10:00",
                    Title = "Fun Event",
                    Type = false
                }
            };
            return @event;
        }

        private List<User> GetUserList()
        {
            List<User> user = new List<User>
            {
                new User
                {
                    UserId = 1,
                    Email = "abhishek.sharma11@nagarro.com",
                    Password = Crypt.Hash("1234"),
                    FullName = "Abhishek Sharma"
                },
                new User
                {
                    UserId = 2,
                    Email = "myadmin@bookevents.com",
                    Password = Crypt.Hash("admin"),
                    FullName = "Admin"
                }
            };
            return user;
        }

        private List<Credential> GetCredList()
        {
            List<Credential> cred = new List<Credential>
            {
                new Credential{ Email = "abhishek.sharma11@nagarro.com", Password = Crypt.Hash("1234") },
                new Credential{ Email = "myadmin@bookevents.com", Password = Crypt.Hash("admin") }
            };

            return cred;
        }
    }
}
