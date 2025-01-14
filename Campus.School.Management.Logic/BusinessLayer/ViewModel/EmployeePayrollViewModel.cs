using System;
using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class EmployeePayrollViewModel
    {

        [Display(Name = "ID")]
        public int Employee_ID { get; set; }

        [Display(Name = "Name")]
        public string NameE { get; set; }

        public int BasePay { get; set; }

        [Display(Name = "Regular Hrs")]
        public int RegularHours { get; set; }
        [Display(Name = "OT")]
        public int OTHours { get; set; }
        [Display(Name = "Bonus")]
        public int Bonus { get; set; }
        [Display(Name = "Absence Hrs")]
        public int DeductionHours { get; set; }
        public int SalaryMonth { get; set; }
        public string PayrollYear { get; set; }
        public Int64? Payroll_Id { get; set; }

        public string Benfits { get; set; }
        public string Deductions { get; set; }
        public string Taxes { get; set; }
        public string GovReq { get; set; }

    }
}
