using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEventManager.Business.Logic
{
    interface IValidate
    {
        int ValidateUser(string email, string password);
        int EmailExists(string email);
    }
}
