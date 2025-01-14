using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class AccountStudentFeesListViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "BooksFeeserror", ErrorMessageResourceType = typeof(AccountStudentFeesList))]
        [Display(Name = "BooksFees", ResourceType = typeof(AccountStudentFeesList))]
        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        public Nullable<decimal> BooksFees { get; set; }

        [Required(ErrorMessageResourceName = "StudyFeeserror", ErrorMessageResourceType = typeof(AccountStudentFeesList))]
        [Display(Name = "StudyFees", ResourceType = typeof(AccountStudentFeesList))]
        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        public Nullable<decimal> StudyFees { get; set; }

        [Required(ErrorMessageResourceName = "AcademicYearIDerror", ErrorMessageResourceType = typeof(AccountStudentFeesList))]
        [Display(Name = "AcademicYearName", ResourceType = typeof(AccountStudentFeesList))]
        public Nullable<int> AcademicYearID { get; set; }

        [Display(Name = "AcademicYearName", ResourceType = typeof(AccountStudentFeesList))]
        public string AcademicYearName { get; set; }
    }
}
