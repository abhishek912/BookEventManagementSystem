using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookEventManager.Data.Model
{
    public class Credential
    {
        [Key]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}