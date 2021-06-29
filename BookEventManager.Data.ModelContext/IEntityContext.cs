using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.Model;

namespace BookEventManager.Data.ModelContext
{
    public interface IEntityContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Credential> Credentials { get; set; }
        DbSet<EventComment> Comments { get; set; }
        DbSet<Event> Events { get; set; }
    }
}
