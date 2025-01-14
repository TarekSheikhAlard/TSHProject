using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public  class ClassRoomViewModel
    {
        public int ClassID { get; set; }

        [Required(ErrorMessageResourceName = "ClassCodeerror", ErrorMessageResourceType = typeof(ClassRoom))]
        [Display(Name = "ClassCode", ResourceType = typeof(ClassRoom))]
        public string ClassCode { get; set; }


        [Required(ErrorMessageResourceName = "ClassNameerror", ErrorMessageResourceType = typeof(ClassRoom))]
        [Display(Name = "ClassName", ResourceType = typeof(ClassRoom))]
        public string ClassName { get; set; }


        [Required(ErrorMessageResourceName = "ClassNameArerror", ErrorMessageResourceType = typeof(ClassRoom))]
        [Display(Name = "ClassNameAr", ResourceType = typeof(ClassRoom))]
        public string ClassNameAr { get; set; }


        [Required(ErrorMessageResourceName = "StudentCounterror", ErrorMessageResourceType = typeof(ClassRoom))]
        [Display(Name = "StudentCount", ResourceType = typeof(ClassRoom))]
        public Nullable<int> StudentCount { get; set; }


        [Required(ErrorMessageResourceName = "ClassLeaderIDerror", ErrorMessageResourceType = typeof(ClassRoom))]
        [Display(Name = "ClassLeaderID", ResourceType = typeof(ClassRoom))]
        public Nullable<int> ClassLeaderID { get; set; }


        [Required(ErrorMessageResourceName = "AcademicYearIDerror", ErrorMessageResourceType = typeof(ClassRoom))]
        [Display(Name = "AcademicYearName", ResourceType = typeof(ClassRoom))]
        public Nullable<int> AcademicYearID { get; set; }

        [Display(Name = "AcademicYearName", ResourceType = typeof(ClassRoom))]
        public string AcademicYearName{ get; set; }

        [Display(Name = "ClassLeaderName", ResourceType = typeof(ClassRoom))]
        public string ClassLeaderName { get; set; }
    }
}
