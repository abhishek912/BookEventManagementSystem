using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookEventManager.Shared.DTO
{
    public class EventDTO
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public string UserFullName { get; set; }

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

        [Range(1,4, ErrorMessage = "Enter a value between 1 and 4")]
        public byte? Duration { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(500)]
        public string OtherDetails { get; set; }

        public int CountOfInvitedTo { get; set; }

        public string InvitedTo { get; set; }
        public DateTime? EventCreateDateTime { get; set; } = DateTime.Now;
    }
}
