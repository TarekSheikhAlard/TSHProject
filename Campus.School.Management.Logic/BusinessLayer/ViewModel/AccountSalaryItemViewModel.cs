using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.School.Management.Logic.Resources;
namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public  class AccountSalaryItemViewModel
    {
        [DisplayName("ID")]

        public int ID { get; set; }


        [Required(ErrorMessageResourceName = "Deduction_BonusError", ErrorMessageResourceType = typeof(BonusandDedecation))]
        [Display(Name = "Deduction_Bonus", ResourceType = typeof(BonusandDedecation))]
        public Nullable<int> Salary_itemTypeID { get; set; }

        [Display(Name = "Deduction_Bonus", ResourceType = typeof(BonusandDedecation))]
        public string Salary_itemTypeName { get; set; }

        [Required(ErrorMessageResourceName = "Deduction_Bonus_typesError", ErrorMessageResourceType = typeof(BonusandDedecation))]
        [Display(Name = "Deduction_Bonus_types", ResourceType = typeof(BonusandDedecation))]
 
        public string Salary_itemNameEn { get; set; }
        public string Salary_itemNameAr { get; set; }
        public int CompanyID { get; set; }
        public int SchoolID { get; set; }



    }

}
