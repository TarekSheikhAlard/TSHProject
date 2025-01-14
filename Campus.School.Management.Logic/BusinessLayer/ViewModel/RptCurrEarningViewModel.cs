using System;
using Campus.School.Management.Logic.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class RptCurrEarningFullViewModel {

        public List<RptCurrEarningViewModel> Earnings = new List<RptCurrEarningViewModel>();

        public List<RptCurrEarningViewModel> Earningsdetails = new List<RptCurrEarningViewModel>();
    }


    public class RptCurrEarningViewModel
    {

        [Display(Name = "Employee Id")]
        public Int64 Id { get; set; }


        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }


        [Display(Name = "Name")]
        public string NameE { get; set; }

        [Display(Name = "Month No.")]
        public int SalaryMonthId { get; set; }


        [Display(Name = "Total Salary")]
        public decimal TotalSalary { get; set; }

        [Display(Name = "Allowance Type")]
        public string SalaryType { get; set; }


        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

      


    }

}
