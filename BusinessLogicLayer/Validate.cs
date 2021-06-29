using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEventManager.Data.DataAccessLayer;
using BookEventManager.Shared;

namespace BookEventManager.Business.Logic
{
    public class Validate : IValidate
    {
        IBookReadOperations obj = (BookReadOperations)Activator.CreateInstance(typeof(BookReadOperations));

        public Validate(IBookReadOperations obj)
        {
            this.obj = obj;
        }

        //Testing Done with the method TestValidateUser()
        public Validate() { }
        public int ValidateUser(string email, string password)
        {
            email = email.ToLower();
            var cred = obj.GetCredentials();
            password = Crypt.Hash(password);
            var result = cred.Where(x => x.Email.Equals(email) && x.Password.Equals(password)).FirstOrDefault();

            if(result == null)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        //Testing Done with the method TestEmailExists()
        public int EmailExists(string email)
        {
            var users = obj.GetUsers();
            email = email == null ? null : email.ToLower();
            var emailExists = users.Where(x => x.Email.Equals(email)).Select(x => x.Email).SingleOrDefault();

            if(emailExists != null)
            {
                //Email already exists
                return -1;
            }
            return 1;
        }

    }
}
