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
        [EmailAddress]
        [Display(Name = "User Email")]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Display(Name = "Message Type")]

        public MessageType MessageType { get; set; } = MessageType.UserToAdmin;

        [Column(name: "Message")]
        [Required]
        public string MessageText { get; set; }

        [Required]
        [Display(Name ="Date Sent")]
        public DateTime DateSent { get; set; } = DateTime.Now;

        [Display(Name =" Date Responded")]
        public DateTime? DateResponded { get; set; }

        [Display(Name = "Admin Response")]
        public string AdminResponse { get; set; }

        [Display(Name ="User Response(s)")]
        public string UserResponse { get; set; }

        [Display(Name ="Admin Email")]
        [EmailAddress]
        public string AdminEmail { get; set; }
    }

    public enum MessageType
    {
        [Display(Name = "From User to Admin")]
        UserToAdmin,
        [Display(Name = "From Admin to User")]
        AdminToUser
    }
}
