using Campus.School.Management.Logic.DataBaseLayer;
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
   
    public class StudentTransactionsViewModel
    {
        [Display(Name = "SerialNo", ResourceType = typeof(StudentTransactions))]
        public string SerialNo { get; set; }

        [Display(Name = "Date", ResourceType = typeof(StudentTransactions))]
        public DateTime Date { get; set; }

        [Display(Name = "Description", ResourceType = typeof(StudentTransactions))]
        public string Description { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(StudentTransactions))]
        public decimal Amount { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(StudentTransactions))]
        public int Discount { get; set; }

        [Display(Name = "Tax", ResourceType = typeof(StudentTransactions))]
        public int Tax { get; set; }

        [Display(Name = "TotalAmount", ResourceType = typeof(StudentTransactions))]
        public decimal TotalAmount { get; set; }

        [Display(Name = "PaidAmount", ResourceType = typeof(StudentTransactions))]
        public decimal PaidAmount { get; set; }

        public long? ReceiptCode { get; set; }
        public int? DailyJournalID { get; set; }
        public int? CashRecivedID { get; set; }
        public int? ChequeRecivedID { get; set; }

        public int ID { get; set; }
        public string  createdBy { get; set; }
        public long? StudRefID { get; set; }

    }
}
