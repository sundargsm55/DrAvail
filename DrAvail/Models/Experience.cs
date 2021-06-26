using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class Experience
    {
        public int ID { get; set; }

        public string OwnerID { get; set; }
        public string Title { get; set; }
        [Display(Name ="Employement Type")]
        public string EmployementType { get; set; }

        [Display(Name = "Hospital or Clinic Name")]
        public string HospitalClinicName { get; set; }

        public string Location { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        
        public bool IsEndDatePresent { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int DoctorID { get; set; }

        public Doctor Doctor { get; set; }

    }
}
