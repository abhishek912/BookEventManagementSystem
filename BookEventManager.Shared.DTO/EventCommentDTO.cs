using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEventManager.Shared.DTO
{
    public class EventCommentDTO
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int EventId { get; set; }
        public DateTime? CurrentDate { get; set; } = DateTime.Now;
    }
}
