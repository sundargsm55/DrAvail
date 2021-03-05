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
        public List<Doctor> Doctors { get; set; }
        public SelectList Speciality { get; set; }

        public SelectList City { get; set; }

        public string SearchString { get; set; }
    }

}
