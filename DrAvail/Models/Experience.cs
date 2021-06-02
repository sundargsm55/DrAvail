using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class Experience
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string EmployementType { get; set; }

        public string HospitalClinicName { get; set; }

        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(Doctor))]
        public int DoctorID { get; set; }

        public Doctor Doctor { get; set; }

    }
}
