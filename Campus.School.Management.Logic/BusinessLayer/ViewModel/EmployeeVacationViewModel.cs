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
    public class EmployeeVacationViewModel
    {

        public int ID { get; set; }
        public int Employee_ID { get; set; }
        [Required]
        [Display(Name = "VacationType", ResourceType = typeof(EmployeeVacation))]
        public int HolidayTypeId { get; set; }
        public string HolidayNameEn { get; set; }
        public string HolidayNameAr { get; set; }


        [Required]
        [Display(Name = "DateOfVacation", ResourceType = typeof(EmployeeVacation))]
        public System.DateTime DateOfVacation { get; set; }
        [Required]
        [Display(Name = "LastWorkingDay", ResourceType = typeof(EmployeeVacation))]
        public System.DateTime LastWorkingDate { get; set; }
        public int EmployeeSalaryId { get; set; }
        [Required]
        public string ContactOnVacation { get; set; }
        [Required]
        public Nullable<int> EmployeeInCharge { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "BasicSalaryError", ErrorMessageResourceType = typeof(AccountEmployeMonthlySalary))]
        [Display(Name = "BasicSalary", ResourceType = typeof(AccountEmployeMonthlySalary))]
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

        [Display(Name = "TotalSalary", ResourceType = typeof(AccountEmployeMonthlySalary))]
        public Nullable<decimal> TotalSalary { get; set; }


        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "DedecationError", ErrorMessageResourceType = typeof(AccountEmployeMonthlySalary))]
        [Display(Name = "Dedecation", ResourceType = typeof(AccountEmployeMonthlySalary))]
        public Nullable<decimal> Dedecation { get; set; }

        [Display(Name = "totalDedecation", ResourceType = typeof(AccountEmployeMonthlySalary))]
        public Nullable<decimal> TotalDedecated { get; set; }

        [Display(Name = "NetSalary", ResourceType = typeof(AccountEmployeMonthlySalary))]
        public Nullable<decimal> NetSalary { get; set; }


        [Display(Name = "AllowanceSalary2", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> AllowanceSalary2 { get; set; }

        [Display(Name = "AllowanceSalary3", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> AllowanceSalary3 { get; set; }



        [Display(Name = "Transitionallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Transitionallowance { get; set; }


        [Display(Name = "Subsistenceallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Subsistenceallowance { get; set; }


        [Display(Name = "Drivingallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> Drivingallowance { get; set; }


        [Display(Name = "conditionsworkallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> conditionsworkallowance { get; set; }

        [Display(Name = "accommodationallowanc", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> accommodationallowanc { get; set; }

        [Display(Name = "sumallowance", ResourceType = typeof(AccountEmployeesSalary))]
        public Nullable<decimal> sumallowance { get; set; }
        [Display(Name = "paymentsalariesway", ResourceType = typeof(AccountEmployeesSalary))]

        public string paymentsalariesway { get; set; }



        [Required(ErrorMessageResourceName = "costcenterError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]
        public Nullable<int> costcenterID { get; set; }

        [Display(Name = "costcenter", ResourceType = typeof(chequesPayable))]
        public string costcenterName { get; set; }



        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "fromAccount", ResourceType = typeof(chequesPayable))]
        public int fromAccount { get; set; }

        [Display(Name = "fromAccount", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN1 { get; set; }



        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "toAccount", ResourceType = typeof(chequesPayable))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "toAccount", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN { get; set; }




        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "fromAccount", ResourceType = typeof(chequesPayable))]
        public int fromAccount2 { get; set; }

        [Display(Name = "fromAccount", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN11 { get; set; }



        [Required(ErrorMessageResourceName = "AccountError", ErrorMessageResourceType = typeof(chequesPayable))]
        [Display(Name = "toAccount", ResourceType = typeof(chequesPayable))]
        public Nullable<int> AccountTreeID2 { get; set; }

        [Display(Name = "toAccount", ResourceType = typeof(chequesPayable))]
        public string AccountNameEN2 { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "DateError", ErrorMessageResourceType = typeof(AccountEmployeMonthlySalary))]
        [Display(Name = "Date", ResourceType = typeof(AccountEmployeMonthlySalary))]
        public Nullable<System.DateTime> OperationDate { get; set; }




    }
}
