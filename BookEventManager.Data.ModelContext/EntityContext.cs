using BookEventManager.Data.Model;
using System.Data.Entity;

namespace BookEventManager.Data.ModelContext
{
    public class EntityContext : DbContext, IEntityContext
    {
        //public EntityContext() : base("DBConnection")
        public EntityContext() : base("EntityContext")
        {
            //Database.CreateIfNotExists();
            Database.SetInitializer<EntityContext>(new DBInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<EventComment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}