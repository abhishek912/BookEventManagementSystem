using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.Model;

namespace BookEventManager.Business.Logic
{
    public class PublicEvents
    {
        public List<Event> PastEvents { get; set; }
        public List<Event> FutureEvents { get; set; }

        public PublicEvents(List<Event> past, List<Event> future)
        {
            PastEvents = past;
            FutureEvents = future;
        }

        public PublicEvents()
        {

        }
    }
}
