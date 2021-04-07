using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class Message
    {
        public int ID { get; set; }

        [Required]
        public string IP { get; set; }

        [Required]
        [Display(Name = "User Email")]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Column(name: "Message")]
        [Required]
        public string MessageText { get; set; }

        [Required]
        [Display(Name ="Date Received")]
        public DateTime DateReceived { get; set; } = DateTime.Now;

        [Display(Name =" Date Responded")]
        public DateTime? DateResponded { get; set; }

        [Display(Name = "Admin Response")]
        public string AdminResponse { get; set; }

    }
}
