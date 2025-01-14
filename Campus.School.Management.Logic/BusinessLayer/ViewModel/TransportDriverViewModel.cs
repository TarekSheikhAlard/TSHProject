using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Campus.School.Management.Logic.Resources;
namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class TransportDriverViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "DriverNameEn", ResourceType = typeof(Transport))]
        public string DriverNameEn { get; set; }
        [Required]
        [Display(Name = "DriverNameAr", ResourceType = typeof(Transport))]
        public string DriverNameAr { get; set; }
        [Required]
        [Display(Name = "LicenseNumber", ResourceType = typeof(Transport))]
        public int LicenseNumber { get; set; }
        [Required]
        [Display(Name = "LicenseExpiryDate", ResourceType = typeof(Transport))]
        public DateTime LicenseExpiryDate { get; set; }
   
        [Required]
        [Display(Name = "EmployeeId", ResourceType = typeof(Transport))]
        public Nullable<int> EmployeeId { get; set; }
      
     
    }
}
