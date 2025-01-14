using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{

    public class AccountCustomerViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "CustomerNameError", ErrorMessageResourceType = typeof(AccountCustomer))]
        [Display(Name = "CustomerName", ResourceType = typeof(AccountCustomer))]
        public string CustomerName { get; set; }


        [Required(ErrorMessageResourceName = "CustomerNameArError", ErrorMessageResourceType = typeof(AccountCustomer))]
        [Display(Name = "CustomerNameAr", ResourceType = typeof(AccountCustomer))]
        public string CustomerNameAr { get; set; }

        [Display(Name = "Address", ResourceType = typeof(AccountCustomer))]
        public string Address { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "Phone", ResourceType = typeof(AccountCustomer))]
        public string Phone { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "Mobile", ResourceType = typeof(AccountCustomer))]
        public string Mobile { get; set; }

        [EmailAddress(ErrorMessageResourceName = "E_mailerror", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Email", ResourceType = typeof(AccountCustomer))]
        public string Email { get; set; }

        [Display(Name = "Website", ResourceType = typeof(AccountCustomer))]
        public string Website { get; set; }

        
        [Display(Name = "AccountTreeID", ResourceType = typeof(AccountCustomer))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "AccountTree", ResourceType = typeof(AccountCustomer))]
        public string AccountTree { get; set;}


        //[Required(ErrorMessageResourceName = "DepeitError", ErrorMessageResourceType = typeof(AccountCustomer))]
        [Display(Name = "Depeit", ResourceType = typeof(AccountCustomer))]
        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        public Nullable<decimal> Depeit { get; set; }

       // [Required(ErrorMessageResourceName = "CreditError", ErrorMessageResourceType = typeof(AccountCustomer))]
        [Display(Name = "Credit", ResourceType = typeof(AccountCustomer))]
        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        public Nullable<decimal> Credit { get; set; }

        [Display(Name = "FBalance", ResourceType = typeof(AccountCustomer))]
        public Nullable<decimal> FBalance { get; set; }



        public Nullable<int> SchoolID { get; set; }

        public Nullable<int> CompanyID { get; set; }


        [Display(Name = "CustomerNumber", ResourceType = typeof(AccountCustomer))]

        public string CustomerNumber { get; set; }

        [Display(Name = "Fax", ResourceType = typeof(AccountCustomer))]

        public string Fax { get; set; }

        [Display(Name = "PostCode", ResourceType = typeof(AccountCustomer))]

        public string PostCode { get; set; }

        [Display(Name = "Typeofactivity", ResourceType = typeof(AccountCustomer))]

        public string Typeofactivity { get; set; }

        [Display(Name = "Commercialregister", ResourceType = typeof(AccountCustomer))]

        public string Commercialregister { get; set; }

        [Display(Name = "BankAccountNumber", ResourceType = typeof(AccountCustomer))]

        public string BankAccountNumber { get; set; }

        [Display(Name = "IBAN1", ResourceType = typeof(AccountCustomer))]

        public string IBAN1 { get; set; }

        [Display(Name = "IBAN2", ResourceType = typeof(AccountCustomer))]

        public string IBAN2 { get; set; }

        [Display(Name = "IBAN3", ResourceType = typeof(AccountCustomer))]

        public string IBAN3 { get; set; }

        [Display(Name = "Customeractivity", ResourceType = typeof(AccountCustomer))]

        public string Customeractivity { get; set; }

        [Display(Name = "ReasonStop", ResourceType = typeof(AccountCustomer))]

        public string ReasonStop { get; set; }

        [Display(Name = "Customer_Employee", ResourceType = typeof(AccountCustomer))]

        public string Customer_Employee { get; set; }

        [Display(Name = "Emp_ID", ResourceType = typeof(AccountCustomer))]

        public Nullable<int> Emp_ID { get; set; }

        [Display(Name = "Emp_ID", ResourceType = typeof(AccountCustomer))]

        public string EmployeeName{ get; set; }

        [Display(Name = "CityID", ResourceType = typeof(AccountCustomer))]

        public Nullable<int> CityID { get; set; }

        [Display(Name = "CityID", ResourceType = typeof(AccountCustomer))]

        public string CityName { get; set; }

        [Display(Name = "BankBranchID", ResourceType = typeof(AccountCustomer))]

        public Nullable<int> BankBranchID { get; set; }

        [Display(Name = "BankBranchID", ResourceType = typeof(AccountCustomer))]

        public string BankBranchName { get; set; }



    }
}
