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
     public class AccountCostCenterViewModel
    {
 
        [Display(Name = "ID", ResourceType = typeof(CostCenter))]
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "CostCenterError", ErrorMessageResourceType = typeof(CostCenter))]
        [Display(Name = "CostCenterName", ResourceType = typeof(CostCenter))]
        public string CostCenterEn { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(CostCenter))]
        public string Comment { get; set; }
        [Required(ErrorMessageResourceName = "CostCenterARError", ErrorMessageResourceType = typeof(CostCenter))]

        [Display(Name = "CostCenterAR", ResourceType = typeof(CostCenter))]
        public string CostCenterAR { get; set; }

        [Required(ErrorMessageResourceName = "CodeError", ErrorMessageResourceType = typeof(CostCenter))]

        [Display(Name = "Code", ResourceType = typeof(CostCenter))]

        public string Code { get; set; }

        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SchoolID { get; set; }

    }
}
