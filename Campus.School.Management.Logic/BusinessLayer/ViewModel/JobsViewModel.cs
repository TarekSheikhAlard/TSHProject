using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class JobsViewModel
    {
        [Display(Name = "JobID", ResourceType = typeof(Jobs))]
        public int JobID { get; set; }

        public int CompanyId { get; set; }
        public int SchoolId { get; set; }


        [Required(ErrorMessageResourceName = "JobNameEnerror", ErrorMessageResourceType = typeof(Jobs))]
        [Display(Name = "JobNameEn", ResourceType = typeof(Jobs))]
         public string JobNameEn { get; set; }


        [Required(ErrorMessageResourceName = "JobNameArerror", ErrorMessageResourceType = typeof(Jobs))]
        [Display(Name = "JobNameAr", ResourceType = typeof(Jobs))]
        public string JobNameAr { get; set; }



        [Required(ErrorMessageResourceName = "DepartmentIDerror", ErrorMessageResourceType = typeof(Jobs))]
        [Display(Name = "DepartmentID", ResourceType = typeof(Jobs))]
        public int DepartmentID { get; set; }
        [Required]
        [Display(Name = "DepartmentEn", ResourceType = typeof(Jobs))]
        public string DepartmentName { get; set; }

   
    }
}
