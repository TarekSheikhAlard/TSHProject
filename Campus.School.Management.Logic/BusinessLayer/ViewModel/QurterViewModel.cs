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
using Campus.School.Management.Logic.DataBaseLayer;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class QurterViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "QcodeError", ErrorMessageResourceType = typeof(Qurter))]
        [Display(Name = "Qcode", ResourceType = typeof(Qurter))]
        public Nullable<int> QID { get; set; }

        [Required(ErrorMessageResourceName = "SemsterError", ErrorMessageResourceType = typeof(Qurter))]
        [Display(Name = "Semster", ResourceType = typeof(Qurter))]
        public Nullable<int> SID { get; set; }

        [Required(ErrorMessageResourceName = "QStudyWeeksError", ErrorMessageResourceType = typeof(Qurter))]
        [Display(Name = "QStudyWeeks", ResourceType = typeof(Qurter))]
        public Nullable<int> QStudyWeeks { get; set; }

        [Required(ErrorMessageResourceName = "QStudyWeeksAbsError", ErrorMessageResourceType = typeof(Qurter))]
        [Display(Name = "QStudyWeeksAbs", ResourceType = typeof(Qurter))]
        public Nullable<int> QStudyWeeksAbs { get; set; }

        public virtual AdmSemster AdmSemster { get; set; }




        [Display(Name = "ArabicSemsterName", ResourceType = typeof(Semster))]
        public string SemNameA { get; set; }

        [Display(Name = "EnglishSemsterName", ResourceType = typeof(Semster))]
        public string SemNameE { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Semster))]
        public string Grade { get; set;}

        [Display(Name = "Year", ResourceType = typeof(Semster))]
        public string Year { get; set; }

        public int GradeID { get; set; }

        public int YearID { get; set; }

    }
}
