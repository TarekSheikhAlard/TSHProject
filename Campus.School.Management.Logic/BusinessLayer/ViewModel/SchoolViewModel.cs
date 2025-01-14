using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class SchoolViewModel
    {


        [Display(Name = "SchoolID", ResourceType = typeof(Schools))]
        public int SchoolID { get; set; }

        [Required(ErrorMessageResourceName = "SchoolNameEnerror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "SchoolNameEn", ResourceType = typeof(Schools))]
        public string SchoolNameEn { get; set; }

        [Required(ErrorMessageResourceName = "SchoolNameArerror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "SchoolNameAr", ResourceType = typeof(Schools))]
        public string SchoolNameAr { get; set; }

        [Required(ErrorMessageResourceName = "CityIDerror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "CityID", ResourceType = typeof(Schools))]
        public Nullable<int> CityID { get; set; }

        [Display(Name = "CityName", ResourceType = typeof(Schools))]
        public string CityName { get; set; }

        [Required(ErrorMessageResourceName = "Addresserror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "Address", ResourceType = typeof(Schools))]

        public string Address { get; set; }


        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "phoneerror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "phone", ResourceType = typeof(Schools))]

        public string phone { get; set; }
        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Mobileerror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "Mobile", ResourceType = typeof(Schools))]

        public string Mobile { get; set; }

        [Display(Name = "Logo", ResourceType = typeof(Schools))]
        public string Logo { get; set; }

        [Required(ErrorMessageResourceName = "CompanyIDerror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "CompanyID", ResourceType = typeof(Schools))]
        public int CompanyID { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Schools))]
        public string CompanyName { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Fax", ResourceType = typeof(Schools))]
        public string Fax { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Schools))]
        public string Website { get; set; }

        [Display(Name = "Facebook", ResourceType = typeof(Schools))]
        public string Facebook { get; set; }

        [Display(Name = "whatsApp", ResourceType = typeof(Schools))]
        public string whatsApp { get; set; }

        [Display(Name = "SmsSender", ResourceType = typeof(Schools))]

        public string SmsSender { get; set; }

        [Display(Name = "PostCode", ResourceType = typeof(Schools))]

        public string PostCode { get; set; }

        public bool IsNew { get; set; }

        [Required(ErrorMessageResourceName = "LicenseNoerror", ErrorMessageResourceType = typeof(Schools))]

        [Display(Name = "LicenseNo", ResourceType = typeof(Schools))]

        public string LicenseNo { get; set; }

        [Required(ErrorMessageResourceName = "SchSuperVisionerror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "SchSuperVision", ResourceType = typeof(Schools))]

        public string SchSuperVision { get; set; }
        public string Url { get; set; }
        public string ConnectionString { get; set; }

        //[System.Web.Mvc.Remote("CheckEmail", "SchoolUser", ErrorMessageResourceName = "toastrEmailExist", ErrorMessageResourceType = typeof(GeneralResource))]
        [EmailAddress(ErrorMessageResourceName = "E_mailerror", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "Emailerror", ErrorMessageResourceType = typeof(Schools))]
        [Display(Name = "Email", ResourceType = typeof(Schools))]

        public string Email { get; set; }


        [StringLength(100, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(GeneralResource), MinimumLength = 6)]
        [DataType(DataType.Password)]
        //[Required(ErrorMessageResourceName = "Passworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Password", ResourceType = typeof(GeneralResource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessageResourceName = "ComparePassworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        //[Required(ErrorMessageResourceName = "ConfirmPassworderror", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(GeneralResource))]
        public string ConfirmPassword { get; set; }

        public string Location { get; set; } 

    }
}
