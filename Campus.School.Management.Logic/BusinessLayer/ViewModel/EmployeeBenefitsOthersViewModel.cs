using Campus.School.Management.Logic.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Campus.School.Management.Logic.BusinessLayer.ViewModel
{
    public class EmployeeBenefitsOthersViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Name Arabic")]
        public string Name_AR { get; set; }
        [Display(Name = "Name English")]
        public string Name_En { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int AccountTreeID { get; set; }
        public string Formula { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedbyID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> ModifiedbyID { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public Nullable<int> DeletedbyID { get; set; }
    
        public bool IsDefault { get; set; }

        public decimal Percentage { get; set; }

        public string AccountTreeName { get; set; }



    }
}
