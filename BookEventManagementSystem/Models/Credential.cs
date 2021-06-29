using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookEventManagementSystem.Models
{
    public class Credential
    {
        [Key]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}