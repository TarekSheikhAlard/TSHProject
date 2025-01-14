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
    public class AccountNotesReceivableViewModel
    {

        public int ID { get; set; }

        [Display(Name = "Code", ResourceType = typeof(chequesPayable))]
        public Nullable<int> InvoiceCode { get; set; }

        [Required(ErrorMessageResourceName = "docement_NumberError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "docement_Number", ResourceType = typeof(chequesPayable))]
        public string docementNumber { get; set; }

        [Required(ErrorMessageResourceName = "DepitError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Depit", ResourceType = typeof(chequesPayable))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "Depit", ResourceType = typeof(chequesPayable))]
 
        public string AccountNameEN { get; set; }

        [Display(Name = "AccountTreeCode", ResourceType = typeof(chequesPayable))]
        public string AccountTreeCode { get; set; }

        [Required(ErrorMessageResourceName = "costcenterError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]
        public Nullable<int> costcenterID { get; set; }

        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]
        public string costcenterName { get; set; }

       // [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof())]
        [Required(ErrorMessageResourceName = "AmountNameError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "AmountName", ResourceType = typeof(chequesPayable))]
        public Nullable<decimal> Amount { get; set; }

        [Required(ErrorMessageResourceName = "TreasuryError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Treasury", ResourceType = typeof(chequesPayable))]
        public Nullable<int> TreasuryID { get; set; }

        [Display(Name = "Treasury", ResourceType = typeof(chequesPayable))]
        public string TreasuryName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "invoiceDateError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "invoiceDate", ResourceType = typeof(chequesPayable))]
        public Nullable<System.DateTime> InvoiceDate { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(chequesPayable))]
        public string description { get; set; }
        [Display(Name = "ispost", ResourceType = typeof(chequesPayable))]
        public bool IsRelay { get; set; }

    }
}
