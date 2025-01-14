using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class BankTransactionsViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Payee { get; set; }
        public decimal? tax { get; set; }
        public int TransactionType { get; set; }
        public int PayeeAccountId { get; set; }
        public int BankAccountId { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public bool IsReviewed { get; set; } 
        public bool IsDeleted { get; set; }
        public int? DailyJournalID { get; set; }
        public int? CashRecivedID { get; set; }
        public int? ChequeRecivedID { get; set; }
        public int SchoolID { get; set; }
        public string TransactionName { get; set; }
        public string PayeeAccountName { get; set; }
        public string BankAccountName { get; set; }

        public string AttachedDocName { get; set; }
    }
}
