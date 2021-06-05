using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Models
{
    public class Doctor
    {

        public int ID { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        [Required]
        [MinLength(3), MaxLength(60)]
        [Display(Name = "Full Name")]
        [RegularExpression("^[a-zA-Z]+(\\s[a-zA-Z]+)?$", ErrorMessage = "Only Alphabets, space allowed.")]
        public string Name { get; set; }

        //Need to be unique
        [Required]
        [MinLength(6), MaxLength(20)]
        [Unique]
        [Display(Name = "Registration Number")]
        public string RegNumber { get; set; }

        [NotMapped]
        [Display(Name = "Registred Medical Council")]
        public MedicalCouncil RegistredMedicalCouncil { get; set; }

        [Required]
        [Display(Name = "Specialization")]
        public Speciality Speciality { get; set; }

        [Required]
        [Display(Name = "Degree(s)")]
        public string Degree { get; set; }

        [Required]
        [Range(25, 100)]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Practice")]
        public Practice Practice { get; set; }

        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal Experience { get; set; }

        [Required]
        public bool IsVerified { get; set; }

        [Display(Name = "Bio")]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "Locality")]
        public string City { get; set; }

        [Required]
        [Display(Name = "District")]
        public District District { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Pincode")]
        public int Pincode { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        [Unique]
        public string EmailId { get; set; }

        [Required]
        [Phone]
        [RegularExpression("^[9][0-9]{9}$", ErrorMessage = "Enter a valid Phone number")]
        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }

        public int HospitalID { get; set; }
        public Hospital Hospital { get; set; }

        [ForeignKey(nameof(CommonAvailability))]
        public int CommonAvaliabilityID { get; set; }

        public Availability CommonAvailability { get; set; }


        [ForeignKey(nameof(CurrentAvailability))]
        public int? CurrentAvaliabilityID { get; set; }

        public Availability CurrentAvailability { get; set; }

        [Display(Name = "Account Registered Date")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Last Modified Date")]
        public DateTime? LastModified { get; set; }

        public ICollection<Experience> Experiences { get; set; }
    }


    #region Enums

    public enum Gender
    {
        Male = 0,
        Female,

    }

    public enum Practice
    {
        Government = 0,
        Private
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

    public enum ContactPreference
    {
        Always,
        EmergencyOnly,
        Never
    }

    public enum MedicalCouncil
    {
        [Display(Name = "Andhra Pradesh Medical Council")]
        AndhraPradeshMedicalCouncil,

        [Display(Name = "Arunachal Pradesh Medical Council")]
        ArunachalPradeshMedicalCouncil,

        [Display(Name = "Assam Medical Council")]
        AssamMedicalCouncil,

        [Display(Name = "Bhopal Medical Council")]
        BhopalMedicalCouncil,

        [Display(Name = "Bihar Medical Council")]
        BiharMedicalCouncil,

        [Display(Name = "Bombay Medical Council")]
        BombayMedicalCouncil,

        [Display(Name = "Chandigarh Medical Council")]
        ChandigarhMedicalCouncil,

        [Display(Name = "Chattisgarh Medical Council")]
        ChattisgarhMedicalCouncil,

        [Display(Name = "Delhi Medical Council")]
        DelhiMedicalCouncil,

        [Display(Name = "Goa Medical Council")]
        GoaMedicalCouncil,

        [Display(Name = "Gujarat Medical Council")]
        GujaratMedicalCouncil,

        [Display(Name = "Haryana Medical Council")]
        HaryanaMedicalCouncil,

        [Display(Name = "Himachal Pradesh Medical Council")]
        HimachalPradeshMedicalCouncil,

        [Display(Name = "Hyderabad Medical Council")]
        HyderabadMedicalCouncil,

        [Display(Name = "Jammu & Kashmir Medical Council")]
        JammuKashmirMedicalCouncil,

        [Display(Name = "Jharkhand Medical Council")]
        JharkhandMedicalCouncil,

        [Display(Name = "Karnataka Medical Council")]
        KarnatakaMedicalCouncil,

        [Display(Name = "Madhya Pradesh Medical Council")]
        MadhyaPradeshMedicalCouncil,

        [Display(Name = "Madras Medical Council")]
        MadrasMedicalCouncil,

        [Display(Name = "Mahakoshal Medical Council")]
        MahakoshalMedicalCouncil,

        [Display(Name = "Maharashtra Medical Council")]
        MaharashtraMedicalCouncil,

        [Display(Name = "Manipur Medical Council")]
        ManipurMedicalCouncil,

        [Display(Name = "Medical Council of India")]
        MedicalCouncilofIndia,

        [Display(Name = "Medical Council of Tanganyika")]
        MedicalCouncilofTanganyika,

        [Display(Name = "Mizoram Medical Council")]
        MizoramMedicalCouncil,

        [Display(Name = "Mysore Medical Council")]
        MysoreMedicalCouncil,

        [Display(Name = "Nagaland Medical Council")]
        NagalandMedicalCouncil,

        [Display(Name = "Orissa Council of Medical Registration")]
        OrissaCouncilofMedicalRegistration,

        [Display(Name = "Pondicherry Medical Council")]
        PondicherryMedicalCouncil,

        [Display(Name = "Punjab Medical Council")]
        PunjabMedicalCouncil,

        [Display(Name = "Rajasthan Medical Council")]
        RajasthanMedicalCouncil,

        [Display(Name = "Sikkim Medical Council")]
        SikkimMedicalCouncil,

        [Display(Name = "Tamil Nadu Medical Council")]
        TamilNaduMedicalCouncil,

        [Display(Name = "Telangana State Medical Council")]
        TelanganaStateMedicalCouncil,

        [Display(Name = "Travancore Cochin Medical Council, Trivandrum")]
        TravancoreCochinMedicalCouncil, Trivandrum,

        [Display(Name = "Tripura State Medical Council")]
        TripuraStateMedicalCouncil,

        [Display(Name = "Uttar Pradesh Medical Council")]
        UttarPradeshMedicalCouncil,

        [Display(Name = "Uttarakhand Medical Council")]
        UttarakhandMedicalCouncil,

        [Display(Name = "Vidharba Medical Council")]
        VidharbaMedicalCouncil,

        [Display(Name = "West Bengal Medical Council")]
        WestBengalMedicalCouncil
    }
    #endregion

    #region Custom ValidationAttribute
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            Doctor doctor = (Doctor)validationContext.ObjectInstance;
            if (value is not null)
            {
                var strValue = (string)value;
                Data.ApplicationDbContext context = new();
                bool IsExists = false;

                if (propertyInfo.Name.Equals("EmailId"))
                {
                    IsExists = context.Doctors.Any(d => d.EmailId.Equals(strValue) && d.ID != doctor.ID);
                }
                else if (propertyInfo.Name.Equals("RegNumber"))
                {
                    IsExists = context.Doctors.Any(d => d.RegNumber.Equals(strValue) && d.ID != doctor.ID);

                }

                if (IsExists)
                {
                    return new ValidationResult($"{strValue} already exists. Please enter valid {GetAttributeDisplayName(propertyInfo)}");

                }
                return ValidationResult.Success;

            }
            else
            {
                return new ValidationResult($"{GetAttributeDisplayName(propertyInfo)} is required");
            }
        }

        private static string GetAttributeDisplayName(System.Reflection.PropertyInfo property)
        {
            var atts = property.GetCustomAttributes(typeof(DisplayAttribute), true);
            if (atts is null || atts.Length == 0)
                return property.Name;
            return (atts[0] as DisplayAttribute).GetName();
        }
    }
    #endregion
}
