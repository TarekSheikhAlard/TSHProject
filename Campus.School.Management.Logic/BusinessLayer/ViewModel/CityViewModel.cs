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
   public class CityViewModel
    {
 
        [Display(Name = "ID", ResourceType = typeof(City))]
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "CityArabicError", ErrorMessageResourceType = typeof(City))]
        [Display(Name = "CityArabic", ResourceType = typeof(City))]
        public string CityAr { get; set; }

        [Required(ErrorMessageResourceName = "CountryError", ErrorMessageResourceType = typeof(City))]
        [Display(Name = "Country", ResourceType = typeof(City))]
         public int NationalityID { get; set; }

        [Display(Name = "CountryName", ResourceType = typeof(City))]
        public string NationalityEn { get; set; }

        [Required(ErrorMessageResourceName = "CityEnError", ErrorMessageResourceType = typeof(City))]
        [Display(Name = "CityEn", ResourceType = typeof(City))]
         public string CityEn { get; set; }

        public int CompanyID { get; set; }




    }
}
