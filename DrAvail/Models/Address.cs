using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class Address
    {
        public string address { get; set; }

        public string city { get; set; }

        public District District { get; set; }
    }
}
