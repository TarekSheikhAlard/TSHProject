using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class AccountProfViewModel
    {
        public int AccountID { get; set; }

        [Display(Name = "Account", ResourceType = typeof(DailyJornal))]
        public string AccountName { get; set; }

        [Display(Name = "Debit", ResourceType = typeof(DailyJornal))]
        public decimal Debit { get; set; }

        [Display(Name = "Credit", ResourceType = typeof(DailyJornal))]
        public decimal Credit { get; set; }
        [Display(Name = "Balance", ResourceType = typeof(DailyJornal))]
        public decimal Balance { get; set; }
      

    }
}
