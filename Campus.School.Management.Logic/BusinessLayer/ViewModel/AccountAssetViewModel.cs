using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public  class AccountAssetViewModel
    {
        public int ID { get; set; }


        [Required(ErrorMessageResourceName = "NameError", ErrorMessageResourceType = typeof(AccountAsset))]
        [Display(Name = "Name", ResourceType = typeof(AccountAsset))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "NameArError", ErrorMessageResourceType = typeof(AccountAsset))]
        [Display(Name = "NameAr", ResourceType = typeof(AccountAsset))]
        public string NameAr { get; set; }

        
        [Display(Name = "AccountTreeID", ResourceType = typeof(AccountAsset))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "AccountTreeID", ResourceType = typeof(AccountAsset))]
        public string AccountTree { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "AmountError", ErrorMessageResourceType = typeof(AccountAsset))]
        [Display(Name = "Amount", ResourceType = typeof(AccountAsset))]
        public Nullable<decimal> Amount { get; set; }


        [Display(Name = "Code", ResourceType = typeof(AccountAsset))]
        public string Code { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(AccountAsset))]
        public string Notes { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "PurchaseDateError", ErrorMessageResourceType = typeof(AccountAsset))]
        [Display(Name = "PurchaseDate", ResourceType = typeof(AccountAsset))]
        public Nullable<System.DateTime> PurchaseDate { get; set; }



        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "depreciationRate", ResourceType = typeof(AccountAsset))]
        public Nullable<decimal> depreciationRate { get; set; }



        [Display(Name = "depreciationType", ResourceType = typeof(AccountAsset))]
        public string depreciationType { get; set; }


        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "depreciationYears", ResourceType = typeof(AccountAsset))]
        public Nullable<int> depreciationYears { get; set; }

        public int CompanyID { get; set; }
        public int SchoolID { get; set; }


        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "AssetTree")]
        public int? AssetTreeID { get; set; }


        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Account Expense")]
        public int? AccountExpenseID { get; set; }

        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Account Accumulated")]
        public int? AccountAccumulatedID { get; set; }

        //[Required]
        [Display(Name = "Department")]
        public int? DepartmentID { get; set; }



        [Display(Name = "AssetTree")]
        public string AssetNameEN { get; set; }
       
        public string AccountExpenseName { get; set; }
        
        public string AccountAccumulatedName { get; set; }
        //[Display(Name = "AssetTree")]
        //public string DepartmentName { get; set; }


    }
}
