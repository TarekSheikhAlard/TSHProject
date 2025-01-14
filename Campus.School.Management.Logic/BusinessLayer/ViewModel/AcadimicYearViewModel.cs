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
    public class AcademicYearViewModel
    {
        public int AcademicID { get; set; }


        [Required(ErrorMessageResourceName = "academicYearError", ErrorMessageResourceType = typeof(AcademicYear))]
        [Display(Name = "academicYear", ResourceType = typeof(AcademicYear))]
        public string AcadmicName { get; set; }


        [Required(ErrorMessageResourceName = "StageError", ErrorMessageResourceType = typeof(AcademicYear))]
        [Display(Name = "Stage", ResourceType = typeof(AcademicYear))]
        public int Stage_ID { get; set; }

        [Display(Name = "Stage", ResourceType = typeof(AcademicYear))]
        public string StageName { get; set; }
        
        public bool IsNew { get; set; }
    }
}
