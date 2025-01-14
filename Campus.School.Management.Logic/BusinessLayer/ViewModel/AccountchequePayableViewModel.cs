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
    public class AccountchequePayableViewModel
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

        [Required(ErrorMessageResourceName = "Bank_BranchError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Bank_Branch", ResourceType = typeof(chequesPayable))]
        public Nullable<int> BankBranchID { get; set; }

        [Display(Name = "Bank_Branch", ResourceType = typeof(chequesPayable))]
        public string BankBranchName { get; set; }


        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "fromAccount", ResourceType = typeof(chequesPayable))]
        public int fromAccount { get; set; }

        [Display(Name = "fromAccount", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN1 { get; set; }


        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "toAccount", ResourceType = typeof(chequesPayable))]

        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "toAccount", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
        [Required(ErrorMessageResourceName = "operationDateError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "operationDate", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> operationDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceName = "collectionDateError", ErrorMessageResourceType = typeof(chequesPayable))]
        // [Display(Name = "collectionDate", ResourceType = typeof(chequesPayable))]
         
        //public Nullable<System.DateTime> collectionDate { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "AmountNameError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "AmountName", ResourceType = typeof(chequesPayable))]
        public Nullable<decimal> Amount { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(chequesPayable))]
 
        public string Notes { get; set; }


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

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "chequeNumber", ResourceType = typeof(chequesPayable))]

        public string chequeNumber { get; set; }

        [Display(Name = "DateOfIssuance", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> DateOfIssuance { get; set; }

        [Display(Name = "Deliverydate", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> Deliverydate { get; set; }

        public virtual AccountDailyJournal AccountDailyJournal { get; set; }


    }



}

