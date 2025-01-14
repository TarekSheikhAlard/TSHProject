using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class AdmInitialStudRegistrationViewModel
    {
        public long id { get; set; }

        [Required]
        
        public int GradeID { get; set; }

        [Display(Name = "GradeName", ResourceType = typeof(AdmInitialStudRegistration))]
        public string GradeName { get; set; }


        [Required]
        [Display(Name = "FullNameEn", ResourceType = typeof(AdmInitialStudRegistration))]
        public string FullNameEn { get; set; }

        [Required]
        [Display(Name = "FullNameAr", ResourceType = typeof(AdmInitialStudRegistration))]
        public string FullNameAr { get; set; }

        [Required]
        [Display(Name = "DOB", ResourceType = typeof(AdmInitialStudRegistration))]
        public System.DateTime DOB { get; set; }

        [Required]
        [Display(Name = "BirthPlace", ResourceType = typeof(AdmInitialStudRegistration))]
        public string BirthPlace { get; set; }

        [Required]
        
        public int Nationality { get; set; }

        [Display(Name = "Nationality", ResourceType = typeof(AdmInitialStudRegistration))]
        public string NationalityName { get; set; }

        [Required]
        [Display(Name = "Address", ResourceType = typeof(AdmInitialStudRegistration))]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Gender", ResourceType = typeof(AdmInitialStudRegistration))]
        public string Gender { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Mobile", ResourceType = typeof(AdmInitialStudRegistration))]
        public string Mobile { get; set; }

        [MaxLength(10)]
        [Display(Name = "Phone", ResourceType = typeof(AdmInitialStudRegistration))]
        public string Phone { get; set; }


        [Display(Name = "LastSchool", ResourceType = typeof(AdmInitialStudRegistration))]
        public string LastSchool { get; set; }
    
        public int CreatedbyID { get; set; }
     
      
        public int SchoolID { get; set; }

        public string Result { get; set; }
        public bool isRegistered { get; set; }

    }
}
