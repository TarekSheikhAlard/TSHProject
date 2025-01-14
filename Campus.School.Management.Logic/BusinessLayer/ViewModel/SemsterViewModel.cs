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
    public class SemsterViewModel
    {
        
        public int SID { get; set; }

        [Required(ErrorMessageResourceName = "YearError", ErrorMessageResourceType = typeof(Semster))]
        [Display(Name = "Year", ResourceType = typeof(Semster))]
        public int YearID { get; set; }

        [Display(Name = "Year", ResourceType = typeof(Semster))]
        public string Year { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Semster))]
        public int GradeID { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Semster))]
        public string Grade { get; set;}

        [Required(ErrorMessageResourceName = "SemsterError", ErrorMessageResourceType = typeof(Semster))]
        [Display(Name = "SemsterID", ResourceType = typeof(Semster))]
        [System.Web.Mvc.Remote("CheckSemester", "Semster", AdditionalFields = "YearID,check", ErrorMessageResourceName = "SemsterExistError", ErrorMessageResourceType = typeof(Semster), HttpMethod = "POST")]
        public int SemID { get; set; }

        [Display(Name = "EnglishSemsterName", ResourceType = typeof(Semster))]
        public string SemNameE { get; set; }

        [Display(Name = "ArabicSemsterName", ResourceType = typeof(Semster))]
        public string SemNameA { get; set; }

        [Required(ErrorMessageResourceName = "SemStDateError", ErrorMessageResourceType = typeof(Semster))]
        [Display(Name = "SemStDate", ResourceType = typeof(Semster))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
         public Nullable<System.DateTime> SemStDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "SemEdDateError", ErrorMessageResourceType = typeof(Semster))]
        [Display(Name = "SemEdDate", ResourceType = typeof(Semster))]
        public Nullable<System.DateTime> SemEdDate { get; set; }


        //[Required(ErrorMessageResourceName = "QuartersCountError", ErrorMessageResourceType = typeof(Semster))]
        [Display(Name = "QuartersCount", ResourceType = typeof(Semster))]
        public Nullable<int> QuartersCount { get; set; }


        public bool check { get; set;}





        public Nullable<bool> Is_Upload { get; set; }
        public Nullable<bool> Is_Update { get; set; }

        public virtual AdmAcademicYear AdmAcademicYearList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdmQurter> AdmQurters { get; set; }
        public virtual admStudyear admStudyearList { get; set; }
    }
}
