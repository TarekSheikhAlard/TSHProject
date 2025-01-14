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
   public class AccountDailyJournalDetailItemViewModel
    {
        //[Required(ErrorMessageResourceName = "AccountNameArError", ErrorMessageResourceType = typeof(DailyJournalDetailsItems))]
        //[Display(Name = "AccountNameAr", ResourceType = typeof(DailyJournalDetailsItems))]



        //[Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(DailyJournalDetailsItems))]
        [Display(Name = "Account", ResourceType = typeof(DailyJournalDetailsItems))]
        public int AccountTreeID { get; set; }


        [Display(Name = "DifferentDebit", ResourceType = typeof(DailyJournalDetailsItems))]
        public decimal DifferentDebit { get; set; }

        [Display(Name = "DifferentCredit", ResourceType = typeof(DailyJournalDetailsItems))]
        public decimal DifferentCredit { get; set; }

        [Display(Name = "TotalDebit", ResourceType = typeof(DailyJournalDetailsItems))]
        public decimal TotalDebit { get; set; }

        [Display(Name = "TotalCredit", ResourceType = typeof(DailyJournalDetailsItems))]
        public decimal TotalCredit { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "AccountName", ResourceType = typeof(DailyJournalDetailsItems))]
        public virtual string AccountNameEN { get; set; }
        public Nullable<System.DateTime> DailyJournalDate { get; set; }
    }
}
