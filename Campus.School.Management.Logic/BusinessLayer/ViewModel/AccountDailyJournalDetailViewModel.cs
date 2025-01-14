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
   public class AccountDailyJournalDetailViewModel
    {
        [Display(Name = "DailyJournalDetail_ID", ResourceType = typeof(DailyJornal))]
        
        public int DailyJournalDetailID { get; set; }

        [Display(Name = "DailyJournal_ID", ResourceType = typeof(DailyJornal))]
        public int DailyJournalID { get; set; }

        [Display(Name = "Account", ResourceType = typeof(DailyJornal))]
        public int AccountTreeID { get; set; }

       
        [Display(Name = "Debit", ResourceType = typeof(DailyJornal))]
        public decimal Debit { get; set; }

        [Display(Name = "Credit", ResourceType = typeof(DailyJornal))]
        public decimal Credit { get; set; }

        [Display(Name = "Description", ResourceType = typeof(DailyJornal))]
        public string Description { get; set; }

        public Nullable<int> CostCenterID { get; set; }

        public string CostCenter { get; set; }

        [Display(Name = "AccountCode", ResourceType = typeof(AccountTrees))]
        public string AccountTreeCode { get; set; }

        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AccountDailyJournal AccountDailyJournal { get; set; }
        public virtual AccountStatementType AccountStatementType { get; set; }



        [Display(Name = "AccountName", ResourceType = typeof(DailyJornal))]
        public virtual string AccountNameEN { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DailyJournal_Date", ResourceType = typeof(DailyJornal))]
        public Nullable<System.DateTime> DailyJournalDate { get; set; }
    }
}
