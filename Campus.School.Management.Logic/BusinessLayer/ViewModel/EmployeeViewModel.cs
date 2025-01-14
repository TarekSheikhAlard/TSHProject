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
    public class EmployeeViewModel
    {
        [Display(Name = "Employee_ID", ResourceType = typeof(Employee))]
        public int Employee_ID { get; set; }


        [Required(ErrorMessageResourceName = "NameAerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "NameA", ResourceType = typeof(Employee))]
        public string NameAr { get; set; }


        [Required(ErrorMessageResourceName = "NameEerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "NameE", ResourceType = typeof(Employee))]
        public string NameEn { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Mobileerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "Mobile", ResourceType = typeof(Employee))]
        public string Mobile { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Phoneerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "Phone", ResourceType = typeof(Employee))]
        public string Tel { get; set; }

        [Required(ErrorMessageResourceName = "Addresserror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "Address", ResourceType = typeof(Employee))]
        public string Address { get; set; }

      

        [Required(ErrorMessageResourceName = "NationalityIDerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "NationalityID", ResourceType = typeof(Employee))]
         public int NationalityID { get; set; }

        [Display(Name = "Nationality", ResourceType = typeof(Employee))]
        public string NationalityName { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "National_IDerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "National_ID", ResourceType = typeof(Employee))]
        public string National_ID { get; set; }

        [Required(ErrorMessageResourceName = "jobIDerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "jobID", ResourceType = typeof(Employee))]
        public int jobID { get; set; }

        [Display(Name = "job", ResourceType = typeof(Employee))]
        public string jobName { get; set; }

        [Display(Name = "DeptID", ResourceType = typeof(Employee))]
        public int? DeptID { get; set; }

        [Display(Name = "DepartmentName", ResourceType = typeof(Employee))]
        public string DepartmentName { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "BirthDateerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "BirthDate", ResourceType = typeof(Employee))]
        public DateTime? BirthDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "WorkDateerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "WorkDate", ResourceType = typeof(Employee))]
         public DateTime? WorkDate { get; set; }

        [Display(Name = "ImgUrl", ResourceType = typeof(Employee))]
        public string ImgUrl { get; set; }

        public bool IsNew { get; set; }

        //[System.Web.Mvc.Remote("Checkmail", "SchoolUser", ErrorMessageResourceName = "toastrEmailExist", ErrorMessageResourceType = typeof(GeneralResource))]
        [EmailAddress(ErrorMessageResourceName = "E_mailerror", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Emailerror", ErrorMessageResourceType = typeof(Employee))]
        [Display(Name = "Email", ResourceType = typeof(Employee))]
        public string Mail { get; set; }

        //------------------------
        [Display(Name = "pleaceNational_ID", ResourceType = typeof(Employee))]
        public string pleaceNational_ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "ReleaseNational_ID_date", ResourceType = typeof(Employee))]
        public Nullable<System.DateTime> ReleaseNational_ID_date { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "EndNational_ID_date", ResourceType = typeof(Employee))]
        public Nullable<System.DateTime> EndNational_ID_date { get; set; }


        [Display(Name = "EmployeeNumber", ResourceType = typeof(Employee))]
         public string EmployeeNumber { get; set; }

        [Display(Name = "BirthPlace", ResourceType = typeof(Employee))]
        public string BirthPlace { get; set; }

        [Display(Name = "Maritalstatus", ResourceType = typeof(Employee))]
        public string Maritalstatus { get; set; }

        [Display(Name = "childNumber", ResourceType = typeof(Employee))]
        public Nullable<int> childNumber { get; set; }

        [Display(Name = "companionscount", ResourceType = typeof(Employee))]
        public Nullable<int> companionscount { get; set; }

        [Display(Name = "sex", ResourceType = typeof(Employee))]
        public string sex { get; set; }

        [Display(Name = "religion", ResourceType = typeof(Employee))]
        public string religion { get; set; }

        [Display(Name = "passportNumber", ResourceType = typeof(Employee))]
        public string passportNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "Releasepassport_date", ResourceType = typeof(Employee))]
        public Nullable<System.DateTime> Releasepassport_date { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "Endpassport_date", ResourceType = typeof(Employee))]
        public Nullable<System.DateTime> Endpassport_date { get; set; }

        [Display(Name = "passportpleace", ResourceType = typeof(Employee))]
        public string passportpleace { get; set; }

        [Display(Name = "visaNumber", ResourceType = typeof(Employee))]
        public string visaNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "visadate", ResourceType = typeof(Employee))]
        public Nullable<System.DateTime> visadate { get; set; }

        [Display(Name = "visajob", ResourceType = typeof(Employee))]
        public string visajob { get; set; }


        [Display(Name = "NumberEnterBorder", ResourceType = typeof(Employee))]
        public string NumberEnterBorder { get; set; }

        [Display(Name = "EnteranceBorder", ResourceType = typeof(Employee))]
        public string EnteranceBorder { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "EnteranceDate", ResourceType = typeof(Employee))]
        public Nullable<System.DateTime> EnteranceDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "ContractDuration", ResourceType = typeof(Employee))]
        public Nullable<System.DateTime> ContractDuration { get; set; }

        [Display(Name = "JoblicenseNumber", ResourceType = typeof(Employee))]
        public string JoblicenseNumber { get; set; }

        [Display(Name = "Educationallevel", ResourceType = typeof(Employee))]
        public string Educationallevel { get; set; }

        [Display(Name = "scientificcertificate", ResourceType = typeof(Employee))]
        public string scientificcertificate { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(Employee))]
        public string Notes { get; set; }

        [Display(Name = "EmployeeStatus", ResourceType = typeof(Employee))]
        public bool EmployeeStatus { get; set; }

        public string CurrentSponsor { get; set; }

        
        public string NewSponsor { get; set; }
        public int BasePay { get; set; }
        public bool IsTerminated { get; set; }
        public string BenefitID { get; set; }
        public List<int> _BenefitID { get; set; }
        public string DeductID { get; set; }
        public List<int> _DeductID { get; set; }
        public string TaxesID { get; set; }
        public List<int> _TaxesID { get; set; }
        public string GovID { get; set; }
        public List<int> _GovID { get; set; }


        public int WorkHrs { get; set; }
        public int CompanyID { get; set; }
        public int SchoolID { get; set; }



        //[StringLength(100, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(GeneralResource), MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Required(ErrorMessageResourceName = "Passworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Display(Name = "Password", ResourceType = typeof(GeneralResource))]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessageResourceName = "ComparePassworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "ConfirmPassworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Display(Name = "ConfirmPassword", ResourceType = typeof(GeneralResource))]
        //public string ConfirmPassword { get; set; }
    }
}
