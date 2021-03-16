using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class DoctorsViewModel
    {
        public Doctor Doctor { get; set; }
        public Availability CommonAvailability { get; set; }
        public Availability CurrentAvailability { get; set; }

        public Hospital Hospital { get; set; }

    }

}
