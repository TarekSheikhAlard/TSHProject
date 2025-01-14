using System;
using Campus.School.Management.Logic.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class BalanceSheetViewModel
    {
        public int AccountTreeLevel { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Amount (SAR)")]
        public decimal Amount { get; set; }
        public int AccountTreeID { get; set; }



        [Display(Name = "StartDate")]
        public string From { get; set; }

        [Display(Name = "EndDate")]
        public string To { get; set; }
    }
}
