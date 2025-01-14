using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class EmployeePayrollSetupViewModel
    {
        public int Employee_ID { get; set; }
        public string BenefitID { get; set; }
        public List<int> _BenefitID { get; set; }
        public string DeductID { get; set; }
        public List<int> _DeductID { get; set; }
        public string TaxesID { get; set; }
        public List<int> _TaxesID { get; set; }
        public string GovID { get; set; }
        public List<int> _GovID { get; set; }
    }
}
