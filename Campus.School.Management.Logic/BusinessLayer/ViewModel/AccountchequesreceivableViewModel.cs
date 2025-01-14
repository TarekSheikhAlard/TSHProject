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
     public  class AccountchequesreceivableViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Code", ResourceType = typeof(chequesPayable))]

        public string InvoiceCode { get; set; }

        [Required(ErrorMessageResourceName = "docement_NumberError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "docement_Number", ResourceType = typeof(chequesPayable))]
        public string docementNumber { get; set; }

        [Required(ErrorMessageResourceName = "costcenterError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]

        public Nullable<int> costcenterID { get; set; }
       
        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]
         public string costcenterName { get; set; }

        [Display(Name = "Bank_Branch", ResourceType = typeof(chequesPayable))]
        [Required(ErrorMessageResourceName = "Bank_BranchError", ErrorMessageResourceType = typeof(chequesPayable))]

        public Nullable<int> BankBranchID { get; set; }

        [Display(Name = "Bank_Branch", ResourceType = typeof(chequesPayable))]
        public string BankBranchName { get; set; }


        [Required(ErrorMessageResourceName = "CreditError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Credit", ResourceType = typeof(chequesPayable))]
        public int fromAccount { get; set; }

        [Display(Name = "Credit", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN1 { get; set; }


        [Required(ErrorMessageResourceName = "DepitError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Depit", ResourceType = typeof(chequesPayable))]
      
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "Depit", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "operationDateError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "operationDate", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> operationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
      // [Required(ErrorMessageResourceName = "collection_DateError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "collection_Date", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> collectionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cheque_Date", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> ChequeDate { get; set; }

        [Required(ErrorMessageResourceName = "AmountNameError", ErrorMessageResourceType = typeof(chequesPayable))]

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "AmountName", ResourceType = typeof(chequesPayable))]
        public Nullable<decimal> Amount { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(chequesPayable))]
        public string Notes { get; set; }

        [Display(Name = "isCollected", ResourceType = typeof(chequesPayable))]
      
        public bool isCollected { get; set; }


        [Display(Name = "CurrencyType", ResourceType = typeof(chequesPayable))]
        public Nullable<int> BankCurrencyID { get; set; }


        [Display(Name = "CurrencyType", ResourceType = typeof(chequesPayable))]
        public string Currency_Type { get; set; }

        [Display(Name = "Emp_Name", ResourceType = typeof(chequesPayable))]
        public Nullable<int> Employee_ID { get; set; }

        [Display(Name = "Emp_Name", ResourceType = typeof(chequesPayable))]
        public string NameA { get; set; }

        public virtual AccountDailyJournal AccountDailyJournal { get; set; }
    }
}
