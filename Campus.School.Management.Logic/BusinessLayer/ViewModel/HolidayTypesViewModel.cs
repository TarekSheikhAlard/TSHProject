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
   public class HolidayTypesViewModel
    {
        
        public int HolidayID { get; set; }

        [Required(ErrorMessageResourceName = "NameArError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "NameAr", ResourceType = typeof(HolidayTypes))]
        public string HolidayNameAr { get; set; }

        [Required(ErrorMessageResourceName = "NameError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Name", ResourceType = typeof(HolidayTypes))]
        public string HolidayNameEn { get; set; }

        [Required(ErrorMessageResourceName = "DiscountRateError", ErrorMessageResourceType = typeof(HolidayTypes))]
        [Display(Name = "DiscountRate", ResourceType = typeof(HolidayTypes))]
        public decimal Rate { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(HolidayTypes))]
        public string Notes { get; set; }

        [Required(ErrorMessageResourceName = "DaysError", ErrorMessageResourceType = typeof(HolidayTypes))]
        [Display(Name = "Days", ResourceType = typeof(HolidayTypes))]
        public int Days { get; set; }

    }
}
