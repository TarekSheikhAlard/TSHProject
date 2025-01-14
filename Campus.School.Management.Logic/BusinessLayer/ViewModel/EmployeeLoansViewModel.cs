using Campus.School.Management.Logic.DataBaseLayer;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class EmployeeLoansViewModel
    {
        public int ID { get; set; }

        [Required]
        public int Employee_ID { get; set; }

        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "TotalAmount", ResourceType = typeof(EmployeeLoans))]
        public decimal TotalAmount { get; set; }
        [Required]
        [Display(Name = "MonthlyDeductionAmt", ResourceType = typeof(EmployeeLoans))]
        public decimal MonthlyDeductionAmt { get; set; }
        [Required]
        [Display(Name = "StartOfDeduction", ResourceType = typeof(EmployeeLoans))]
        public System.DateTime StartOfDeduction { get; set; }
        [Display(Name = "Balance", ResourceType = typeof(EmployeeLoans))]
        public decimal Balance { get; set; }

        //public System.DateTime CreatedDate { get; set; }
        //public int CreatedbyID { get; set; }
        //public Nullable<System.DateTime> ModifiedDate { get; set; }
        //public Nullable<int> ModifiedbyID { get; set; }
        //public Nullable<System.DateTime> DeletedDate { get; set; }
        //public Nullable<int> DeletedbyID { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
