using Campus.School.Management.Logic.DataBaseLayer;
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
   public class AccountEmployeeSalaryItemViewModel
    {

      
        [Display(Name = "ID", ResourceType = typeof(AccountEmployeeSalaryItems))]
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "EmployeeError", ErrorMessageResourceType = typeof(AccountEmployeeSalaryItems))]
        [Display(Name = "Employee", ResourceType = typeof(AccountEmployeeSalaryItems))]
        public int Employee_ID { get; set; }

        [Required(ErrorMessageResourceName = "Bouns_DeductionError", ErrorMessageResourceType = typeof(AccountEmployeeSalaryItems))]
        [Display(Name = "Bouns_Deduction", ResourceType = typeof(AccountEmployeeSalaryItems))]
        public int SalaryItemTypeID { get; set; }

        [Required(ErrorMessageResourceName = "TypeError", ErrorMessageResourceType = typeof(AccountEmployeeSalaryItems))]
        [Display(Name = "Type", ResourceType = typeof(AccountEmployeeSalaryItems))]

        public int SalaryItemID { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "AmountError", ErrorMessageResourceType = typeof(AccountEmployeeSalaryItems))]
        [Display(Name = "Amount", ResourceType = typeof(AccountEmployeeSalaryItems))]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "OperationDateError", ErrorMessageResourceType = typeof(AccountEmployeeSalaryItems))]
        [Display(Name = "OperationDate", ResourceType = typeof(AccountEmployeeSalaryItems))]
        public Nullable<System.DateTime> OperationDate { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(AccountEmployeeSalaryItems))]
        public string Notes { get; set; }

        [Display(Name = "BounsType", ResourceType = typeof(Resources.AccountEmployeesSalary))]
        public string BounsType { get; set; }


        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AccountSalaryItem AccountSalaryItem { get; set; }
        [Display(Name = "ItemType", ResourceType = typeof(AccountEmployeeSalaryItems))]

        public virtual string AccountSalaryItemName { get; set; }
        [Display(Name = "BounsAndDeduction", ResourceType = typeof(AccountEmployeeSalaryItems))]
 
        public virtual string AccountSalaryItemTypeName { get; set; }

        public virtual AdmEmployee AdmEmployee { get; set; }
        [Display(Name = "EmployeeName", ResourceType = typeof(AccountEmployeeSalaryItems))]
 
        public virtual string EmployeeName { get; set; }

        public int CompanyID { get; set; }
        public int SchoolID { get; set; }


    }
}
