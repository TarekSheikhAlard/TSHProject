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
    public class StorePlacesViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "PlaceNumber", ResourceType = typeof(Resources.StorePlaces))]
        public string PlaceNumber { get; set; }
        [Required]
        [Display(Name = "PlaceNameEn", ResourceType = typeof(Resources.StorePlaces))]
        public string PlaceNameEn { get; set; }
        [Required]
        [Display(Name = "PlaceNameAr", ResourceType = typeof(Resources.StorePlaces))]
        public string PlaceNameAr { get; set; }
    }
}
