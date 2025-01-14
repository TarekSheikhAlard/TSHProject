using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;
using System.Web.Mvc;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public  class BankViewModel
    {
      
        [Display(Name = "BankID", ResourceType = typeof(Bank))]
         public int BankID { get; set; }

          //[Required(ErrorMessageResourceName = "bankArError", ErrorMessageResourceType = typeof(Bank))]
          [Display(Name ="ArabicBankName", ResourceType = typeof(Bank))]
          public string BankNameAr { get; set; }


        [Required(ErrorMessageResourceName = "bankError", ErrorMessageResourceType = typeof(Bank))]
        [Display(Name = "BankEnglishName", ResourceType = typeof(Bank))]
        public string BankNameEn { get; set; }


        //[Required(ErrorMessageResourceName = "bankAccArError", ErrorMessageResourceType = typeof(Bank))]
        [Display(Name = "ArabicBankAccName", ResourceType = typeof(Bank))]
        public string BankAccountNameAr { get; set; }


        [Required(ErrorMessageResourceName = "bankAccError", ErrorMessageResourceType = typeof(Bank))]
        [Display(Name = "BankAccEnglishName", ResourceType = typeof(Bank))]
        public string BankAccountNameEn { get; set; }

        [Required(ErrorMessageResourceName = "BankBranchNameError", ErrorMessageResourceType = typeof(Bank))]
        [Display(Name = "BankBranchName", ResourceType = typeof(Bank))]
        public string BranchName { get; set; }

        [Required(ErrorMessageResourceName = "DescriptionError", ErrorMessageResourceType = typeof(Bank))]
        [Display(Name = "Description", ResourceType = typeof(Bank))]
        public string Description { get; set; }

        
        [Display(Name = "OpeningBalance", ResourceType = typeof(Bank))]
        public decimal OpeningBalance { get; set; }

        [Required(ErrorMessageResourceName = "AccountNumberError", ErrorMessageResourceType = typeof(Bank))]
        [Display(Name = "AccountNumber", ResourceType = typeof(Bank))]
        public string AccountNumber { get; set; }

        
        public int AccountTreeID { get; set; }
        public int CompanyID { get; set; }

    }
}
