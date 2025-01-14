using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public  class AccountsubsidiaryledgerViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DailyJournal_Date", ResourceType = typeof(DailyJornal))]
        public Nullable<System.DateTime> DailyJournalDate { get; set; }

        [Display(Name = "Serial_No", ResourceType = typeof(DailyJornal))]
        public string SerialNo { get; set; }

        [Display(Name = "Document_Number", ResourceType = typeof(DailyJornal))]
        public string DocumentNumber { get; set; }


        [Display(Name = "operationDescription", ResourceType = typeof(DailyJornal))]
        public string Description { get; set; }

        [Display(Name = "BankCurrency", ResourceType = typeof(DailyJornal))]
        public Nullable<int> BankCurrencyID { get; set; }
        [Display(Name = "Currency_Type", ResourceType = typeof(DailyJornal))]
        public string Currency_Type { get; set; }

        [Display(Name = "originalamount", ResourceType = typeof(DailyJornal))]
        public decimal originalamount { get; set; }

        [Display(Name = "Debit", ResourceType = typeof(DailyJornal))]
        public decimal Debit { get; set; }

        [Display(Name = "Credit", ResourceType = typeof(DailyJornal))]
        public decimal Credit { get; set; }

        [Display(Name = "Balance", ResourceType = typeof(DailyJornal))]
        public decimal Balance { get; set; }
    }
}
