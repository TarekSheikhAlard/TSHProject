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
   public class EmployeePayrollHistoryViewModel
    {
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }


        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Month")]
        public string MonthName{ get; set; }

        [Display(Name = "Description")]
        public string BenefitName { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

    }
}
