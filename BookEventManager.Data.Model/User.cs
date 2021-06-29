using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Security;

namespace BookEventManager.Data.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z ]+")]
        public string FullName { get; set; }
        
        [Index(IsUnique = true)]
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MembershipPassword(
        MinRequiredNonAlphanumericCharacters = 1,
        ErrorMessage = "Your password must be 5 characters long and contain at least one wildsymbol ",
        MinRequiredPasswordLength = 5)]
        public string Password { get; set; }
    }
}