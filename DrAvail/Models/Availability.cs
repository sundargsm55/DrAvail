using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class Avaliability
    {
        public int ID { get; set; }
        public string status { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ContactPreference ContactPreference { get; set; }

        public Hospital Hospital { get; set; }

        public Doctor Doctor { get; set; }

    }
}
