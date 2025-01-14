using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;
using System.ComponentModel.DataAnnotations;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
 public   class SupplierViewModel
    {
          
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "SupplierNameError", ErrorMessageResourceType = typeof(Supplier_res))]
        [Display(Name = "SupplierName", ResourceType = typeof(Supplier_res))]
        public string SupplierName { get; set; }


        [Required(ErrorMessageResourceName = "SupplierNameArError", ErrorMessageResourceType = typeof(Supplier_res))]
        [Display(Name = "SupplierNameAr", ResourceType = typeof(Supplier_res))]
        public string SupplierNameAr { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Supplier_res))]
        public string Address { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "Phone", ResourceType = typeof(Supplier_res))]
        public string Phone { get; set; }

        [RegularExpression("^\\d+", ErrorMessageResourceName = "Reg_ExpDigit", ErrorMessageResourceType = typeof(GeneralResource))]

        [Display(Name = "Mobile", ResourceType = typeof(Supplier_res))]
        public string Mobile { get; set; }

        [EmailAddress(ErrorMessageResourceName = "E_mailerror", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Email", ResourceType = typeof(Supplier_res))]
        public string Email { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Supplier_res))]
        public string Website { get; set; }

      //  [Required(ErrorMessageResourceName = "AccountTreeIDError", ErrorMessageResourceType = typeof(Supplier_res))]
        [Display(Name = "AccountTreeID", ResourceType = typeof(Supplier_res))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "AccountTreeID", ResourceType = typeof(Supplier_res))]
        public string AccountTree { get; set; }


        [Display(Name = "SupplierNumber", ResourceType = typeof(Supplier_res))]

        public string SupplierNumber { get; set; }


        [Display(Name = "Fax", ResourceType = typeof(Supplier_res))]
        public string Fax { get; set; }


        [Display(Name = "PostCode", ResourceType = typeof(Supplier_res))]
        public string PostCode { get; set; }

        [Display(Name = "Typeofactivity", ResourceType = typeof(Supplier_res))]
        public string Typeofactivity { get; set; }

        [Display(Name = "Commercialregister", ResourceType = typeof(Supplier_res))]
        public string Commercialregister { get; set; }

        [Display(Name = "CityID", ResourceType = typeof(Supplier_res))]
        public Nullable<int> CityID { get; set; }
        [Display(Name = "CityID", ResourceType = typeof(Supplier_res))]

        public string CityName { get; set; }

        [Display(Name = "BankBranchID", ResourceType = typeof(Supplier_res))]
        public Nullable<int> BankBranchID { get; set; }
        [Display(Name = "BankBranchID", ResourceType = typeof(Supplier_res))]

        public string BankBranchName { get; set; }


        [Display(Name = "BankAccountNumber", ResourceType = typeof(Supplier_res))]
        public string BankAccountNumber { get; set; }

        [Display(Name = "IBAN1", ResourceType = typeof(Supplier_res))]
        public string IBAN1 { get; set; }

        [Display(Name = "IBAN2", ResourceType = typeof(Supplier_res))]
        public string IBAN2 { get; set; }

        [Display(Name = "IBAN3", ResourceType = typeof(Supplier_res))]
        public string IBAN3 { get; set; }

        [Display(Name = "Supplieractivity", ResourceType = typeof(Supplier_res))]
        public string Supplieractivity { get; set; }

        [Display(Name = "ReasonStop", ResourceType = typeof(Supplier_res))]
        public string ReasonStop { get; set; }

        [Display(Name = "Emp_ID", ResourceType = typeof(Supplier_res))]
        public Nullable<int> Emp_ID { get; set; }

        [Display(Name = "Emp_ID", ResourceType = typeof(Supplier_res))]

        public string EmployeeName { get; set; }

        [Display(Name = "Supplier_Employee", ResourceType = typeof(Supplier_res))]
        public string Supplier_Employee { get; set; }

        [Display(Name = "Depeit", ResourceType = typeof(Supplier_res))]
        public Nullable<decimal> Depeit { get; set; }

        [Display(Name = "Credit", ResourceType = typeof(Supplier_res))]
        public Nullable<decimal> Credit { get; set; }

        [Display(Name = "FBalance", ResourceType = typeof(Supplier_res))]
        public Nullable<decimal> FBalance { get; set; }

        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> CompanyID { get; set; }



    }
}
