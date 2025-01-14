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
    public class EmployeeReturnHistoryViewModel
    {
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        [Required(ErrorMessageResourceName = "ReturnDateError", ErrorMessageResourceType = typeof(EmployeeReturnHistory))]
        [Display(Name = "ReturnDate", ResourceType = typeof(EmployeeReturnHistory))]
        public System.DateTime ReturnDate { get; set; }

        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(EmployeeReturnHistory))]
        public string Notes { get; set; }
    }
}
