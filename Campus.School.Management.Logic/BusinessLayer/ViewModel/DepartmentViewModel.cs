using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Campus.School.Management.Logic.Resources;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class DepartmentViewModel
    {

        [Display(Name = "DepartmentID", ResourceType = typeof(Department))]
        public int DepartmentID { get; set; }

        
        [Required(ErrorMessageResourceName = "DepartMentEnerror", ErrorMessageResourceType = typeof(Department))]
        [Display(Name = "DepartmentEn", ResourceType = typeof(Department))]
         public string DepartmentEn { get; set; }

        [Required(ErrorMessageResourceName = "DepartmentArerror", ErrorMessageResourceType = typeof(Department))]
        [Display(Name = "DepartmentAr", ResourceType = typeof(Department))]
        public string DepartmentAr { get; set; }

        public int CompanyID { get; set; }
        public int ScholID { get; set; }

    }
}
