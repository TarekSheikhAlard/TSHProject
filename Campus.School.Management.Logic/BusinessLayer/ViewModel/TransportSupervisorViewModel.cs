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
    public class TransportSupervisorViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "SupervisorNameEn", ResourceType = typeof(Transport))]
        public string SupervisorNameEn { get; set; }
        [Required]
        [Display(Name = "SupervisorNameAr", ResourceType = typeof(Transport))]
        public string SupervisorNameAr { get; set; }
        [Required]
        [Display(Name = "EmployeeId", ResourceType = typeof(Transport))]
        public Nullable<int> EmployeeId { get; set; }

        public string SchoolCoordinates { get; set; }

        public int CompanyID { get; set; }
        public int SchoolID { get; set; }
    }
}
