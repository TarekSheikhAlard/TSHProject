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
   public  class NationalityViewModel
    {
        [Display(Name = "NationalityID", ResourceType = typeof(Nationality))]
   
        public int NationalityID { get; set; }


        [Required(ErrorMessageResourceName = "NationalityArerror", ErrorMessageResourceType = typeof(Nationality))]
        [Display(Name = "NationalityAr", ResourceType = typeof(Nationality))]
        public string NationalityAr { get; set; }

        [Required(ErrorMessageResourceName = "NationalityEnerror", ErrorMessageResourceType = typeof(Nationality))]
        [Display(Name = "NationalityEn", ResourceType = typeof(Nationality))]
      
        public string NationalityEn { get; set; }
        public int CompanyID { get; set; }

    }
}
