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
   public class BankCurrencyViewModel
    {

    

        [Display(Name = "CurrencyID", ResourceType = typeof(BankCurrency))]

        public int BankCurrencyID { get; set; }

        [Required(ErrorMessageResourceName = "CurrencyTypeError", ErrorMessageResourceType = typeof(BankCurrency))]
        [Display(Name = "CurrencyType", ResourceType = typeof(BankCurrency))]
         public string Currency_Type { get; set; }
        

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "FactorError", ErrorMessageResourceType = typeof(BankCurrency))]
        [Display(Name = "Factor", ResourceType = typeof(BankCurrency))]
    
        public Nullable<double> Factor { get; set; }


        public bool IsDeleted { get; set; }

        [Display(Name = "Isdefault", ResourceType = typeof(BankCurrency))]
        public bool Isdefault { get; set; }

        public int CompanyID { get; set; }


    }
}
