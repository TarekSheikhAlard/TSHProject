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
    public class AcademicCostViewModel
    {


        [Display(Name = "AcadimicCostID", ResourceType = typeof(Academic_Cost))]
        public int ID { get; set; }


        [Required(ErrorMessageResourceName = "AcadimicYearIDError", ErrorMessageResourceType = typeof(Academic_Cost))]
        [Display(Name = "AcadimicYearID", ResourceType = typeof(Academic_Cost))]

        public Nullable<int> Academic_YearID { get; set; }

        [Required(ErrorMessageResourceName = "AcadimicYearNameError", ErrorMessageResourceType = typeof(Academic_Cost))]
        [Display(Name = "AcadimicYearName", ResourceType = typeof(Academic_Cost))]

        public string AcadmicName { get; set; }


        [Required(ErrorMessageResourceName = "AcadimicYearCostError", ErrorMessageResourceType = typeof(Academic_Cost))]
        [Display(Name = "AcadimicYearCost", ResourceType = typeof(Academic_Cost))]
        public Nullable<decimal> Year_Cost { get; set; }
    }
}
