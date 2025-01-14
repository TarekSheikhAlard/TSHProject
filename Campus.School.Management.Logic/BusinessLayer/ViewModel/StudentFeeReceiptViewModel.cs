using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class StudentReceiptViewModel {
        public string NameEn { get; set; }
        public string ArabicName { get; set; }
        public string GradeName { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }
        public string datePayment { get; set; }
        public string email { get; set; }
        public string pemail { get; set; }


        public long? ReceiptID { get; set; }
        public List<StudentFeesReceiptViewModel> studentFeesReceipts { get; set; }
    }
    public class StudentFeesReceiptViewModel
    {
        public string InvoiceCode { get; set; }
        public string Description { get; set; }
        public decimal originalAmount { get; set; }
        public int? Discount { get; set; }
        public int? Tax { get; set; }
        public decimal Amount { get; set; }
        public string feeType { get; set; }
        public long? ReceiptID { get; set; }
    }

    public class automateFeeDetails
    {
        public int AccountTreeID { get; set; }
        public string AccountName  {get;set;}
        public decimal Amount { get; set; }
        public int AutomaticCount { get; set; }
        public bool isPaid { get; set; }
        public bool isSelected { get; set; }

    }

}
