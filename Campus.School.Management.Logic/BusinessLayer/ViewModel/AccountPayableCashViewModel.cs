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
     public class AccountPayableCashViewModel
    {


        public int ID { get; set; }

        [Display(Name = "Code", ResourceType = typeof(chequesPayable))]
        public string InvoiceCode { get; set; }

        [Required(ErrorMessageResourceName = "docement_NumberError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "docement_Number", ResourceType = typeof(chequesPayable))]
        public string docementNumber { get; set; }

        [Required(ErrorMessageResourceName = "costcenterError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]
        public Nullable<int> CostCenterID { get; set; }

        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]
        public string costcenterName { get; set; }

        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "fromAccount", ResourceType = typeof(chequesPayable))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "fromAccount", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN { get; set; }

        [Required(ErrorMessageResourceName = "TreasuryError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "TreasuryIDCredit", ResourceType = typeof(chequesPayable))]
        public Nullable<int> TreasuryID { get; set; }

        [Display(Name = "TreasuryIDCredit", ResourceType = typeof(chequesPayable))]
        public string TreasuryName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "invoiceDateError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "invoiceDate", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> OperationDate { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "AmountNameError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "AmountName", ResourceType = typeof(chequesPayable))]
        public Nullable<decimal> Amount { get; set; }


        [Display(Name = "Notes", ResourceType = typeof(chequesPayable))]
         public string Description { get; set; }



        [Display(Name = "CurrencyType", ResourceType = typeof(chequesPayable))]
        public Nullable<int> BankCurrencyID { get; set; }


        [Display(Name = "CurrencyType", ResourceType = typeof(chequesPayable))]
        public string Currency_Type { get; set; }


        [Display(Name = "Emp_Name1", ResourceType = typeof(chequesPayable))]
        public Nullable<int> Employee_ID { get; set; }

        [Display(Name = "Emp_Name1", ResourceType = typeof(chequesPayable))]
        public string NameA { get; set; }


        [Display(Name = "RecipientName", ResourceType = typeof(chequesPayable))]
        public string RecipientName { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "RecipientTel", ResourceType = typeof(chequesPayable))]
        public string RecipientTel { get; set; }

        public virtual AccountDailyJournal AccountDailyJournal { get; set; }


    }
}
