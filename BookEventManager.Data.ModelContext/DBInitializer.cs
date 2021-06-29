using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookEventManager.Data.Model;
using BookEventManager.Shared;

namespace BookEventManager.Data.ModelContext
{
    public class DBInitializer : System.Data.Entity.CreateDatabaseIfNotExists<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            var login = new List<Credential>
            {
                new Credential{  Email=Admin.AdminEmail[0], Password=Crypt.Hash("admin") }
            };
            login.ForEach(s => context.Credentials.Add(s));
            context.SaveChanges();

            var users = new List<User>
            {
                new User{ Email=Admin.AdminEmail[0], FullName="Admin", Password=Crypt.Hash("admin") }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}