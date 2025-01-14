using Campus.School.Management.Logic.DataBaseLayer;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;
namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class AccountDailyJournalViewModel
    {

      //[Required(ErrorMessageResourceName = "academicYearError", ErrorMessageResourceType = typeof(DailyJornal))]
        [Display(Name = "DailyJournal_ID", ResourceType = typeof(DailyJornal))]

        public int DailyJournalID { get; set; }
        [Display(Name = "Debit", ResourceType = typeof(DailyJornal))]
     
        public decimal Debit { get; set; }

        [Display(Name = "Credit", ResourceType = typeof(DailyJornal))]
        public decimal Credit { get; set; }

        //[Required(ErrorMessageResourceName = "NoteError", ErrorMessageResourceType = typeof(DailyJornal))]

        [Display(Name = "Note", ResourceType = typeof(DailyJornal))]
        public string Note { get; set; }

       // [Required(ErrorMessageResourceName = "Note1Error", ErrorMessageResourceType = typeof(DailyJornal))]
        [Display(Name = "Note1", ResourceType = typeof(DailyJornal))]

        public string Note1 { get; set; }

        //[Required(ErrorMessageResourceName = "Document_NumberError", ErrorMessageResourceType = typeof(DailyJornal))]
        [Display(Name = "Document_Number", ResourceType = typeof(DailyJornal))]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessageResourceName = "Serial_NoError", ErrorMessageResourceType = typeof(DailyJornal))]
        [Display(Name = "Serial_No", ResourceType = typeof(DailyJornal))]
        public string SerialNo { get; set; }

        

    

        [Display(Name = "PhysicalYear", ResourceType = typeof(DailyJornal))]
 
        public int PhysicalYearID { get; set; }
        [Display(Name = "PhysicalYear", ResourceType = typeof(DailyJornal))]
        public virtual string PhysicalYearName { get; set; }

       [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "DailyJournal_DateError", ErrorMessageResourceType = typeof(DailyJornal))]
        [Display(Name = "DailyJournal_Date", ResourceType = typeof(DailyJornal))]
        public Nullable<System.DateTime> DailyJournalDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public string Createdby { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public string Modifiedby { get; set; }

        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApprove { get; set; }

        //[Required(ErrorMessageResourceName = "BankCurrencyIDError", ErrorMessageResourceType = typeof(DailyJornal))]

        [Display(Name = "BankCurrency", ResourceType = typeof(DailyJornal))]
        public Nullable<int> BankCurrencyID { get; set; }


        [Display(Name = "Currency_Type", ResourceType = typeof(DailyJornal))]
        public string Currency_Type { get; set; }

        [Display(Name = "JournalTypes", ResourceType = typeof(GeneralResource))]
        public Nullable<int> JournalType { get; set; }

        [Display(Name = "TransationRate", ResourceType = typeof(DailyJornal))]
        public Nullable<decimal> TransationRate { get; set; }

        [Display(Name = "operationDescription", ResourceType = typeof(DailyJornal))]
        public string    Description { get; set; }


        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SchoolID { get; set; }

        public string Datefrom { get; set; }
        public string DateTo { get; set; }

        public string AttachedDocName { get; set; }

        public bool IsPost { get; set; }

        public ICollection<AccountDailyJournalDetailViewModel> _AccountDailyJournalDetail { get; set; }

    }
}
