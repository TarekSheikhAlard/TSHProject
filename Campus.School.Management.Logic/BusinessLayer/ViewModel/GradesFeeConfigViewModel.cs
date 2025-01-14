using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class GradesFeeConfigViewModel
    {
        public long? Id { get; set; }
        public int GradeID { get; set; }
        public int AccountTreeID { get; set; }
        public string AccountTreeName { get; set; }
        public decimal Amount { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public int Priority { get; set; }
        public int InstallmentType { get; set; }
        public string InstallmentName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public int SchoolID { get; set; }
        public List<GradesFeeConfigViewModel> configList { get; set; }
    }

    public class FeeReportViewModel {

        public string Feetype { get; set; }
        public string Grade { get; set; }
        public decimal Amount { get; set; }
    }
}
