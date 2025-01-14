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

    public class LedgerTrailBalanceViewModel {
        [Display(Name = "Account Code")]
        public string AccountTreeCode { get; set; }

        [Display(Name = "Account Description")]
        public string AccountNameEn { get; set; }

        
        public int AccountTreeID { get; set; }

        [Display(Name = "Credit Amt")]
        public decimal CreditAmount { get; set; }

        [Display(Name = "Credit Total")]
        public decimal CreditTotal{ get; set; }

        [Display(Name = "Debit Amt")]
        public decimal DebitAmount { get; set; }

        [Display(Name = "Debit Total")]
        public decimal DebitTotal { get; set; }
        public int AccountTreeLevel { get; set; }

    }
   public class TrailbalanceViewModel
    {
        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "Start Account")]
        public int AccountTreeIDStart { get; set; }

        [Display(Name = "Start Account")]
        public string AccountNameStart { get; set; }

        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "End Account")]
        public int AccountTreeIDEnd { get; set; }

        [Display(Name = "Start Account")]
        public string AccountNameEnd { get; set; }

        [Required]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [Display(Name = "To Account")]
        public DateTime ToDate { get; set; }

        [Required]
        [Display(Name = "Level")]
        public int Level { get; set; }


        public string AccountName { get; set; }
        public string AccountNumber { get; set; }

        [Display(Name = "Credit")]
        public decimal BalanceCr { get; set; }
        [Display(Name = "Debit")]
        public decimal BalanceDr { get; set; }
        [Display(Name = "Credit")]
        public decimal AggCr { get; set; }
        [Display(Name = "Debit")]
        public decimal AggDr { get; set; }
        [Display(Name = "Credit")]
        public decimal MovementCr { get; set; }
        [Display(Name = "Debit")]
        public decimal MovementDr { get; set; }
        [Display(Name = "Credit")]
        public decimal FirstBalanceCr { get; set; }
        [Display(Name = "Debit")]
        public decimal FirstBalanceDr { get; set; }





    }
}
