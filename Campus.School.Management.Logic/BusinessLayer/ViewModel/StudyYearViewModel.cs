using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;

using System.Security.AccessControl;
using Microsoft.Office.Interop.Excel;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
     public class StudyYearViewModel
    {

        
        public string YearID { get; set; }

        [Display(Name = "IsDefault", ResourceType = typeof(StudyYear))]
        public bool IsDefault { get; set; }

        public int CompanyID { get; set; }
        public int SchoolID { get; set; }



        [Required(ErrorMessageResourceName = "EnglishYearNameError", ErrorMessageResourceType = typeof(StudyYear))]
        [Display(Name ="EnglishYearName", ResourceType = typeof(StudyYear))]
        public string YearNameEn { get; set; }

        [Required(ErrorMessageResourceName = "ArabicYearNameError", ErrorMessageResourceType = typeof(StudyYear))]
        [Display(Name ="ArabicYearName", ResourceType = typeof(StudyYear))]
        public string YearNameAr { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "YearStartError", ErrorMessageResourceType = typeof(StudyYear))]
        [Display(Name = "YearStart", ResourceType = typeof(StudyYear))]
 
        public Nullable<System.DateTime> YearStDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "YearEndError", ErrorMessageResourceType = typeof(StudyYear))]
   
        [Display(Name ="YearEnd", ResourceType = typeof(StudyYear))]
        public Nullable<System.DateTime> YearEdDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "YearhijristartError", ErrorMessageResourceType = typeof(StudyYear))]
        [Display(Name = "YearHijriStart", ResourceType = typeof(StudyYear))]
   
        public Nullable<System.DateTime> YearStDateHijri { get; set; }

        [Required(ErrorMessageResourceName = "YearhijriendError", ErrorMessageResourceType = typeof(StudyYear))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "YearHijriEnd", ResourceType = typeof(StudyYear))]
        public Nullable<System.DateTime> YearEdDateHijri { get; set; }

        public Nullable<int> CurrentYear { get; set; }
        public Nullable<int> CurrentSem { get; set; }
        public Nullable<int> CurrentQ { get; set; }
        public Nullable<int> CurrentWeek { get; set; }
        public Nullable<bool> Is_Upload { get; set; }
        public Nullable<bool> Is_Update { get; set; }
    }
}
