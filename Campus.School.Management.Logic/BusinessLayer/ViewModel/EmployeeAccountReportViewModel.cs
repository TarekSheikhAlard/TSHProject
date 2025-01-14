using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class EmployeeAccountReportViewModel
    {
        public DateTime date { get; set; }
        public string serialNo { get; set; }
        public string description { get; set; }
        public string docNo { get; set; }
        public string debit { get; set; }
        public string credit { get; set; }
        public string total { get; set; }
    }

    public class EmployeeAccountReportInputs
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string  fromAccount { get; set; }
        public string toAccount { get; set; }
        public string costCenter { get; set; }
        public int entryType { get; set; }
        public enum entryTypes { unpostedEntry =0, postedEntry =1 ,allEntry =2}
    }




}
