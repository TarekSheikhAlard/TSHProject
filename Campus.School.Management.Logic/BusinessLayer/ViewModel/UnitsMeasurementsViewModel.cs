using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.Resources;
using Campus.School.Management.Logic.DataBaseLayer;

namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
   public class UnitsMeasurementsViewModel
    {
    
        public int Id { get; set; }
        [Required]
        [Display(Name = "UnitNumber", ResourceType = typeof(UnitsMeasurements))]
        public string UnitNumber { get; set; }
        [Required]
        [Display(Name = "UnitNameEn", ResourceType = typeof(UnitsMeasurements))]
        public string UnitNameEn { get; set; }
        [Required]
        [Display(Name = "UnitNameAr", ResourceType = typeof(UnitsMeasurements))]
        public string UnitNameAr { get; set; }
        [Required]
        [Display(Name = "SymbolEn", ResourceType = typeof(UnitsMeasurements))]
        public string SymbolEn { get; set; }
        [Required]
        [Display(Name = "SymbolAr", ResourceType = typeof(UnitsMeasurements))]
        public string SymbolAr { get; set; }
    }
}
