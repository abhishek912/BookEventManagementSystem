using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.Model;

namespace BookEventManager.Shared.DTO
{
    public class PublicEventsDTO
    {
        public List<Event> PastEvents { get; set; }
        public List<Event> FutureEvents { get; set; }

        public PublicEventsDTO(List<Event> past, List<Event> future)
        {
            PastEvents = past;
            FutureEvents = future;
        }

        public PublicEventsDTO()
        {

        }
    }
}
