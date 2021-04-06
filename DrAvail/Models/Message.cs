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
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Column(name: "Message")]
        [Required]
        public string MessageText { get; set; }

        [Required]
        public DateTime DateReceived { get; set; } = DateTime.Now;

        public DateTime? DateResponded { get; set; }

        public string AdminResponse { get; set; }

    }
}
