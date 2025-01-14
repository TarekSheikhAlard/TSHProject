using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class IncomeStatementViewModel
    {
       
        public int AccountTreeLevel { get; set; }
        
            
        
        public string MonthlyPeriod { get; set; }


        [Display(Name = "StartDate")]
        public string From { get; set; }

        [Display(Name = "EndDate")]
        public string To { get; set; }

        //[Display(Name = "Month")]
        //public DateTime Month { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName {get;set;}

        [Display(Name = "Amount (SAR)")]
        public decimal Amount { get; set; }

        public int AccountTreeID { get; set; }
       

        [Display(Name = "Main Account Total")]
        public decimal MainAccountTotal { get; set; }

        [Display(Name = "Branch Account Total")]
        public decimal BranchAccountTotal { get; set; }
    

        [Display(Name = "Total Expenses Total")]
        public decimal TotalExpenses { get; set; }
        [Display(Name = "Total Revenues Total")]
        public decimal TotalRevenues { get; set; }

    }
}
