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
   public class EmployeeTerminationViewModel
    {
        public int ID { get; set; }
        public int Employee_ID { get; set; }

        [Display(Name = "Salary", ResourceType = typeof(EmployeeTermination))]
        [Required]
        public Nullable<double> Salary { get; set; }

        [Display(Name = "HousingAllowance", ResourceType = typeof(EmployeeTermination))]
        public Nullable<double> HousingAllowance { get; set; }

        [Display(Name = "EndOfService", ResourceType = typeof(EmployeeTermination))]
        public Nullable<double> EndOfService { get; set; }

        [Required]
        [Display(Name = "LastWorkingDate", ResourceType = typeof(EmployeeTermination))]
        public System.DateTime LastWorkingDate { get; set; }

        [Required]
        [Display(Name = "Reason", ResourceType = typeof(EmployeeTermination))]
        public string Reason { get; set; }

        [Required]
        [Display(Name = "TelNo", ResourceType = typeof(EmployeeTermination))]
        public string TelNo { get; set; }

        //public System.DateTime CreatedDate { get; set; }
        //public int CreatedbyID { get; set; }
        //public Nullable<System.DateTime> ModifiedDate { get; set; }
        //public Nullable<int> ModifiedbyID { get; set; }
        //public Nullable<System.DateTime> DeletedDate { get; set; }
        //public Nullable<int> DeletedbyID { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
