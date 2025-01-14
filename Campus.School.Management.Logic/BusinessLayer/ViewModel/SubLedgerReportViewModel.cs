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
    public class SubLedgerReportViewModel
    {
        public DateTime CreatedDate { get; set; }
        public string JournalType { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }
        
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        public decimal? Balance { get; set; }
        public int DailyJournalID { get; set; }

        public int AccountTreeID { get; set; }
        public string Account { get; set; }



    }
}
