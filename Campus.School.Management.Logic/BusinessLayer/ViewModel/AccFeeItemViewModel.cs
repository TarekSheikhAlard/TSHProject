using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class AccFeeItemViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceName = "NameError", ErrorMessageResourceType = typeof(AccFeeItem))]
        [Display(Name = "Name", ResourceType = typeof(AccFeeItem))]
        public string NAME_EN { get; set; }

        [Required(ErrorMessageResourceName = "NameArError", ErrorMessageResourceType = typeof(AccFeeItem))]
        [Display(Name = "Name", ResourceType = typeof(AccFeeItem))]
        public string NAME_AR { get; set; }      
    }
}
