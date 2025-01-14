using Campus.School.Management.Logic.DataBaseLayer;
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
    public class MaterialManufactureViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "MaterialNumber", ResourceType = typeof(MaterialManufactures))]
        public string MaterialNumber { get; set; }
        [Required]
        [Display(Name = "MaterialNameEn", ResourceType = typeof(MaterialManufactures))]
        public string MaterialNameEn { get; set; }
        [Required]
        [Display(Name = "MaterialNameAr", ResourceType = typeof(MaterialManufactures))]
        public string MaterialNameAr { get; set; }
    }
}
