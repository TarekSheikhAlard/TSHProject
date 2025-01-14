using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;
using Campus.School.Management.Logic.DataBaseLayer;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class BankBranchViewModel
    {


        

        [Display(Name = "ID", ResourceType = typeof(Bank_Branch))] 
        public int ID { get; set; }

        public int CompanyID { get; set; }


        [Required(ErrorMessageResourceName = "EnglishBranchNameError", ErrorMessageResourceType = typeof(Bank_Branch))]
        [Display(Name = "EnglishBranchName", ResourceType = typeof(Bank_Branch))]
        public string BranchNameEn { get; set; }

        [Required(ErrorMessageResourceName = "ArabicBranchNameError", ErrorMessageResourceType = typeof(Bank_Branch))]
        [Display(Name = "ArabicBranchName", ResourceType = typeof(Bank_Branch))]
        public string BranchNameAr { get; set; }

        [Required(ErrorMessageResourceName = "BankIDError", ErrorMessageResourceType = typeof(Bank_Branch))]
        [Display(Name = "BankID", ResourceType = typeof(Bank_Branch))]
        public int BankID { get; set; }

         [Display(Name = "BankName", ResourceType = typeof(Bank_Branch))]
         public string BankNameEn { get; set; }

        [Required(ErrorMessageResourceName = "BranchAddressError", ErrorMessageResourceType = typeof(Bank_Branch))]
        [Display(Name = "BranchAddress", ResourceType = typeof(Bank_Branch))]
        public string BranchAddress { get; set; }

        [DataType(DataType.PhoneNumber)]

        [Required(ErrorMessageResourceName = "BranchPhoneError", ErrorMessageResourceType = typeof(Bank_Branch))]
        [Display(Name = "BranchPhone", ResourceType = typeof(Bank_Branch))]
        [RegularExpression("^\\d+", ErrorMessage = "phone Must be Number")]    
        public string BranchPhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\d+", ErrorMessage = "Mobile Must be Number")]

        [Required(ErrorMessageResourceName = "BranchMobileError", ErrorMessageResourceType = typeof(Bank_Branch))]
        [Display(Name = "BranchMobile", ResourceType = typeof(Bank_Branch))]
        public string BranchMobile { get; set; }

        [Required(ErrorMessageResourceName = "BranchFaxError", ErrorMessageResourceType = typeof(Bank_Branch))]
        [Display(Name = "BranchFax", ResourceType = typeof(Bank_Branch))]
         public string BranchFax { get; set; }

        [Required(ErrorMessageResourceName = "AccountTreeIDError", ErrorMessageResourceType = typeof(Treasury))]
        [Display(Name = "AccountTreeID", ResourceType = typeof(Treasury))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "AccountNameAR", ResourceType = typeof(Treasury))]
        public string AccountNameAR { get; set; }



        //public virtual ICollection<DataBaseLayer.BranchAccount> BranchAccounts { get; set; }

        public virtual ICollection<Accountchequesreceivable> Accountchequesreceivables { get; set; }
        public virtual ICollection<AccountchequePayable> AccountchequePayables { get; set; }

        public virtual ICollection<DataBaseLayer.AccountCustomer> AccountCustomers { get; set; }
        public virtual ICollection<AccountSupplier> AccountSuppliers { get; set; }


    }
}
