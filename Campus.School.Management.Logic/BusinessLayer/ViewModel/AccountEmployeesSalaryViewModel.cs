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
   public class AccountEmployeesSalaryViewModel
    {
 
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "EmployeeIDError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "EmployeeID", ResourceType = typeof(AccountEmployeesSalary))]

        public Nullable<int> Employee_ID { get; set; }

        [Display(Name = "Employee_Name", ResourceType = typeof(AccountEmployeesSalary))]

        public string EmployeeName { get; set; }

        [Display(Name = "Job_Name", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<int> JobID { get; set; }

        [Display(Name = "Job_Name", ResourceType = typeof(AccountEmployeesSalary))]
        public string JobName { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Basic_SalaryError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "Basic_Salary", ResourceType = typeof(AccountEmployeesSalary))] 
        public Nullable<decimal> BasicSalary { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Additional_SalaryError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "Additional_Salary", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> AdditionalSalary { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Allowance_SalaryError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "Allowance_Salary", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> AllowanceSalary { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Bonuses_SalaryError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "Bonuses_Salary", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> BonusesSalary { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "extra_SalaryError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "extra_Salary", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> extraSalary { get; set; }


        [Display(Name = "Total_Salary", ResourceType = typeof(AccountEmployeesSalary))] 
        public Nullable<decimal> TotalSalary { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Social_insuranceError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "Social_insurance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Socialinsurance { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Medical_insuranceError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "Medical_insurance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Medicalinsurance { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "TaxError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "Tax", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Taxes { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "DedecationError", ErrorMessageResourceType = typeof(AccountEmployeesSalary))]
        [Display(Name = "Dedecation", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Dedecation { get; set; }

        [Display(Name = "Total_Dedecated", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> TotalDedecated { get; set; }

        [Display(Name = "Net_Salary", ResourceType = typeof(AccountEmployeesSalary))]
         public Nullable<decimal> NetSalary { get; set; }



        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "AllowanceSalary2", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> AllowanceSalary2 { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "AllowanceSalary3", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> AllowanceSalary3 { get; set; }

        [Display(Name = "workingHours", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<int> workingHours { get; set; }

        [Display(Name = "BankBranchID", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<int> BankBranchID { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(AccountEmployeesSalary))]
        public string AccountNumber { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "Transitionallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Transitionallowance { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "Subsistenceallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Subsistenceallowance { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "Drivingallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Drivingallowance { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "conditionsworkallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> conditionsworkallowance { get; set; }

        [Display(Name = "Airline", ResourceType = typeof(AccountEmployeesSalary))]
        public string Airline { get; set; }

        [Display(Name = "Airlineclass", ResourceType = typeof(AccountEmployeesSalary))]
        public string Airlineclass { get; set; }

        [Display(Name = "accommodationallowancetype", ResourceType = typeof(AccountEmployeesSalary))]
        public string accommodationallowancetype { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "accommodationallowanc", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> accommodationallowanc { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "valuedate", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<System.DateTime> valuedate { get; set; }




        [Display(Name = "leavebalance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<int> leavebalance { get; set; }

        [Display(Name = "LBperMonth", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<int> LBperMonth { get; set; }

        [Display(Name = "flighttickets", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<int> flighttickets { get; set; }

        [Display(Name = "FTperMonth", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<int> FTperMonth { get; set; }

        [Display(Name = "Dept_ID", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<int> Dept_ID { get; set; }

        [Display(Name = "Dept_ID", ResourceType = typeof(AccountEmployeesSalary))]
        public string Dept_Name{ get; set; }

        public string BankName { get; set; }

        public int CompanyID { get; set; }
        public int SchoolID { get; set; }


    }
}
