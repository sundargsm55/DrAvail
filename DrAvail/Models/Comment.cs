using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int DoctorID { get; set; }

        public Doctor Doctor { get; set; }

        public string OwnerID { get; set; }

        public DateTime CommentDate { get; set; }

        [ForeignKey(nameof(Reply))]
        public int? ReplyID { get; set; }

        public Comment Reply { get; set; }

        public string Message { get; set; }
    }
}
