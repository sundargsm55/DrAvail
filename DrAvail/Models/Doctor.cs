using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class Doctor
    {

        public class Avaliablity
        {
            public int MyProperty { get; set; }
        }
        public Avaliablity common;
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Speciality { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public decimal Experience { get; set; }

        public string Summary { get; set; }
        
        public Hospital Hospital { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public District District { get; set; }
    }

    public enum Speciality
    {
        [Display(Name = "Siddha", Description = "The Siddha system is based on a combination of ancient medicinal practices and spiritual disciplines as well as alchemy and mysticism")]
        Siddha,

        [Display(Name = "Allergist", Description = "Specializes in determining food and environmental allergies")]
        Allergist,

        [Display(Name = "Anesthesiologist", Description = "Specializes in pain prevention during surgery")]
        Anesthesiologist,

        [Display(Name = "Cardiologist", Description = "Heart specialist")]
        Cardiologist,

        [Display(Name = "Chiropractor", Description = "Back specialist")]
        Chiropractor,

        [Display(Name = "Dentist", Description = "Tooth specialist")]
        Dentist,

        [Display(Name = "Dermatologist", Description = "Skin specialist")]
        Dermatologist,

        [Display(Name = "Fertility Specialist", Description = "Helps people who have difficulty getting pregnant")]
        FertilitySpecialist,

        [Display(Name = "Gynecologist", Description = "Specializes in women's needs")]
        Gynecologist,

        [Display(Name = "Massage Therapist", Description = "Specializes in muscle relaxation")]
        MassageTherapist,

        [Display(Name = "Naturopath", Description = "Specializes in natural cures and remedies")]
        Naturopath,

        [Display(Name = "Neurologist", Description = "Brain specialist")]
        Neurologist,

        [Display(Name = "Obstetrician", Description = "Specialist for pregnant women")]
        Obstetrician,

        [Display(Name = "Occupational Therapist", Description = "Specializes in workplace health")]
        OccupationalTherapist,

        [Display(Name = "Oncologist", Description = "Tumour specialist, including cancer")]
        Oncologist,

        [Display(Name = "Ophthalmologist", Description = "Specializes in eye diseases")]
        Ophthalmologist,

        [Display(Name = "Pediatrician", Description = "Specialist for babies and children")]
        Pediatrician,

        [Display(Name = "Physical Therapist", Description = "Specializes in the body's movement")]
        PhysicalTherapist,

        [Display(Name = "Podiatrist", Description = "Foot specialist")]
        Podiatrist,

        [Display(Name = "Psychiatrist", Description = "Specialist in mental health")]
        Psychiatrist,

        [Display(Name = "Radiologist", Description = "Specializes in imaging tests")]
        Radiologist,

        [Display(Name = "General Surgeons", Description = "Operate on all parts of your body")]
        GeneralSurgeons,

        [Display(Name = "General Physician", Description = "Highly trained specialists who provide a range of non-surgical health care to adult patients")]
        GeneralPhysician
    }

    
}
