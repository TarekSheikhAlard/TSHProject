using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
  public  class BranchAccountViewModel
    {




        [Display(Name = "BankAccountID", ResourceType = typeof(BranchAccount))]
        public int AccountID { get; set; }


        public int CompanyID { get; set; }


        [Required(ErrorMessageResourceName = "AccountNumberError", ErrorMessageResourceType = typeof(BranchAccount))]
        [Display(Name = "AccountNumber", ResourceType = typeof(BranchAccount))]
        public string Account_Number { get; set; }



        [Required(ErrorMessageResourceName = "BankIDError", ErrorMessageResourceType = typeof(BranchAccount))]
        [Display(Name = "BankID", ResourceType = typeof(BranchAccount ))]
         public Nullable<int> BankID { get; set; }

        [Display(Name = "EnglishBankName", ResourceType = typeof(BranchAccount))]
        public string BankNameEn { get; set; }



        [Required(ErrorMessageResourceName = "BranchIDError", ErrorMessageResourceType = typeof(BranchAccount))]
        [Display(Name = "BranchID", ResourceType = typeof(BranchAccount))]
        public int BranchID { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(BranchAccount))]
        public string BranchName { get; set; }


        [Required(ErrorMessageResourceName = "AccountCurrencyError", ErrorMessageResourceType = typeof(BranchAccount))]
        [Display(Name = "AccountCurrency", ResourceType = typeof(BranchAccount))]
        public Nullable<int> BankCurrencyID { get; set; }

        [Display(Name = "CurrencyType", ResourceType = typeof(BranchAccount))]
         public string Currency_Type { get; set; }



    }
}
