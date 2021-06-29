using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookEventManager.Data.Model
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime DateOfEvent { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string Location { get; set; }

        // Type either private or public, default it is public i.e. true
        public bool? Type { get; set; } = true;

        [Range(1, 4, ErrorMessage = "Enter a value between 1 and 4")]
        public byte? Duration { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(500)]
        public string OtherDetails { get; set; }

        public string InvitedTo { get; set; }
        public DateTime? EventCreateDateTime { get; set; } = DateTime.Now;
    }
}