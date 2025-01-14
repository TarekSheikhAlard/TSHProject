using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Campus.School.Management.Logic.Resources;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
  public  class StudentDiscountsViewModel
    {
        [Display(Name = "DiscountID", ResourceType = typeof(StudentDiscounts))]
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "DiscountTypeerror", ErrorMessageResourceType = typeof(StudentDiscounts))]
        [Display(Name = "DiscountType", ResourceType = typeof(StudentDiscounts))]
    
        public string DiscountType { get; set; }

        [RegularExpression("^[0-9]\\d*(\\.\\d+)?$", ErrorMessageResourceName = "Reg_Expmoney", ErrorMessageResourceType = typeof(GeneralResource))]
        [Required(ErrorMessageResourceName = "Disc_Amounterror", ErrorMessageResourceType = typeof(StudentDiscounts))]
        [Display(Name = "Disc_Amount", ResourceType = typeof(StudentDiscounts))]
        public Decimal Disc_Amount { get; set; }


    }
}
