using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookEventManagementSystem.Models
{
    public class EventComment
    {
        [Key]
        public int CommentId { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime? CurrentDate { get; set; } = DateTime.Now;
    }
}