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
   public class CompanyViewModel
    {
      
        [Display(Name = "CompanyID", ResourceType = typeof(Company))]
        public int CompanyID { get; set; }

        [Required(ErrorMessageResourceName = "CompanyNameArerror", ErrorMessageResourceType = typeof(Company))]
        [Display(Name = "CompanyNameAr", ResourceType = typeof(Company))]
        public string CompanyNameAr { get; set; }

        [Required(ErrorMessageResourceName = "CompanyNameEnerror", ErrorMessageResourceType = typeof(Company))]
        [Display(Name = "CompanyNameEn", ResourceType = typeof(Company))]
        public string CompanyNameEn { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "CompanyPhoneerror", ErrorMessageResourceType = typeof(Company))]
        [Display(Name = "CompanyPhone", ResourceType = typeof(Company))]
        public string CompanyPhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "CompanyMobileerror", ErrorMessageResourceType = typeof(Company))]
        [Display(Name = "CompanyMobile", ResourceType = typeof(Company))]
        public string CompanyMobile { get; set; }

        [Display(Name = "Logo", ResourceType = typeof(Company))]
        public string Logo { get; set; }

        public bool showArabic { get; set; }
    }
}
