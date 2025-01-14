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
   public class AccountTreasuryViewModel
    {

      
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "Treasury_NameError", ErrorMessageResourceType = typeof(Treasury))]
        [Display(Name = "Treasury_Name", ResourceType = typeof(Treasury))]
        public string TreasuryNameEn { get; set; }


        [Display(Name = "Notes", ResourceType = typeof(Treasury))]
        public string Comment { get; set; }

        [Required(ErrorMessageResourceName = "TreasuryNameARError", ErrorMessageResourceType = typeof(Treasury))]
        [Display(Name = "TreasuryNameAR", ResourceType = typeof(Treasury))]
        public string TreasuryNameAR { get; set; }

     


        [Required(ErrorMessageResourceName = "AccountTreeIDError", ErrorMessageResourceType = typeof(Treasury))]
        [Display(Name = "AccountTreeID", ResourceType = typeof(Treasury))]
        public Nullable<int> AccountTreeID { get; set; }

        [Display(Name = "AccountNameAR", ResourceType = typeof(Treasury))]
        public string AccountNameAR { get; set; }


        [Display(Name = "Emp_Name", ResourceType = typeof(Treasury))]
        public Nullable<int> Employee_ID { get; set; }

        [Display(Name = "Emp_Name", ResourceType = typeof(Treasury))]
        public string NameA { get; set; }

        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> CompanyID { get; set; }


    }
}
