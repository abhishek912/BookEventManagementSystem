using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookEventManagementSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        
        [Index(IsUnique = true)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public Credential Credential { set; get; }
    }
}