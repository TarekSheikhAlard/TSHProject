using Campus.School.Management.Logic.DataBaseLayer;
using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class StageViewModel
    {
        [Display(Name = "Stage_ID", ResourceType = typeof(Stage))]
        public int Stage_ID { get; set; }


        [Required(ErrorMessageResourceName = "StageNameArerror", ErrorMessageResourceType = typeof(Stage))]
        [Display(Name = "StageNameAr", ResourceType = typeof(Stage))]

   
        public string StageNameAr { get; set; }

        [Required(ErrorMessageResourceName = "StageNameEnerror", ErrorMessageResourceType = typeof(Stage))]
        [Display(Name = "StageNameEn", ResourceType = typeof(Stage))]
 
        public string StageNameEn { get; set; }

        [Display(Name = "SchoolD", ResourceType = typeof(Stage))]
        public Nullable<int> SchoolD { get; set; }

        public bool IsNew { get; set; }
    }
}
