using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookEventManagementSystem.Models
{
    public class EntityContext : DbContext
    {
        public EntityContext():base("EntityContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<EventComment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}