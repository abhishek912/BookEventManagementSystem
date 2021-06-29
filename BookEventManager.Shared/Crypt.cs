using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEventManager.Shared
{
    public class Crypt
    {
        public static string Hash(string val)
        {
            if(val == null)
            {
                return null;
            }
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(val))
                );
        }
    }
}
